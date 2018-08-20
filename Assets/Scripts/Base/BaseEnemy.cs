using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour {

    //protected CharacterManager player;
    protected int health, energy, damage, difficultyMultipler, worth = 1;
    protected bool isBoss;
    protected LevelManager levelManager;

    // Weapon Manager
    protected Dictionary<string, WeaponManager> weaponManagers;
    protected WeaponManager selectedWeapon;

    public abstract void abilityAttack();
    public abstract int touchAttack();

    protected void loadWeaponManagers()
    {
        var allChild = gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in allChild)
        {
            if (child.CompareTag("WeaponManager"))
            {
                weaponManagers.Add(child.gameObject.name, child.gameObject.GetComponent<WeaponManager>());
                if (child.gameObject.activeSelf)
                {
                    selectedWeapon = child.gameObject.GetComponent<WeaponManager>();
                }
            }
        }
    }

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
