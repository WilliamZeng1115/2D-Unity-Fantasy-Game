using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Manager
{

    private GameObject[] bossObjects;
    private GameObject[] enemyObjects;
    private List<GameObject> bosses;
    private List<GameObject> enemies;

    // temp for now until we create an initialization so specify which folder
    // (separated by levels) to load boss and enemy from prefabs
    void Start () {
        bosses = new List<GameObject>();
        enemies = new List<GameObject>();
        loadPrefabs("Prefabs/Enemy/Sect", "Prefabs/Enemy/Boss");
    }
    
    private void loadPrefabs(string enemyFolder, string bossFolder)
    {
        bossObjects = Resources.LoadAll<GameObject>(bossFolder);
        enemyObjects = Resources.LoadAll<GameObject>(enemyFolder);
    }

    public void spawnEnemies(int num, int type, GameObject map)
    {
        //Use map width for the limiting range
        //always start spawning from left side of map, then direction of map being spawned in doesnt matter
        var spriteRenderer = map.GetComponent<SpriteRenderer>();
        var mapWidth = spriteRenderer.bounds.size.x;
        var mapHeight = spriteRenderer.bounds.size.y;
        var platforms = getPlatforms(map);

        //var mapWidth = direction ? renderer.bounds.size.x : -renderer.bounds.size.x;
        var leftCornerX = map.transform.position.x - mapWidth / 2;
        var rightCornerX = leftCornerX + mapWidth;
        var topY = map.transform.position.y + mapHeight / 2;
        
        for(var i = 0; i < num; i++)
        {
            var randomPlatformNum = Random.Range(0, platforms.Count);
            var randomPlatform = platforms[randomPlatformNum];
            var platformCollider = randomPlatform.GetComponent<BoxCollider2D>();
           
            var randXPos = Random.Range(randomPlatform.transform.position.x, randomPlatform.transform.position.x + platformCollider.size.x);
            var pos = new Vector2(randXPos, topY);
            var enemy = Instantiate(enemyObjects[type], pos, enemyObjects[type].transform.rotation);
            enemy.transform.parent = map.transform;
            enemies.Add(enemy);
        }
    }

    // stub
    public void spawnBoss(Vector2 position)
    {
        var boss = Instantiate(bossObjects[0], position, bossObjects[0].transform.rotation);
        boss.transform.parent = transform;
        bosses.Add(boss);
    }

    private List<Transform> getPlatforms(GameObject map)
    {
        var platforms = new List<Transform>();
        foreach (Transform child in map.transform)
        {
            // if child type is platform add it
            if (child.CompareTag("Ground"))
            {
                platforms.Add(child);
            }
        }
        return platforms;
    }
}
