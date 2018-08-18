using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrogantYoungMaster : BaseEnemy {

    private ProjectileManager projectiles;

    // Use this for initialization
    void Start () {
        health = 100;
        energy = 100;
        damage = 10;
        difficultyMultipler = 1;
        isBoss = false;
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        projectiles = new ProjectileManager("Prefabs/Projectile/Enemy_Laser_Projectile", this.gameObject); // get projectilie Manager
        InvokeRepeating("skillAttack", 2.0f, 1.0f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        levelManager.OnCollideForEnemy(gameObject, other.gameObject, this);
    }

    public override int basicAttack() {
        return damage;
    }

    public override int skillAttack() {
        instantiateSkill();
        return damage * 2;
    }

    private void instantiateSkill()
    {
        projectiles.newProjectile(false);
    }
}
