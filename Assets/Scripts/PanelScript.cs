using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour {

    public GameObject Panel;
    private int counter = 0;

    void Start ()
    {
        Panel.gameObject.SetActive(false);
    }

	public void showhidePanel()
    {
        counter++;
        if (counter%2 == 1)
        {
            Panel.gameObject.SetActive(false);
        } else
        {
            Panel.gameObject.SetActive(true);
        }
    }
		
}
