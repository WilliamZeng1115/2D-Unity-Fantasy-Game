using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

// not extending manager because its special - only used in character manager
public class CharInfoManager
{
    private delegate void ButtonDelegate<T>(string id, T value);
    private DefaultControls.Resources uiResources;
    
    private CharacterManager characterManager;
    private GameObject abilityContentHolder, skillContentHolder;

    // buttons
    private GameObject equipOrUnequip;

    // Save text ui element to update on button click
    private Dictionary<string, GameObject> skillUI;
    private Dictionary<string, GameObject> abilityUI;
    private GameObject skillpointUI;
    private string selectedItem;
    private string equipedWeapon;
    private Color selectedColor;
    private Color equipedColor;
    private Color defaultColor;

    private ButtonDelegate<int> methodOnClick;
    private ButtonDelegate<WeaponManager> methodOnClick_two;

    // Use this for initialization
    public CharInfoManager(CharacterManager characterManager, GameObject abilityContent, GameObject skillContent) {
        // Initialize
        methodOnClick = characterManager.UpdateSkill;
        methodOnClick_two = characterManager.SelectWeapon;
        uiResources = new DefaultControls.Resources();
        skillUI = new Dictionary<string, GameObject>();
        abilityUI = new Dictionary<string, GameObject>();
        selectedColor = Color.yellow;
        equipedColor = Color.blue;
        defaultColor = Color.white;

        // GameObject
        this.characterManager = characterManager;
        this.abilityContentHolder = abilityContent.transform.Find("ContentHolder").gameObject;
        this.skillContentHolder = skillContent.transform.Find("ContentHolder").gameObject;
        this.equipOrUnequip = abilityContent.transform.Find("ItemButton").transform.Find("EquipOrUnEquip").gameObject;

        // Render
        RenderAbility();
        RenderSkill();
        RenderEverythingElse();
    }

    public void selectWeapon(string id, WeaponManager weaponManager)
    {

        // set previous selected color to default
        UpdateItemView(id);

        // update content based on weapon manager
        UpdateAbilityContent(weaponManager);
    }

    private void UpdateItemView(string id)
    {
        if (selectedItem != null && equipedWeapon != selectedItem)
        {
            abilityUI[selectedItem].GetComponent<Image>().color = defaultColor;
        }
        selectedItem = id;
        // set new selected item to selected color
        if (equipedWeapon != selectedItem) abilityUI[selectedItem].GetComponent<Image>().color = selectedColor;
        if (selectedItem == equipedWeapon)
        {
            // change button text to unequip;
            this.equipOrUnequip.GetComponent<Text>().text = "Unequip";
        }
        else
        {
            this.equipOrUnequip.GetComponent<Text>().text = "Equip";
        }
    }

    private void UpdateAbilityContent(WeaponManager selected)
    {

    }

    private void RenderAbility()
    {
        var top = -5;
        var bottom = 295;
        var weaponManagers = characterManager.getWeaponManagers();
        foreach (KeyValuePair<string, GameObject> weaponManager in weaponManagers)
        {
            var weapon = weaponManager.Value.GetComponent<WeaponManager>();
            var icon = weapon.getIcon();
            var newSprite = Sprite.Create(icon, new Rect(0.0f, 0.0f, icon.width, icon.height), new Vector2(0.5f, 0.5f), 100.0f);

            var panel = CreateUIPanel<WeaponManager>(5, top, -200, bottom, abilityContentHolder, weaponManager.Key, weapon, methodOnClick_two);
            var image = CreateUIImage(60, 60, -40, 0, panel, newSprite);
            var text = CreateUIText(weapon.getName(), 12, 80, 15, panel);

            abilityUI.Add(weaponManager.Key, panel);
            top -= 95;
            bottom -= 95;
        }
        var currWeapon = characterManager.getEquipedWeapon();
        if (currWeapon != null) equipedWeapon = currWeapon.getName();
    }

    private void RenderSkill()
    {
        var yPos = 155;
        var keyXPos = -90;
        var textXPos = keyXPos + 160;
        var buttonAddXPos = 45;
        var buttonMinusXPos = buttonAddXPos + 25;
        var skills = characterManager.getSkills();
        foreach (KeyValuePair<string, int> skill in skills)
        {
            // Create text
            CreateUIText(skill.Key + ": ", 24, keyXPos, yPos, skillContentHolder);
            // Added to skill ui to modify later on click
            skillUI.Add(skill.Key, CreateUIText(skill.Value.ToString(), 24, textXPos, yPos, skillContentHolder));

            // Create button
            CreateUIButton<int>("+", 20, 20, buttonAddXPos, yPos, skillContentHolder, skill.Key, 1, methodOnClick);
            CreateUIButton<int>("-", 20, 20, buttonMinusXPos, yPos, skillContentHolder, skill.Key, -1, methodOnClick);

            yPos -= 50;
        }
    }

    private void RenderEverythingElse()
    {
        var yPos = -50;
        var textXPos = -50;
        var valXPos = textXPos + 150;
        CreateUIText("Skill Points: ", 24, textXPos, yPos, skillContentHolder);
        skillpointUI = CreateUIText(characterManager.getSkillpoints().ToString(), 24, valXPos, yPos, skillContentHolder);
    }

    // if ability already equiped then UI says Unequip
    // if ability isn't equiped then UI says Equip
    // Called when ability is clicked
    public void SelectAbility(string id)
    {

    }

    // Update local change (UI update only)
    public void UpdateSkill(string id, string sp, string skill)
    {
        var textSkillElement = skillUI[id].GetComponent<Text>();
        var textSPElement = skillpointUI.GetComponent<Text>();
        textSPElement.text = sp;
        textSkillElement.text = skill;
    }

    // These are called when button is clicked 
    public void UpdateSkills(Dictionary<string, int> skills, int skillpoints)
    {
        foreach (KeyValuePair<string, int> skill in skills)
        {
            var textSkillElement = skillUI[skill.Key].GetComponent<Text>();
            textSkillElement.text = skill.Value.ToString();
        }
        skillpointUI.GetComponent<Text>().text = skillpoints.ToString();
    }

    public void UpdateLearnOrBuyButton()
    {
        // TODO
    } 
    
    public void UpdateEquipOrUnEquipItem(string equiped, string selected, bool equip)
    {
        if (selected == null) return;
        if (equip)
        {
            equipedWeapon = selected;
            abilityUI[selected].GetComponent<Image>().color = equipedColor;
            if (equiped != null) abilityUI[equiped].GetComponent<Image>().color = defaultColor;
            this.equipOrUnequip.GetComponent<Text>().text = "Unequip";
        }
        else
        {
            equipedWeapon = null;
            abilityUI[equiped].GetComponent<Image>().color = selectedColor;
            this.equipOrUnequip.GetComponent<Text>().text = "Equip";
        }
    }

    private GameObject CreateUIText(string value, int fontsize, float xPos, float yPos, GameObject contentHolder)
    {
        var uiTextElement = DefaultControls.CreateText(uiResources);
        uiTextElement.transform.SetParent(contentHolder.transform, false);
        uiTextElement.GetComponent<RectTransform>().localPosition = new Vector2(xPos, yPos);
        uiTextElement.GetComponent<Text>().text = value;
        uiTextElement.GetComponent<Text>().fontSize = fontsize;
        return uiTextElement;
    }

    private GameObject CreateUIButton<T>(string text, float xSize, float ySize, float xPos, float yPos, GameObject contentHolder, string id, T value, ButtonDelegate<T> func)
    {
        var uiButton = DefaultControls.CreateButton(uiResources);
        uiButton.transform.SetParent(contentHolder.transform, false);
        uiButton.GetComponent<RectTransform>().sizeDelta = new Vector2(xSize, ySize);
        uiButton.GetComponent<RectTransform>().localPosition = new Vector2(xPos, yPos);
        uiButton.GetComponentInChildren<Text>().text = text;
        uiButton.GetComponent<Button>().onClick.AddListener(() => func(id, value));
        return uiButton;
    }
    
    private GameObject CreateUIPanel<T>(float left, float top, float right, float bottom, GameObject contentHolder, string id, T value, ButtonDelegate<T> func)
    {
        var uiPanel = DefaultControls.CreatePanel(uiResources);
        uiPanel.transform.SetParent(contentHolder.transform, false);
        
        uiPanel.GetComponent<RectTransform>().offsetMin = new Vector2(left, bottom);//left-bottom;
        uiPanel.GetComponent<RectTransform>().offsetMax = new Vector2(right, top);//right-top

        uiPanel.GetComponent<Image>().color = defaultColor;
        var button = uiPanel.AddComponent<Button>();
        button.onClick.AddListener(() => func(id, value));
        var colorBlock = button.colors;
        colorBlock.highlightedColor = defaultColor;
        button.colors = colorBlock;
        return uiPanel;
    }

    private GameObject CreateUIImage(float xSize, float ySize, float xPos, float yPos, GameObject contentHolder, Sprite sprite)
    {
        var uiImage = DefaultControls.CreateImage(uiResources);
        uiImage.transform.SetParent(contentHolder.transform, false);
        uiImage.GetComponent<RectTransform>().sizeDelta = new Vector2(xSize, ySize);
        uiImage.GetComponent<RectTransform>().localPosition = new Vector2(xPos, yPos);
        uiImage.GetComponent<Image>().sprite = sprite;
        return uiImage;
    }
}
