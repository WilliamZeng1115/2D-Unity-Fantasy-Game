using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject[] bossObjects;
    public GameObject[] enemyObjects;
    public List<GameObject> bosses;
    public List<GameObject> enemies;

    // temp for now until we create an initialization so specify which folder
    // (separated by levels) to load boss and enemy from prefabs
	// Use this for initialization
	void Start () {
        bosses = new List<GameObject>();
        enemies = new List<GameObject>();
        // hardcode for now
        loadPrefabs("Prefabs/Enemy/Sect", "Prefabs/Enemy/Boss");
        spawnEnemies(3);
    }

    private void loadPrefabs(string enemy, string boss)
    {
        bossObjects = Resources.LoadAll<GameObject>(boss);
        enemyObjects = Resources.LoadAll<GameObject>(enemy);
    }

    private void spawnEnemies(int num)
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

        //Get positions a platform's box collider
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Ground"))
            {
                Vector2 platformSize = child.GetComponent<BoxCollider2D>().size;
                if (Random.Range(0, 2) == 1)
                {
                    var randXPos = Random.Range(child.transform.position.x, child.transform.position.x + platformSize.x);
                    var pos = new Vector2(randXPos, topY);
                    var enemy = Instantiate(enemyObjects[0], pos, Quaternion.identity);
                    enemy.transform.parent = transform;
                    enemies.Add(enemy);
                    Debug.Log("Newly spawned enemy position: " + enemy.transform.position);
                }
            }
        }

        /*
        for (int i = 0; i <= num; i++)
        {
            var randXPos = Random.Range(leftCornerX, rightCornerX);
            var position = new Vector2(randXPos, topY);

            // hard code which enemy to spawn for now -> should use level and progress to determine type of enemy
            var enemy = Instantiate(enemyObjects[0], position, Quaternion.identity);
            enemy.transform.parent = transform;
            enemies.Add(enemy);
        }
        */
    }

    // stub
    private void spawnBoss(Vector2 position)
    {
        var boss = Instantiate(bossObjects[0], position, Quaternion.identity);
        boss.transform.parent = transform;
        bosses.Add(boss);
    }
}
