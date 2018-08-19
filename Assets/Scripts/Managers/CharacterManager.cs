using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Manager
{
    private BaseClass currentClass;
    
    private float currHealth;
    private int skillpoints;
    
    private LevelManager levelManager;
    private CharInfoManager charInfoManager;

    // Use this for initialization
    void Start()
    {
        currentClass = new BowMan(gameObject);
        currHealth = currentClass.getMaxHealth();
        skillpoints = 5;

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
        return currentClass.getMaxHealth();
    }

    public Dictionary<string, int> getSkills()
    {
        return currentClass.getSkills();
    }

    public bool isSkillValid(string id, int value)
    {
        var skills = currentClass.getSkills();
        var oldValue = skills[id];

        return value >= oldValue;
    }

    public int getSkillpoints()
    {
        return skillpoints;
    }

    public void setSkillpoints(int skillpoints)
    {
        this.skillpoints = skillpoints;
    }

    public void setSkill(string id, int value)
    {
        currentClass.setSkill(id, value);
    }
}