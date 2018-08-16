using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrogantYoungMaster : BaseEnemy {

	// Use this for initialization
	void Start () {
        health = 100;
        energy = 100;
        damage = 10;
        difficultyMultipler = 1;
        isBoss = false;
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        levelManager.OnCollideForEnemy(gameObject, other.gameObject, this);
    }

    public override int basicAttack() {
        return damage;
    }

    public override void skill() {

    }
}
