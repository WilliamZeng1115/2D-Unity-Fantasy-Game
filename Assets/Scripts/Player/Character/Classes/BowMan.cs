using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowMan : BaseClass {

    // private ProjectileManager projectileManager;
    // private AbilityManager abilities;

    // Use this for initialization
    public BowMan (GameObject player) {
        skills = new Dictionary<string, int>();
        skills["Strength"] = 3;
        skills["Agility"] = 6;
        skills["Divine Sense"] = 4;
        skills["Dexterity"] = 8;
        maxHealth = 100000000f;
        stamina = 100f;
        skillpoints = 5;
        spiritStones = 0;
        experience = 0;
        maxExperience = 30;
        levelUpExperienceRquiredMultiplier = 2f;
        level = 1;
        staminaRegeneration = 10;
        staminaRegenerationWaitTime = 3f;
        healthRegeneration = 10;
    }

    // override
    public override void ultimate()
    {

    }
}
