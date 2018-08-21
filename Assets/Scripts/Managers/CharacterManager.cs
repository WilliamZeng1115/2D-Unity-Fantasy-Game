using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Manager
{
    private BaseClass currentClass;

    // Weapon Managers
    private Dictionary<string, WeaponManager> weaponManagers;
    private WeaponManager selectedWeapon;

    // Attributes
    private float currHealth;
    private int skillpoints;
    protected Dictionary<string, int> skills;

    // Manager
    private CharInfoManager charInfoManager;

    // Use this for initialization
    void Start()
    {
        // Class attributes
        currentClass = new BowMan(gameObject);
        currHealth = currentClass.getMaxHealth();
        skillpoints = currentClass.getSkillpoints();
        skills = new Dictionary<string, int>(currentClass.getSkills());

        // Weapons and current weapon
        weaponManagers = new Dictionary<string, WeaponManager>();
        loadWeaponManagers();

        // Managers
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        var charInfo = GameObject.Find("Popups").transform.Find("CharInfo");
        var abilityContentHolder = charInfo.transform.Find("Ability").transform.Find("ContentHolder").gameObject;
        var skillContentHolder = charInfo.transform.Find("Skill").transform.Find("ContentHolder").gameObject;
        charInfoManager = new CharInfoManager(this, abilityContentHolder, skillContentHolder);
    }

    void loadWeaponManagers()
    {
        var childs = transform.GetComponentsInChildren<Transform>();
        foreach (var child in childs)
        {
            if (child.CompareTag("WeaponManager")) {
                weaponManagers.Add(child.gameObject.name, child.gameObject.GetComponent<WeaponManager>());
                if (child.gameObject.name == "BasicSword2") {
                    selectedWeapon = child.gameObject.GetComponent<WeaponManager>();
                    selectedWeapon.applyStats(currentClass.getSkills());
                }
                else 
                {
                    child.gameObject.active = false;
                }
            }
        }
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
        // selected weapon attack
        selectedWeapon.attack();
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

    public int getSkillpoints()
    {
        return skillpoints;
    }

    // These are called when button is clicked 
    public void ApplySkill()
    {
        currentClass.setSkillsAndSP(skills, skillpoints);
        charInfoManager.UpdateSkills(skills, skillpoints);
        selectedWeapon.applyStats(skills);
    }

    public void ResetSkill()
    {
        var skills = currentClass.getSkills();
        var skillpoints = currentClass.getSkillpoints();
        this.skills = new Dictionary<string, int>(skills);
        this.skillpoints = skillpoints;
        charInfoManager.UpdateSkills(skills, skillpoints);
    }

    public void LearnAbility()
    {

    }

    public void EquipOrUnEquipAbility()
    {
        // Depend if selected is equip or not
    }

    public void UpdateSkill(string id, int value)
    {
        var newSPValue = skillpoints - value;
        var newSkillValue = skills[id] + value;
        var isValid = isSkillValid(id, newSkillValue);
        var isValidSP = isSPValid(newSPValue);
        if (isValidSP && isValid)
        {
            skillpoints = newSPValue;
            skills[id] = newSkillValue;
            charInfoManager.UpdateSkill(id, newSPValue.ToString(), newSkillValue.ToString());
        }
    }

    private bool isSkillValid(string id, int value)
    {
        var skills = currentClass.getSkills();
        var oldValue = skills[id];

        return value >= oldValue;
    }

    private bool isSPValid(int sp)
    {
        var skillpoints = currentClass.getSkillpoints();
        return sp <= skillpoints && sp >= 0;
    }

    public Dictionary<string, WeaponManager> getWeaponManagers()
    {
        return weaponManagers;
    }

    public WeaponManager getSelectedWeapon()
    {
        return selectedWeapon;
    }
}