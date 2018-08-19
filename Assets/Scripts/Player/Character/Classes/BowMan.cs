using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowMan : BaseClass {

    private ProjectileManager projectiles;
    // private AbilityManager abilities;
    private GameObject player;

    // Use this for initialization
    public BowMan (GameObject player) {
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
