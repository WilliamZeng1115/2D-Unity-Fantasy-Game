using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : Manager
{
    private BaseClass currentClass;

    // Weapon Managers
    private Dictionary<string, GameObject> weaponManagers;
    private string equipedWeapon;
    private string selectedWeapon;

    // Attributes
    private float currHealth;
    private float currStamina;
    private int currScore;
    private int skillpoints;
    protected Dictionary<string, int> skills;

    // Manager
    private CharInfoManager charInfoManager;
    private BagManager bagManager;

    private Animator anim;

    // Use this for initialization
    void Start()
    {
        // Class attributes
        currentClass = new BowMan(gameObject);
        currHealth = currentClass.getMaxHealth();
        currStamina = currentClass.getMaxStamina();
        skillpoints = currentClass.getSkillpoints();
        skills = new Dictionary<string, int>(currentClass.getSkills());
        
        // Managers -> These can be reloaded when scene change and new LevelManager, popups, ability

        // Weapons and current weapon
        weaponManagers = new Dictionary<string, GameObject>();
        loadWeaponManagers();
        
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        var charInfo = GameObject.Find("Popups").transform.Find("CharInfo");
        var abilityUI = charInfo.transform.Find("Ability").gameObject;
        var skillUI = charInfo.transform.Find("Skill").gameObject;
        charInfoManager = new CharInfoManager(this, abilityUI, skillUI);
        charInfoManager.UpdateEquipOrUnEquipItem(null, equipedWeapon, true);

        var bagOfHolding = GameObject.Find("Popups").transform.Find("BagOfHolding").gameObject;
        bagManager = new BagManager(bagOfHolding);

        anim = GetComponent<Animator>();
    }

    void loadWeaponManagers()
    {
        var childs = transform.GetComponentsInChildren<Transform>();
        foreach (var child in childs)
        {
            if (child.CompareTag("WeaponManager"))
            {
                var weaponManager = child.gameObject.GetComponent<WeaponManager>();
                weaponManagers.Add(weaponManager.getName(), child.gameObject);
                child.gameObject.active = false;
            }
        }
        var wp = weaponManagers.First().Value;
        if (wp != null)
        {
            wp.gameObject.active = true;
            var weapon = wp.GetComponent<WeaponManager>();
            weapon.applyStats(currentClass.getSkills());
            equipedWeapon = weapon.getName();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        levelManager.OnCollideForCharacter(gameObject, other.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        levelManager.OnTriggerForCharacter(gameObject, other.gameObject);
    }

    public BaseClass getClass()
    {
        return currentClass;
    }

    public void useAbility()
    {
        // selected weapon attack
        if (equipedWeapon == null) return;
        var weaponManager = weaponManagers[equipedWeapon].GetComponent<WeaponManager>();
        weaponManager.attack();
        weaponManager.GetComponent<BoxCollider2D>().enabled = true;
        anim.SetTrigger("noAttack");
    }

    public float takeDamage(float damage)
    {
        currHealth -= damage;
        return currHealth;
    }

    public float heal(float health)
    {
        currHealth += health;
        if (currHealth > getMaxHealth()) currHealth = getMaxHealth();
        return currHealth; 
    }

    public float useStamina(float stamina)
    {
        currStamina -= stamina;
        return currStamina;
    }
    
    public float getStamina()
    {
        return currStamina;
    }

    public float getMaxHealth()
    {
        return currentClass.getMaxHealth();
    }

    public float getMaxStamina()
    {
        return currentClass.getMaxStamina();
    }

    public bool onEnemyKill(int experience, int spiritStone, int score)
    {
        var isLevelUp = currentClass.addExperience(experience);
        currentClass.addSpiritStone(spiritStone);
        currScore += score;
        if (isLevelUp)
        {
            skillpoints = currentClass.getSkillpoints();
            charInfoManager.UpdateSkillpoint(skillpoints);
        }
        return isLevelUp;
    }

    public void regenerateStamina()
    {
        var maxStamina = currentClass.getMaxStamina(); 
        if (currStamina <= maxStamina)
        {
            var regenerationRate = currentClass.getStaminaRegenerationRate();
            currStamina += regenerationRate;
            currStamina = currStamina > maxStamina ? maxStamina : currStamina;
        }
    }

    public float getRegenerationWaitTime()
    {
        return currentClass.getRegenerationWaitTime();
    }

    public int getScore()
    {
        return currScore;
    }

    public int getCurrentExperience()
    {
        return currentClass.getExperience();
    }

    public int getMaxExperience()
    {
        return currentClass.getMaxExperience();
    }

    public int getSpiritStone()
    {
        return currentClass.getSpiritStone();
    }

    public int getLevel()
    {
        return currentClass.getLevel();
    }

    public Dictionary<string, int> getSkills()
    {
        return currentClass.getSkills();
    }

    public int getSkillpoints()
    {
        return skillpoints;
    }
    // Char Info Button stuff

    // These are called when button is clicked 
    public void ApplySkill()
    {
        currentClass.setSkillsAndSP(skills, skillpoints);
        charInfoManager.UpdateSkills(skills, skillpoints);
        if (equipedWeapon == null) return;
        var weaponManager = weaponManagers[equipedWeapon].GetComponent<WeaponManager>();
        weaponManager.applyStats(skills);
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
        // TODO
    }

    public void EquipOrUnEquipAbility()
    {
        if (selectedWeapon == null) return;
        var equip = (equipedWeapon != selectedWeapon);
        charInfoManager.UpdateEquipOrUnEquipItem(equipedWeapon, selectedWeapon, equip);
        if (equip)
        {
            weaponManagers[selectedWeapon].active = true;
            if (equipedWeapon != null) weaponManagers[equipedWeapon].active = false;
            equipedWeapon = selectedWeapon;
            var weaponManager = weaponManagers[equipedWeapon].GetComponent<WeaponManager>();
            weaponManager.applyStats(skills);
        }
        else if (!equip)
        {
            weaponManagers[equipedWeapon].active = false;
            equipedWeapon = null;
        }
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

    public Dictionary<string, GameObject> getWeaponManagers()
    {
        return weaponManagers;
    }

    public WeaponManager getSelectedWeapon()
    {
        if (equipedWeapon == null) return null;
        var weaponManager = weaponManagers[equipedWeapon].GetComponent<WeaponManager>();
        return weaponManager;
    }

    public WeaponManager getEquipedWeapon()
    {
        if (equipedWeapon == null) return null;
        var weaponManager = weaponManagers[equipedWeapon].GetComponent<WeaponManager>();
        return weaponManager;
    }

    public void SelectWeapon(string id, WeaponManager weaponManager)
    {
        selectedWeapon = id;
        charInfoManager.selectWeapon(id, weaponManager);
    }

    public void disableSwordCollider()
    {
        weaponManagers[equipedWeapon].GetComponent<BoxCollider2D>().enabled = false;
        anim.ResetTrigger("noAttack");
    }

    public void UseItem()
    {
        var item = bagManager.Use();
        if (item == null) return;
        levelManager.applyItemToCharacter(item);
        item.use();
    }

    public void DropItem()
    {
        var item = bagManager.Drop();
        if (item == null) return;
        levelManager.applyItemToCharacter(item);
        item.drop();
    }

    public void SelectItem(GameObject selected)
    {
        bagManager.SelectItem(selected);
    }
}