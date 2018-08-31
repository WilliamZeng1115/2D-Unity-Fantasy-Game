using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSword : MeleeManager {

    void Start()
    {
        damage = 100;
        if (stats == null) stats = new Dictionary<string, float>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        GetComponent<BoxCollider2D>().enabled = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("test");
        levelManager.OnCollideForObject(gameObject, other.gameObject);
    }
}
