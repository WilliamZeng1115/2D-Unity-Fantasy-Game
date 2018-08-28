using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapManager : Manager
{

    // public GameObject rightMap, leftMap;
    public int numOfStages;
    public int numOfMapPerStage;
    public int area;
    private Dictionary<int, GameObject> stages;
    private Dictionary<int, GameObject> spawns;
    private Dictionary<int, GameObject> goals;
    // private GameObject[] maps;

    // temp for now... until we figure out how big a map we want so we can just set a default map width like
    // private float mapWidth; as a global
    // private SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
        numOfMapPerStage = 5;
        stages = new Dictionary<int, GameObject>();
        spawns = new Dictionary<int, GameObject>();
        goals = new Dictionary<int, GameObject>();
        LoadAllStage();
        // maps = Resources.LoadAll<GameObject>("Prefabs/Maps");

        // temp for now
        // renderer = maps[0].GetComponent<SpriteRenderer>();
    }

    //public GameObject createMap(bool direction) // true is right, false is left
    //{
    //    deleteMap(!direction);
    //    var currMap = direction ? leftMap : rightMap;

    //    // temp with renderer...change it to global width later -> discuss
    //    var mapWidth = direction ? renderer.bounds.size.x : -renderer.bounds.size.x;
    //    var corner = currMap.transform.position.x;
    //    var topCorner = corner + mapWidth;
    //    var position = new Vector2(topCorner, currMap.transform.position.y);

    //    var numOfMaps = maps.Length;
    //    var index = Random.Range(0, numOfMaps);

    //    var newMap = Instantiate(maps[index], position, Quaternion.identity);
    //    newMap.transform.parent = transform;
    //    if (direction)
    //    {
    //        rightMap = newMap;
    //    } else
    //    {
    //        leftMap = newMap;
    //    }
    //    return newMap;
    // }

    //public void deleteMap(bool direction) // true is right, false is left
    //{
    //    if (!direction)
    //    {
    //        if (leftMap != null) Destroy(leftMap);
    //        leftMap = rightMap;
    //    }
    //    if (direction)
    //    {
    //        if (rightMap != null) Destroy(rightMap);
    //        rightMap = leftMap;
    //    }
    //}

    // Minimum 2 maps per stage
    private void LoadStage(int stage)
    {
        var maps = Resources.LoadAll<GameObject>("Prefabs/Maps/Area-" + area + "/Stage-" +(stage + 1));
        var numOfMaps = maps.Length;

        var newStage = new GameObject();
        newStage.name = "Stage-" + (stage + 1);
        newStage.transform.parent = transform;
        stages.Add(stage, newStage);

        // Spawn map

        var topCorner = 0f;
        var mapWidth = maps[0].GetComponent<SpriteRenderer>().bounds.size.x;
        var position = new Vector2(topCorner, 0);
        var index = Random.Range(0, numOfMaps);
        var currMap = Instantiate(maps[index], position, Quaternion.identity);
        currMap.transform.parent = newStage.transform;

        var spawn = CreateGameObject(currMap);
        spawn.name = "Spawn";
        spawn.transform.parent = newStage.transform;
        spawns.Add(stage, spawn);

        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        for (var i = 0; i < numOfMapPerStage; i++)
        {
            topCorner += mapWidth;
            position = new Vector2(topCorner, 0);
            index = Random.Range(0, numOfMaps);
            currMap = Instantiate(maps[index], position, Quaternion.identity);
            currMap.transform.parent = newStage.transform;
        }

        // Goal map

        topCorner += mapWidth;
        position = new Vector2(topCorner, 0);
        index = Random.Range(0, numOfMaps);
        currMap = Instantiate(maps[index], position, Quaternion.identity);
        currMap.transform.parent = newStage.transform;

        var goal = CreateGameObject(currMap);
        goal.name = "Goal";
        goal.transform.parent = newStage.transform;
        goal.AddComponent<SpriteRenderer>();
        goal.AddComponent<BoxCollider2D>();
        goal.AddComponent<GoalManager>();
        goal.GetComponent<BoxCollider2D>().isTrigger = true;

        // temp now TODO
        var goalSprite = Resources.Load("Prefabs/Goal") as Texture2D;
        var newSprite = Sprite.Create(goalSprite, new Rect(0.0f, 0.0f, goalSprite.width, goalSprite.height), new Vector2(0.5f, 0.5f), 100.0f);
        goal.GetComponent<SpriteRenderer>().sprite = newSprite;
        // temp now TODO
        goal.GetComponent<SpriteRenderer>().sortingOrder = 5;
        goals.Add(stage, goal);
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        newStage.SetActive(false);
    }

    private void LoadAllStage()
    {
        for(var i = 0; i < numOfStages; i++)
        {
            LoadStage(i);
        }
    }

    // Fix the collision box on the platforms TODO
    private GameObject CreateGameObject(GameObject map)
    {
        var grounds = new List<GameObject>();
        foreach(Transform child in map.transform)
        {
            if (child.gameObject.tag == "Ground")
            {
                grounds.Add(child.gameObject);
            }
        }
        var index = Random.Range(0, grounds.Count);
        var newGameObject = new GameObject();

        var platform = grounds[index];
        var platformWidth = platform.GetComponent<BoxCollider2D>().size.x;
        var platformHeight = platform.GetComponent<BoxCollider2D>().size.y;

        var newPosition = platform.transform.position;
        // TODO temp with + 5
        newPosition.y = platformHeight + newPosition.y + 5;
        newPosition.x = platformWidth / 2 + newPosition.x;

        newGameObject.transform.position = newPosition;
        return newGameObject;
    }

    public void setStageActive(int stage)
    {
        stages.Where(e => e.Key != stage).Select(e => e.Value).ToList().ForEach(e => e.SetActive(false));
        stages[stage].SetActive(true);
    }

    public GameObject getStage(int stage)
    {
        return stages[stage];
    }

    public GameObject getSpawn(int stage)
    {
        return spawns[stage];
    }

    public GameObject getGoal(int stage)
    {
        return goals[stage];
    }

    public int getNumOfStages()
    {
        return numOfStages;
    }
}
