using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour {

    public GameObject Panel;
    private bool isVisible;

    void Start ()
    {
        isVisible = false;
        Panel.gameObject.SetActive(isVisible);
    }

	public void showhidePanel()
    {
        isVisible = !isVisible;
        Panel.gameObject.SetActive(isVisible);
    }
}
