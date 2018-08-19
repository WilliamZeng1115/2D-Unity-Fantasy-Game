using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowMan : BaseClass {

    private ProjectileManager projectiles;
    // private AbilityManager abilities;

    // Use this for initialization
    public BowMan (GameObject player) {
        skills = new Dictionary<string, int>();
        skills["Strength"] = 3;
        skills["Agility"] = 6;
        skills["Divine Sense"] = 4;
        skills["Dexterity"] = 8;
        maxHealth = 100f;
        projectiles = new ProjectileManager("Prefabs/Projectile/Laser_Projectile", player); // get projectilie Manager
    }

    // override
    public override void ultimate()
    {

    }

    // override
    public override void basicAttack()
    {
        projectiles.newProjectile();
    }
}
