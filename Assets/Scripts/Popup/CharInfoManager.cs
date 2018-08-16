using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CharInfoManager : MonoBehaviour {

    private GameObject[] statsValues;
    private GameObject player;
    public enum SkillNames { Strength, Agility, Dexterity, DivineSense}
    private BaseClass currClass;
    private Dictionary<SkillNames, int> addedSkill;
    private Button addStrBtn;
    private int availSkillPoints;
    private Text availSKillPointsText;

    // Use this for initialization
    void Start () {
        statsValues = GameObject.FindGameObjectsWithTag("Stats Value");
        findPlayerObject();
        if (player) currClass = player.GetComponent<CharacterManager>().getClass();
        populateStats();
        

        //Set button listeners
        addStrBtn = GameObject.Find("StrengthAdd").GetComponent<Button>(); //use tags later?
        addStrBtn.onClick.AddListener(delegate { addSkillPoint(SkillNames.Strength); });

        //Testing, set available skill points to 10 by default
        availSkillPoints = 20;
        availSKillPointsText = GameObject.Find("SkillPoints Text").GetComponent<Text>();
        updateAvailSkillPts();
    }
	
    void findPlayerObject()
    {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        catch (NullReferenceException ex)
        {

        }
    }

    private void populateStats()
    {
        if (currClass != null)
        {
            for (int i = 0; i < statsValues.Length; i++)
            {
                if (statsValues[i].name == "Strength Text")
                {
                    statsValues[i].GetComponent<Text>().text = currClass.getStr().ToString();
                }
                if (statsValues[i].name == "Agility Text")
                {
                    statsValues[i].GetComponent<Text>().text = currClass.getAgi().ToString();
                }
                if (statsValues[i].name == "Dexterity Text")
                {
                    statsValues[i].GetComponent<Text>().text = currClass.getDex().ToString();
                }
                if (statsValues[i].name == "DivineSense Text")
                {
                    statsValues[i].GetComponent<Text>().text = currClass.getDS().ToString();
                }
                if (statsValues[i].name == "SkillPoints Text")
                {
                    statsValues[i].GetComponent<Text>().text = "0";
                }
                /*
                if (statsValues[i].name == "CultivationLevel Text")
                {
                    statsValues[i].GetComponent<Text>().text = currClass.getStr().ToString();
                }
                if (statsValues[i].name == "Spirit Energy Text")
                {
                    statsValues[i].GetComponent<Text>().text = currClass.getStr().ToString();
                }
                */
            }
        }
    }

    void setAvailSkillPts(int amount)
    {
        availSkillPoints = amount;

    }

    void setSkillPoint(SkillNames skillName, int amount)
    {
        addedSkill[skillName] = amount;
    }

    public void addSkillPoint(SkillNames skillName)
    {
        addedSkill[skillName] = +1;
        if (availSkillPoints > 0) availSkillPoints--;
        updateAvailSkillPts();
    }

    private void updateAvailSkillPts()
    {
       availSKillPointsText.text = availSkillPoints.ToString();
    }

    public void saveAddedPoints()
    {
        foreach (KeyValuePair<SkillNames,int> i in addedSkill)
        {
            if (i.Key == SkillNames.Strength)
            {
                currClass.addStrength(i.Value);
            }
            if (i.Key == SkillNames.Agility)
            {
                currClass.addAgility(i.Value);
            }
            if (i.Key == SkillNames.Dexterity)
            {
                currClass.addDex(i.Value);
            }
            if (i.Key == SkillNames.DivineSense)
            {
                currClass.addDivineSense(i.Value);
            }
        }

    }
    
	// Update is called once per frame
	void Update () {
		
	}
}
