using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseClass {

    protected Dictionary<string, int> skills;
    protected float maxHealth;

    // different class use different combination of str, ds, agi, dex to determine attack
    public abstract void ultimate();
    public abstract void basicAttack();

    // TODO - add a check if its one of the attribute we want str, ds, agi, dex
    public void setSkill(string id, int value)
    {
        skills[id] = value;
    }

    // TODO - add a check if its one of the attribute we want str, ds, agi, dex
    public void addSkill(string id, int value)
    {
        skills[id] += value;
    }

    public Dictionary<string, int> getSkills()
    {
        return skills;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }
}
