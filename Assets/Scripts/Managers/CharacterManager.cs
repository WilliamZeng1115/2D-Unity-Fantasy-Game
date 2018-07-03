using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{

    private BaseClass currentClass;
    private GameObject player;

    // ui stuff -> should refactor it later
    private Text healthDisplay;
    private RectTransform[] hpTransform;
    private Text scoreDisplay;

    private float currHealth, maxHealth;

    private int skillPoints, score;

    private KeyCode charInfoKey;
    private KeyCode skillKey;
    private LevelManager levelManager;

    public static CharacterManager instance;

    // temp for now -> make it enum later
    private string weapon, armor;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        currentClass = new BowMan(player);

        maxHealth = 100f; //setting full health
        currHealth = maxHealth;
        score = 0;
        skillPoints = 5;

        charInfoKey = KeyCode.LeftBracket;
        skillKey = KeyCode.V;

        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        healthDisplay = GameObject.Find("HPText").GetComponent<UnityEngine.UI.Text>();
        hpTransform = GameObject.Find("HP").GetComponents<RectTransform>();
        scoreDisplay = GameObject.Find("Score").GetComponent<UnityEngine.UI.Text>();
    }

    void Update()
    {
        if (Input.GetKeyUp(charInfoKey)) levelManager.LoadLevel("CharInfo");

        //if (Input.GetKeyDown(skillKey)) useSkill();
        //Delay 1 second before using skill, waiting for animation to start
        if (Input.GetKeyDown(skillKey)) Invoke("useSkill", 0.5f);
    }
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
    

    void OnCollisionEnter2D(Collision2D other)
    {
        // TODO temp now... shouldn't be monster but projectile tags
        if (other.gameObject.tag == "Monster")
        {
            BaseEnemy monsterScript = other.gameObject.GetComponent<BaseEnemy>();
            takeDamage(monsterScript.basicAttack());
        }
    }

    public void switchClass(BaseClass newClass)
    {
        currentClass = newClass;
    }

    public void upgradeClass()
    {
        // later when we make more class
    }

    public BaseClass getClass()
    {
        return currentClass;
    }

    // type string for now -> change to item later
    public bool isItemAvailableToClass(string item)
    {
        return true;
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

    // health related
    private void takeDamage(int damageTaken)
    {
        currHealth -= damageTaken;
        updateHealthDisplay();
        if (currHealth <= 0)
        {
            Destroy(gameObject);
            levelManager.LoadLevel("Loss");
        }
    }

    private void updateHealthDisplay()
    {
        float healthRatio = currHealth / maxHealth;
        hpTransform[0].localScale = new Vector3(1, healthRatio, 1);
        healthDisplay.text = (Mathf.Round(healthRatio * 100)) + "%";
    }

    // score related
    public void addScore(int score)
    {
        this.score += score;
        updateScoreDisplay();
    }

    public int getScore()
    {
        return score;
    }

    private void updateScoreDisplay()
    {
        scoreDisplay.text = score.ToString();
    }

    private void useSkill()
    {
        currentClass.basicAttack();
    }
}