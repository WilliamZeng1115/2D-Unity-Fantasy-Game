using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowMan : BaseClass {

    private ProjectileManager projectiles;
    private GameObject player;

    // Use this for initialization
    public BowMan (GameObject player) {
        projectiles = new ProjectileManager("Prefabs/Laser_Projectile"); // get projectilie Manager
        this.player = player;
    }

    // override
    public override void ultimate()
    {

    }

    // override
    public override void basicAttack()
    {
        projectiles.newProjectile(player);
    }
}
