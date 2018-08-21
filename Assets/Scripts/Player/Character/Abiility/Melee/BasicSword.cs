using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSword : MeleeManager {

    void Start()
    {
        damage = 100;
        anim = GetComponent<Animator>();
        if (stats == null) stats = new Dictionary<string, float>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        levelManager.OnCollideForObject(gameObject, other.gameObject);
    }
}
