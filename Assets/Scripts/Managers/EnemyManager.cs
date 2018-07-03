using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject[] bossObjects;
    public GameObject[] enemyObjects;
    public List<GameObject> bosses;
    public List<GameObject> enemies;
    public List<Transform> platforms;

    // temp for now until we create an initialization so specify which folder
    // (separated by levels) to load boss and enemy from prefabs
    void Start () {
        bosses = new List<GameObject>();
        enemies = new List<GameObject>();
        platforms = new List<Transform>();
        loadPlatforms();
        // hardcode for now
        loadPrefabs("Prefabs/Enemy/Sect", "Prefabs/Enemy/Boss");
        spawnEnemies(3, 0);
    }

    private void loadPlatforms()
    {
        foreach (Transform child in transform)
        {
            // if child type is platform add it
            if (child.CompareTag("Ground"))
            {
                platforms.Add(child);
            }
        }
    }

    private void loadPrefabs(string enemyFolder, string bossFolder)
    {
        bossObjects = Resources.LoadAll<GameObject>(bossFolder);
        enemyObjects = Resources.LoadAll<GameObject>(enemyFolder);
    }

    private void spawnEnemies(int num, int type)
    {
        //Use map width for the limiting range
        //always start spawning from left side of map, then direction of map being spawned in doesnt matter
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var mapWidth = spriteRenderer.bounds.size.x;
        var mapHeight = spriteRenderer.bounds.size.y;

        //var mapWidth = direction ? renderer.bounds.size.x : -renderer.bounds.size.x;
        var leftCornerX = transform.position.x - mapWidth / 2;
        var rightCornerX = leftCornerX + mapWidth;
        var topY = transform.position.y + mapHeight / 2;
        
        for(var i = 0; i < num; i++)
        {
            var randomPlatformNum = Random.Range(0, platforms.Count);
            var randomPlatform = platforms[randomPlatformNum];
            var platformCollider = randomPlatform.GetComponent<BoxCollider2D>();
           
            var randXPos = Random.Range(randomPlatform.transform.position.x, randomPlatform.transform.position.x + platformCollider.size.x);
            var pos = new Vector2(randXPos, topY);
            var enemy = Instantiate(enemyObjects[type], pos, Quaternion.identity);
            enemy.transform.parent = transform;
            enemies.Add(enemy);
        }
    }

    // stub
    private void spawnBoss(Vector2 position)
    {
        var boss = Instantiate(bossObjects[0], position, Quaternion.identity);
        boss.transform.parent = transform;
        bosses.Add(boss);
    }
}
