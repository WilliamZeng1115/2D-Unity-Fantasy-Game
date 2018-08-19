using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CharInfoManager : Manager
{
    private GameObject selected;
    private CharacterManager characterManager;
    private GameObject abilityContentHolder, skillContentHolder; 

    // Use this for initialization
    void Start () {
        characterManager = GameObject.Find("Player").GetComponent<CharacterManager>();
        abilityContentHolder = GameObject.Find("Ability").transform.Find("ContentHolder").gameObject;
        skillContentHolder = GameObject.Find("Skill").transform.Find("ContentHolder").gameObject;
        RenderAbility();
        RenderSkill();
    }

    // Get an dictionary of skills and their values from character manager
    // Generate the UI according to the values with buttons and store them 
    private void RenderAbility()
    {

    }

    // Get an dictionary of skills and their values from character manager
    // Generate the UI according to the values with buttons and store them 
    private void RenderSkill()
    {

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

    }

    public void ResetSkill()
    {

    }

    public void LearnAbility()
    {

    } 
    
    public void EquipOrUnEquipAbility()
    {
        // Depend if selected is equip or not
    }

    // These are called when character is updated in CharacterManager
    private void UpdateAbility(string id, int value)
    {

    }

    private void UpdateSkill(string id, int value)
    {

    }
}
