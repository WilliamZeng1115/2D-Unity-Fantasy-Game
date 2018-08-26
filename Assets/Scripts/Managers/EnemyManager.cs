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

    public void spawnEnemies(int num, int[] types, Transform spawn, Transform parent)
    {
        var numOfTypes = types.Length;
        for(var i = 0; i < num; i++)
        {
            var index = Random.Range(0, numOfTypes);
            var type = types[index];
            var enemy = Instantiate(enemyObjects[type], spawn.position, enemyObjects[type].transform.rotation);
            enemy.transform.parent = parent;
            enemies.Add(enemy);
        }
    }
    
    public void spawnBoss(int[] types, Transform spawn, Transform parent)
    {
        var numOfTypes = types.Length;
        for (var i = 0; i < numOfTypes; i++)
        {
            var type = types[i];
            var boss = Instantiate(bossObjects[type], spawn.position, bossObjects[0].transform.rotation);
            boss.transform.parent = parent;
            bosses.Add(boss);
        }
    }
}
