using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CharInfoManager : Manager
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
    void Start () {
        // Initialize
        methodOnClick = UpdateSkill;
        uiResources = new DefaultControls.Resources();
        skillUI = new Dictionary<string, GameObject>();

        // GameObject
        characterManager = GameObject.Find("Player").GetComponent<CharacterManager>();
        abilityContentHolder = GameObject.Find("Ability").transform.Find("ContentHolder").gameObject;
        skillContentHolder = GameObject.Find("Skill").transform.Find("ContentHolder").gameObject;

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

    // These are called when button is clicked 
    public void ApplySkill()
    {
        foreach (KeyValuePair<string, GameObject> skill in skillUI)
        {
            var skillValue = int.Parse(skill.Value.GetComponent<Text>().text);
            characterManager.setSkill(skill.Key, skillValue);
        }
        var skillpoints = int.Parse(skillpointUI.GetComponent<Text>().text);
        characterManager.setSkillpoints(skillpoints);
    }

    public void ResetSkill()
    {
        var skills = characterManager.getSkills();
        foreach (KeyValuePair<string, int> skill in skills)
        {
            SetSkill(skill.Key, skill.Value);
        }
        skillpointUI.GetComponent<Text>().text = characterManager.getSkillpoints().ToString();
    }

    public void LearnAbility()
    {

    } 
    
    public void EquipOrUnEquipAbility()
    {
        // Depend if selected is equip or not
    }

    private void SetSkill(string id, int value)
    {
        var textSkillElement = skillUI[id].GetComponent<Text>();
        textSkillElement.text = value.ToString();
    }

    // Update local change (UI update only)
    private void UpdateSkill(string id, int value)
    {
        var textSkillElement = skillUI[id].GetComponent<Text>();
        var newSkillValue = int.Parse(textSkillElement.text) + value;

        var textSPElement = skillpointUI.GetComponent<Text>();
        var newSPValue = int.Parse(textSPElement.text) - value;

        var isValid = characterManager.isSkillValid(id, newSkillValue);
        if (newSPValue >= 0 && isValid)
        {
            textSPElement.text = newSPValue.ToString();
            textSkillElement.text = newSkillValue.ToString();
        }
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
