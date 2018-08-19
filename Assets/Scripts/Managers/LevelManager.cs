using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : Manager
{
    // Managers
    private Dictionary<string, Manager> managers;

    // Keys
    private KeyCode charInfoKey;
    private KeyCode skillKey;

    // Display
    private Text healthDisplay;
    private RectTransform[] hpTransform;
    private Text scoreDisplay;

    // Variables
    private int score;
    private GameObject spawnPoint;

    void Start()
    {
        // Managers
        // Load Managers by batch
        // Load specific Manager that can't be by batch
        managers = new Dictionary<string, Manager>();
        loadManagers();
        loadSpecialManagers();

        //Display
        healthDisplay = GameObject.Find("HPText").GetComponent<UnityEngine.UI.Text>();
        hpTransform = GameObject.Find("HP").GetComponents<RectTransform>();
        scoreDisplay = GameObject.Find("Score").GetComponent<UnityEngine.UI.Text>();

        // Keys
        charInfoKey = KeyCode.LeftBracket;
        skillKey = KeyCode.V;

        // Variables
        score = 0;
        spawnPoint = GameObject.Find("SpawnPoint");
    }

    private void loadManagers()
    {
        var managerObjects = GameObject.FindGameObjectsWithTag("Manager");
        foreach (var manager in managerObjects)
        {
            managers.Add(manager.name, manager.GetComponent<Manager>());
        }
    }

    private void loadSpecialManagers()
    {
        managers.Add("CharacterManager", GameObject.Find("Player").GetComponent<CharacterManager>());
    }

    void Update()
    {
        if (Input.GetKeyUp(charInfoKey)) ((PopupManager)managers["PopupManager"]).showhidePopup("CharInfo");

        if (Input.GetKeyDown(skillKey)) ((CharacterManager)managers["CharacterManager"]).Invoke("useAbility", 0.5f);
    }

    // can load scene with data and pass it to new scene
    public void LoadLevel(string name) {
        SceneManager.LoadScene(name);
        spawnPoint = GameObject.Find("SpawnPoint");
    }

    public void OnCollideForCharacter(GameObject character, GameObject o)
    {
        var characterManager = ((CharacterManager)managers["CharacterManager"]);
        var mapManager = ((MapManager)managers["MapManager"]);
        var enemyManager = ((EnemyManager)managers["EnemyManager"]);

        if (o.tag == "Monster")
        {
            var monsterScript = o.GetComponent<BaseEnemy>();
            var characterHealth = characterManager.takeDamage(monsterScript.basicAttack());
            updateHealthDisplay(characterHealth, characterManager.getMaxHealth());
            if (characterHealth <= 0)
            {
               Destroy(character);
               LoadLevel("Loss");
            }
        }

        if (o.tag == "EnemySkill")
        {
            var projectileScript = o.GetComponent<BaseProjectile>();
            var characterHealth = characterManager.takeDamage(projectileScript.getDamage());
            updateHealthDisplay(characterHealth, characterManager.getMaxHealth());
            if (characterHealth <= 0)
            {
                Destroy(character);
                LoadLevel("Loss");
            }
        }

        if (o.tag.Contains("Checkpoint"))
        {
            GameObject newMap = null;
            
            if ((character.transform.position.x - o.transform.position.x) < 0)
            {
                if (o.tag == "Checkpoint")
                {
                    newMap = mapManager.createMap(true);
                }
            }
            else if ((character.transform.position.x - o.transform.position.x) > 0)
            {
                if (o.tag == "Checkpoint")
                {
                    newMap = mapManager.createMap(false);
                }
            }

            if (newMap != null)
            {
                enemyManager.spawnEnemies(3, 0, newMap);
            }
        }
    }

    public void OnCollideForEnemy(GameObject enemy, GameObject o, BaseEnemy enemyClass)
    {
        if (o.tag == "BasicAttack")
        {
            var projectileScript = o.GetComponent<BaseProjectile>();
            var enemyHealth = enemyClass.takeDamage(projectileScript.getDamage());
            if (enemyHealth <= 0)
            {
                score += enemyClass.getWorth();
                updateScoreDisplay();
                Destroy(enemy);
            }
        }
    }

    public void OnCollideForObject(GameObject o1, GameObject o2)
    {
    }

    public void QuitRequest()
    {
		Application.Quit();
	}

    public GameObject getSpawnPoint()
    {
        return spawnPoint;
    }
    
    private void updateHealthDisplay(float currHealth, float maxHealth)
    {
        float healthRatio = currHealth / maxHealth;
        hpTransform[0].localScale = new Vector3(1, healthRatio, 1);
        healthDisplay.text = (Mathf.Round(healthRatio * 100)) + "%";
    }

    private void updateScoreDisplay()
    {
        scoreDisplay.text = score.ToString();
    }
}
