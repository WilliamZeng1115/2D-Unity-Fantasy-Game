using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : Manager
{
    Dictionary<string, GameObject> popups;
    Dictionary<string, bool> popupsEnable;

    void Start()
    {
        popups = new Dictionary<string, GameObject>();
        popupsEnable = new Dictionary<string, bool>();
        loadPopups();
        // Set each popup to hidden
        foreach (var popupName in popups.Keys)
        {
            showhidePopup(popupName);
        }
    }
    
    private void loadPopups()
    {
        var popupObjects = GameObject.FindGameObjectsWithTag("Popup");
        foreach (var popup in popupObjects)
        {
            var popupObject = GameObject.Find(popup.name);
            popups.Add(popup.name, popupObject);
            popupsEnable.Add(popup.name, false);
        }
    }

    public void showhidePopup(string popup)
    {
        popups[popup].gameObject.SetActive(popupsEnable[popup]);
        popupsEnable[popup] = !popupsEnable[popup];
    }
}
