using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrogantYoungMaster : BaseEnemy {

    void Start () {
        health = 100;
        energy = 100;
        damage = 10;
        difficultyMultipler = 1;
        isBoss = false;
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        weaponManagers = new Dictionary<string, WeaponManager>();
        loadWeaponManagers();

        //InvokeRepeating("abilityAttack", 2.0f, 1.0f); Using enemy animation even trigger instead
        //InvokeRepeating("changeDirections", 2.0f, 3.0f); //Changes direction of enemy every 3 seconds
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        levelManager.OnCollideForEnemy(gameObject, other.gameObject, this);
    }

    public override int touchAttack() {
        return damage;
    }

    public override void abilityAttack() {
        selectedWeapon.attack();
    }

    void Update()
    {
        enemyMovement();
    }
}
