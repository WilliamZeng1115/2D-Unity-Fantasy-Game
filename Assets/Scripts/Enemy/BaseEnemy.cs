using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour {

    protected CharacterManager player;

    protected int health, energy, damage, difficultyMultipler, worth = 1;
    protected bool isBoss;

    public abstract void skill();
    public abstract int basicAttack();

    protected void takeDamage(int damageTaken)
    {
        health -= damageTaken;
        if (health <= 0)
        {
            if (player != null) player.addScore(worth);
            Destroy(gameObject);
        }
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
