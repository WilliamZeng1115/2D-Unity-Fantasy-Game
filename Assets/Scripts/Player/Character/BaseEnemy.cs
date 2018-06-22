using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : BaseEntity {

    public readonly int baseEnergy;
    public int health, energy, damage, difficultyMultipler;
    private bool isBoss;


    //private abstract void useSkill(); already in BaseEntity

    protected int takeDamage(int damageTaken)
    {
        health -= damageTaken;
        return damageTaken;
    }


    //public abstract int useBasicAttack(); already in BaseEntity

    private void setDifficulty(int difficultylevel)
    {
        difficultyMultipler = difficultylevel;
    }

    private void addEnergy(int energyAdd)
    {
        energy += energyAdd;
    }

    private void addDamage(int damageAdd)
    {
        damage += damageAdd;
    }

    private void addHealth(int healthAdd)
    {
        health += healthAdd;
    }
}
