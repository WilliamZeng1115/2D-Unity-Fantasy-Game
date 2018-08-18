using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    private GameObject spawnPoint;

    private CharacterManager characterManager;
    private MapManager mapManager;
    private EnemyManager enemyManager;
    private PopupManager popupManager;

    private KeyCode charInfoKey;
    private KeyCode skillKey;

    // Display
    private Text healthDisplay;
    private RectTransform[] hpTransform;
    private Text scoreDisplay;

    private int score;

    void Start()
    {
        // Managers
        popupManager = GameObject.Find("PopupManager").GetComponent<PopupManager>();
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
        characterManager = GameObject.Find("Player").GetComponent<CharacterManager>();

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

    void Update()
    {
        // For now - have to be a popup
        if (Input.GetKeyUp(charInfoKey)) popupManager.showhidePopup("CharInfo");

        if (Input.GetKeyDown(skillKey)) characterManager.Invoke("useSkill", 0.5f);
    }

    // can load scene with data and pass it to new scene
    public void LoadLevel(string name) {
        SceneManager.LoadScene(name);
        spawnPoint = GameObject.Find("SpawnPoint");
    }

    public void OnCollideForCharacter(GameObject character, GameObject o)
    {
        if (o.tag == "Monster")
        {
            var monsterScript = o.GetComponent<BaseEnemy>();
            var characterHealth = characterManager.takeDamage(monsterScript.basicAttack());
            updateHealthDisplay(characterHealth);
            if (characterHealth <= 0)
            {
               Destroy(character);
               LoadLevel("Loss");
            }
        }

        if (o.tag == "EnemySkill")
        {
            var monsterScript = o.GetComponent<BaseEnemy>();
            var characterHealth = characterManager.takeDamage(monsterScript.skillAttack());
            updateHealthDisplay(characterHealth);
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
                mapManager.deleteMap(false);
                if (o.tag == "RightCheckpoint")
                {
                    newMap = mapManager.createMap(true);
                }
            }
            else if ((character.transform.position.x - o.transform.position.x) > 0)
            {
                mapManager.deleteMap(true);
                if (o.tag == "LeftCheckpoint")
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

    public void OnOptionClick()
    {
        popupManager.showhidePopup("Option");
    }

    public void QuitRequest()
    {
		Application.Quit();
	}

    public GameObject getSpawnPoint()
    {
        return spawnPoint;
    }
    
    private void updateHealthDisplay(float currHealth)
    {
        var maxHealth = characterManager.getMaxHealth();
        float healthRatio = currHealth / maxHealth;
        hpTransform[0].localScale = new Vector3(1, healthRatio, 1);
        healthDisplay.text = (Mathf.Round(healthRatio * 100)) + "%";
    }

    private void updateScoreDisplay()
    {
        scoreDisplay.text = score.ToString();
    }
}
