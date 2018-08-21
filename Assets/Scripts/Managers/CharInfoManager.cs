using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

// not extending manager because its special - only used in character manager
public class CharInfoManager
{
    private delegate void ButtonDelegateForSkill(string id, int value);
    private DefaultControls.Resources uiResources;

    private GameObject selected;
    private CharacterManager characterManager;
    private GameObject abilityContentHolder, skillContentHolder;

    // Save text ui element to update on button click
    private Dictionary<string, GameObject> skillUI;
    private GameObject skillpointUI;

    private ButtonDelegateForSkill methodOnClick;

    // Use this for initialization
    public CharInfoManager(CharacterManager characterManager, GameObject abilityContentHolder, GameObject skillContentHolder) {
        // Initialize
        methodOnClick = characterManager.UpdateSkill;
        uiResources = new DefaultControls.Resources();
        skillUI = new Dictionary<string, GameObject>();

        // GameObject
        this.characterManager = characterManager;
        this.abilityContentHolder = abilityContentHolder;
        this.skillContentHolder = skillContentHolder;

        // Render
        RenderAbility();
        RenderSkill();
        RenderEverythingElse();
    }

    private void RenderAbility()
    {

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
            CreateUIText(skill.Key + ": ", 24, keyXPos, yPos);
            // Added to skill ui to modify later on click
            skillUI.Add(skill.Key, CreateUIText(skill.Value.ToString(), 24, textXPos, yPos));

            // Create button
            CreateUIButton("+", 20, 20, buttonAddXPos, yPos, skill.Key, 1, methodOnClick);
            CreateUIButton("-", 20, 20, buttonMinusXPos, yPos, skill.Key, -1, methodOnClick);

            yPos -= 50;
        }
    }

    private void RenderEverythingElse()
    {
        var yPos = -50;
        var textXPos = -50;
        var valXPos = textXPos + 150;
        CreateUIText("Skill Points: ", 24, textXPos, yPos);
        skillpointUI = CreateUIText(characterManager.getSkillpoints().ToString(), 24, valXPos, yPos);
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

    public void UpdateAbility()
    {

    }

    public void UpdateLearnOrBuyButton()
    {

    } 
    
    public void UpdateEquipOrUnEquipButton()
    {
        // Depend if selected is equip or not
    }

    private GameObject CreateUIText(string value, int fontsize, float xPos, float yPos)
    {
        var uiTextElement = DefaultControls.CreateText(uiResources);
        uiTextElement.transform.SetParent(skillContentHolder.transform, false);
        uiTextElement.GetComponent<RectTransform>().localPosition = new Vector2(xPos, yPos);
        uiTextElement.GetComponent<Text>().text = value;
        uiTextElement.GetComponent<Text>().fontSize = fontsize;
        return uiTextElement;
    }

    private GameObject CreateUIButton(string type, float xSize, float ySize, float xPos, float yPos, string id, int value, ButtonDelegateForSkill func)
    {
        var uiButton = DefaultControls.CreateButton(uiResources);
        uiButton.transform.SetParent(skillContentHolder.transform, false);
        uiButton.GetComponent<RectTransform>().sizeDelta = new Vector2(xSize, ySize);
        uiButton.GetComponent<RectTransform>().localPosition = new Vector2(xPos, yPos);
        uiButton.GetComponentInChildren<Text>().text = type;
        uiButton.GetComponent<Button>().onClick.AddListener(() => func(id, value));
        return uiButton;
    }
}
