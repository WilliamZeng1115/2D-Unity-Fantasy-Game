using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Manager
{
    private BaseClass currentClass;
    
    private float currHealth, maxHealth;
    private int skillPoints;
    
    private LevelManager levelManager;
    private CharInfoManager charInfoManager;

    // Use this for initialization
    void Start()
    {
        currentClass = new BowMan(gameObject);
        maxHealth = currentClass.getMaxHealth();
        currHealth = maxHealth;
        skillPoints = 5;

        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        charInfoManager = GameObject.Find("CharInfo").GetComponent<CharInfoManager>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        levelManager.OnCollideForCharacter(gameObject, other.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        levelManager.OnCollideForCharacter(gameObject, other.gameObject);
    }

    public BaseClass getClass()
    {
        return currentClass;
    }

    public void addSkillPoints(int skillPoints)
    {
        this.skillPoints += skillPoints;
    }

    public void useAbility()
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