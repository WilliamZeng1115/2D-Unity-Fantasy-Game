using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseClass {

    protected Dictionary<string, int> skills;
    protected int skillpoints;
    protected float maxHealth;

    // different class use different combination of str, ds, agi, dex to determine attack
    public abstract void ultimate();

    // TODO - add a check if its one of the attribute we want str, ds, agi, dex
    public void setSkillsAndSP(Dictionary<string, int> skills, int skillpoints)
    {
        foreach (KeyValuePair<string, int> skill in skills)
        {
            this.skills[skill.Key] = skill.Value;
        }
        this.skillpoints = skillpoints;
    }

    // TODO - add a check if its one of the attribute we want str, ds, agi, dex
    public void addSkill(string id, int value)
    {
        skills[id] += value;
    }

    public int getSkillPoints(string id)
    {
        return skills[id];
    }

    public Dictionary<string, int> getSkills()
    {
        return skills;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }
    
    public int getSkillpoints()
    {
        return skillpoints;
    }
}
