using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : BaseItem
{
    public override void use()
    {
        count--;
        if (count <= 0) Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        size = 10; // scale down by 10 so this is actually 1
        cost = 100;
        value = 25;
        description = "A basic potion restore" + value + "health points";
        name = "Health Potion";
        count = 2;
    }
}
