using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassManager : MonoBehaviour {

    private BaseClass currentClass;
    private GameObject player;

	// Use this for initialization
	void Start() {
        player = GameObject.Find("Player");
        currentClass = new BowMan(player);
	}
	
	public void switchClass(BaseClass newClass)
    {
        currentClass = newClass;
    }

    public void upgradeClass()
    {
        // later when we make more class
    }

    public BaseClass getClass()
    {
        return currentClass;
    }

    // type string for now -> change to item later
    public bool isItemAvailableToClass(string item)
    {
        return true;
    }
}
