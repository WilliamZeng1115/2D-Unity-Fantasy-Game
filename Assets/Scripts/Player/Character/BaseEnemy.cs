using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour {

    public readonly int baseHealth, baseDamage, baseEnergy;
    public int health, energy, damage, difficultyMultipler;
    private bool isBoss;


    private void useSkill()
    {

    }

    public abstract int takeDamage(int damageTaken);


    public abstract int useBasicAttack();

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
