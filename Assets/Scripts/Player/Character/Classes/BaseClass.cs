using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseClass : MonoBehaviour {

    public int skillPoints;
    public int str, ds, agi, dex;

    public readonly int baseHealth, baseDamage,
        baseEnergy, basePerception;

    // temp for now -> make it enum later
    public string weapon; // adds to damage or attack speed
    public string armor; // adds to health

    // different class use different combination of str, ds, agi, dex to determine attack
    public abstract void ultimate();
    public abstract void basicAttack();

    private void useSkillPoints(int skillPoints)
    {
        // TODO handle error
        if (skillPoints > this.skillPoints) return;
        this.skillPoints -= skillPoints;
    }

    private void addSkillPoints(int skillPoints)
    {
        skillPoints += skillPoints;
    }

    private void equipWeapon(string weapon)
    {
        this.weapon = weapon;
    }

    private void equipArmor(string armor)
    {
        this.armor = armor;
    }

    private void addStrength(int str)
    {
        this.str += str;
    }

    private void addDivineStrength(int ds)
    {
        this.ds += ds;
    }

    private void addAgility(int agi)
    {
        this.agi += agi;
    }

    private void addDex(int dex)
    {
        this.dex += dex;
    }
}
