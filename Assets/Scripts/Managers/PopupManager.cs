using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour {

    Dictionary<string, GameObject> popups;
    Dictionary<string, bool> popupsEnable;

    void Start()
    {
        popups = new Dictionary<string, GameObject>();
        popupsEnable = new Dictionary<string, bool>();

        loadPopups();
        showhidePopup("CharInfo");
        showhidePopup("Option");
    }
    
    private void loadPopups()
    {
        var charInfo = GameObject.Find("CharInfo");
        var option = GameObject.Find("Option");
        popups["CharInfo"] = charInfo;
        popups["Option"] = option;
        popupsEnable["CharInfo"] = false;
        popupsEnable["Option"] = false;
    }

    public void showhidePopup(string popup)
    {
        popups[popup].gameObject.SetActive(popupsEnable[popup]);
        popupsEnable[popup] = !popupsEnable[popup];
    }
}
