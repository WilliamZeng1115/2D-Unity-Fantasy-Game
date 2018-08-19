﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Manager
{

    public GameObject rightMap, leftMap;
    private GameObject[] maps;

    // temp for now... until we figure out how big a map we want so we can just set a default map width like
    // private float mapWidth; as a global
    private SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
        maps = Resources.LoadAll<GameObject>("Prefabs/Maps");

        // temp for now
        renderer = maps[0].GetComponent<SpriteRenderer>();
    }

    public GameObject createMap(bool direction) // true is right, false is left
    {
        deleteMap(!direction);
        var currMap = direction ? leftMap : rightMap;

        // temp with renderer...change it to global width later -> discuss
        var mapWidth = direction ? renderer.bounds.size.x : -renderer.bounds.size.x;
        var corner = currMap.transform.position.x;
        var topCorner = corner + mapWidth;
        var position = new Vector2(topCorner, currMap.transform.position.y);

        var numOfMaps = maps.Length;
        var index = Random.Range(0, numOfMaps);

        var newMap = Instantiate(maps[index], position, Quaternion.identity);
        newMap.transform.parent = transform;
        if (direction)
        {
            rightMap = newMap;
        } else
        {
            leftMap = newMap;
        }
        return newMap;
     }

    public void deleteMap(bool direction) // true is right, false is left
    {
        if (!direction)
        {
            if (leftMap != null) Destroy(leftMap);
            leftMap = rightMap;
        }
        if (direction)
        {
            if (rightMap != null) Destroy(rightMap);
            rightMap = leftMap;
        }
    }
}
