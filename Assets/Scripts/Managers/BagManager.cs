using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagManager {

    private GameObject bagOfHoldingContainer;
    private Dictionary<string, GameObject> itemTemplates;
    private List<string> playerItems;
    private GameObject selected;
  
    public BagManager(GameObject bagOfHoldingContainer)
    {
        this.bagOfHoldingContainer = bagOfHoldingContainer;
        itemTemplates = new Dictionary<string, GameObject>();
        playerItems = new List<string>();

        LoadTemplateItems();
        LoadPlayerItems();
    }

    private void LoadTemplateItems()
    {
        var items = Resources.LoadAll<GameObject>("Prefabs/Item");
        foreach (var item in items)
        {
            itemTemplates.Add(item.name, item);
        }
    }

    private void LoadPlayerItems()
    {
        var childs = bagOfHoldingContainer.transform.GetComponentsInChildren<Transform>();
        foreach (var child in childs)
        {
            if (child.CompareTag("Item"))
            {
                playerItems.Add(child.name);
            }
        }
    }

    // Attach button Use and Drop to these two functions
    public BaseItem Drop()
    {
        if (selected == null) return null;
        var itemScript = selected.GetComponent<BaseItem>();
        return itemScript;
    }

    // Call level manager with item script component to get item effect.
    // Delete item or deduct usage
    public BaseItem Use()
    {
        if (selected == null) return null;
        var itemScript = selected.GetComponent<BaseItem>();
        return itemScript;
    }

    // Calls on buy item in shop or sell item in shop
    // Add or Drop item (UI)
    // Update player list as well (Date)
    public void UpdateBagOfHolding()
    {

    }

    public void SelectItem(GameObject selected)
    {
        this.selected = selected;
    }
}
