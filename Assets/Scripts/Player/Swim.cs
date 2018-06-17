using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : MonoBehaviour {

    private float swimForce = 500f;
    private Rigidbody2D rigidBody2D;

    private KeyCode swimKey;

	// Use this for initialization
	void Start () {
        rigidBody2D = GetComponent<Rigidbody2D>();
        swimKey = KeyCode.Space;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(swimKey)) swimUp();
    }

    private void swimUp()
    {
        rigidBody2D.velocity = Vector2.zero;
        rigidBody2D.AddForce(new Vector2(0, swimForce));
    }
}
