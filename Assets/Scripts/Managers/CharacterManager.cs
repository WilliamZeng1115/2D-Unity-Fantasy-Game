using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private BaseClass currentClass;
    
    private float currHealth, maxHealth;
    private int skillPoints;
    
    private LevelManager levelManager;

    // temp for now -> make it enum later
    private string weapon, armor;

    // Use this for initialization
    void Start()
    {
        currentClass = new BowMan(gameObject);

        maxHealth = 100f; 
        currHealth = maxHealth;
        skillPoints = 5;

        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
     }

    void OnCollisionEnter2D(Collision2D other)
    {
        levelManager.OnCollideForCharacter(gameObject, other.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        levelManager.OnCollideForCharacter(gameObject, other.gameObject);
    }

    public void switchClass(BaseClass newClass)
    {
        currentClass = newClass;
    }

    public BaseClass getClass()
    {
        return currentClass;
    }
    
    public void useSkillPoints(int skillPoints)
    {
        // TODO handle error
        if (skillPoints > this.skillPoints) return;
        this.skillPoints -= skillPoints;
    }

    public void addSkillPoints(int skillPoints)
    {
        this.skillPoints += skillPoints;
    }

    public void equipWeapon(string weapon)
    {
        this.weapon = weapon;
    }

    public void equipArmor(string armor)
    {
        this.armor = armor;
    }

    public void useSkill()
    {
        currentClass.basicAttack();
    }

    public float takeDamage(float damage)
    {
        currHealth -= damage;
        return currHealth;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }
}