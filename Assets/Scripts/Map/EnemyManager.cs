using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject enemyObject;
	// Use this for initialization
	void Start () {
		
	}
	
    void setEnemyPosition(GameObject newMap)
    {
        //var mapWidth = direction ? renderer.bounds.size.x : -renderer.bounds.size.x;
        var corner = newMap.transform.position.x;
        //Use map width for the limiting range
        //always start spawning from left side of map, then direction of map being spawned in doesnt matter
        var mapWidth = newMap.GetComponent<SpriteRenderer>().bounds.size.x;
        var topCorner = corner + mapWidth;

        

        for (int i = 0; i <= 3; i++)
        {
            var randXPos = Random.Range(corner, mapWidth);
            var position = new Vector2(randXPos, newMap.transform.position.y);
            spawnEnemy(position, enemyObject);
        }

        
    }

    void spawnEnemy(Vector2 position, GameObject enemyObject)
    {
        Debug.Log("Spawning enemy in pos: " + position);
        //Instantiate(enemyObject, position, Quaternion.identity);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
