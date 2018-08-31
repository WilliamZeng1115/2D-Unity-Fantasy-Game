using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : Manager {

    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            levelManager.SetActiveOrInActiveStageTransition(true);
        }
    }
}
