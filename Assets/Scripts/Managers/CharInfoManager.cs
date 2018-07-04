using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharInfoManager : MonoBehaviour {

    private GameObject[] statsValues;
    private GameObject player;
	// Use this for initialization
	void Start () {
        statsValues = GameObject.FindGameObjectsWithTag("Stats Value");
        player = GameObject.FindGameObjectWithTag("Player");
        populateStats();
    }
	
    private void populateStats()
    {
        BaseClass currClass = player.GetComponent<CharacterManager>().getClass();
        for (int i = 0; i < statsValues.Length; i++)
        {
            Debug.Log(statsValues[i].name);
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
                statsValues[i].GetComponent<Text>().text = player.GetComponent<CharacterManager>().getScore().ToString();
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

	// Update is called once per frame
	void Update () {
		
	}
}
