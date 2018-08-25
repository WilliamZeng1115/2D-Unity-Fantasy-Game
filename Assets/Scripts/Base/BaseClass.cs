using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseClass {

    protected Dictionary<string, int> skills;
    protected int skillpoints;
    protected float maxHealth;
    protected float stamina;
    protected int spiritStones;
    protected int level;
    protected int experience;
    protected int maxExperience;
    protected int staminaRegeneration;
    protected float staminaRegenerationWaitTime;
    protected int healthRegeneration;

    // Multipliers
    protected float levelUpExperienceRquiredMultiplier;

    // different class use different combination of str, ds, agi, dex to determine attack
    public abstract void ultimate();

    public bool addExperience(int experience)
    {
        var isLevelUp = false;
        this.experience += experience;
        while (this.experience >= maxExperience)
        {
            this.experience = this.experience - maxExperience;
            maxExperience = (int)(maxExperience * levelUpExperienceRquiredMultiplier);
            level++;
            skillpoints += 5;
            isLevelUp = true;
        }
        return isLevelUp;
    }

    public int getStaminaRegenerationRate()
    {
        return staminaRegeneration;
    }

    public float getRegenerationWaitTime()
    {
        return staminaRegenerationWaitTime;
    }

    public void addStamina(int stamina)
    {
        this.stamina += stamina;
    }

    // can't lose levels. . . TODO
    public void removeExperience(int experience)
    {
        this.experience -= experience;
        if (this.experience <= 0) this.experience = 0;
    }

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

    public float getMaxStamina()
    {
        return stamina;
    }

    public int getMaxExperience()
    {
        return maxExperience;
    }

    public int getExperience()
    {
        return experience;
    }

    public int getLevel()
    {
        return level;
    }

    public int addSpiritStone(int amount)
    {
        if (amount <= 0 && spiritStones <= 0) return 0;
        spiritStones += amount;
        return spiritStones;
    }

    public int getSpiritStone()
    {
        return spiritStones;
    }
}
