﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour {

    //protected CharacterManager player;
    protected int health, energy, damage, difficultyMultipler, worth = 1;
    protected bool isBoss;
    protected LevelManager levelManager;

    public abstract int skillAttack();
    public abstract int basicAttack();

    public int takeDamage(int damageTaken)
    {
        health -= damageTaken;
        return health;
    }

    public int getWorth()
    {
        return worth;
    }

    protected void addEnergy(int energyAdd)
    {
        energy += energyAdd;
    }

    protected void addDamage(int damageAdd)
    {
        damage += damageAdd;
    }

    protected void addHealth(int healthAdd)
    {
        health += healthAdd;
    }
}
