using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Managers
    private Dictionary<string, Manager> managers;

    // Keys
    private KeyCode charInfoKey;
    private KeyCode skillKey;

    // Display
    private Text healthDisplay;
    private Slider hpTransform;
    private Text scoreDisplay;

    // Variables
    private int score;
    private GameObject spawnPoint;

    // Camera screen shake
    public ScreenShake screenShake;

    void Start()
    {
        // Managers
        // Load Managers by batch
        // Load specific Manager that can't be by batch
        managers = new Dictionary<string, Manager>();
        loadManagers();
        loadSpecialManagers();

        //Display
        healthDisplay = GameObject.Find("HealthText").GetComponent<UnityEngine.UI.Text>();
        hpTransform = GameObject.Find("Health").GetComponent<Slider>();
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

        if (Input.GetKeyDown(skillKey)) {
            ((CharacterManager)managers["CharacterManager"]).Invoke("useAbility", 0.5f);
            StartCoroutine(screenShake.Shake(0.4f, 0.2f));
        };
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

        // When running into/touching the enemy take damage
        if (o.tag == "Monster")
        {
            var monsterScript = o.GetComponent<BaseEnemy>();
            var characterHealth = characterManager.takeDamage(monsterScript.touchAttack());
            updateHealthDisplay(characterHealth, characterManager.getMaxHealth());
            StartCoroutine(screenShake.Shake(0.1f, 0.5f));
            if (characterHealth <= 0)
            {
               Destroy(character);
               LoadLevel("Loss");
            }
        }
        
        // one for melee and one for projectile
        if (o.tag == "EnemyRangeAttack")
        {
            var projectileScript = o.GetComponent<BaseProjectile>();
            var characterHealth = characterManager.takeDamage(projectileScript.getDamage());
            updateHealthDisplay(characterHealth, characterManager.getMaxHealth());
            StartCoroutine(screenShake.Shake(0.1f, 0.5f));
            if (characterHealth <= 0)
            {
                Destroy(character);
                LoadLevel("Loss");
            }
        }

        if (o.tag == "EnemyMeleeAttack")
        {

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
        if (o.tag == "PlayerRangeAttack")
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
        if (o.tag == "PlayerMeleeAttack")
        {
            var meleeScript = o.GetComponent<MeleeManager>();
            var enemyHealth = enemyClass.takeDamage(meleeScript.getDamage());
            if (enemyHealth <= 0)
            {
                score += enemyClass.getWorth();
                updateScoreDisplay();
                Destroy(enemy);
            }
        }
        // one for melee and one for projectile
    }

    public void OnCollideForObject(GameObject o1, GameObject o2)
    {
        if (o2.tag == "Monster")
        {
            var meleeScript = o1.GetComponent<MeleeManager>();
            var enemyClass = o2.GetComponent<BaseEnemy>();
            var enemyHealth = enemyClass.takeDamage(meleeScript.getDamage());
            if (enemyHealth <= 0)
            {
                score += enemyClass.getWorth();
                updateScoreDisplay();
                Destroy(o2);
            }
        }
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
        hpTransform.value = 1 - healthRatio;
        healthDisplay.text = (Mathf.Round(healthRatio * 100)) + "%";
    }

    private void updateScoreDisplay()
    {
        scoreDisplay.text = score.ToString();
    }
}
