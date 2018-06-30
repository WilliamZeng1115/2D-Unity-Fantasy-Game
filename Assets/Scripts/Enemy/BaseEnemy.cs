using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour {

    protected int health, energy, damage, difficultyMultipler;
    protected bool isBoss;

    public abstract void useSkill();
    public abstract int useBasicAttack(); 

    protected void takeDamage(int damageTaken)
    {
        health -= damageTaken;
        if (health <= 0) Destroy(gameObject);
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
