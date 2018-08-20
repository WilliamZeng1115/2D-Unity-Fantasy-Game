using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSword : BaseMelee {

    void Start()
    {
        damage = 100;
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        levelManager.OnCollideForObject(gameObject, other.gameObject);
    }
}
