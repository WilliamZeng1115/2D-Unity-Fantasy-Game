using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLastMap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerEnter2D(Collider2D other) {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Maps");
        if (gos.Length > 2) Destroy(gos[0]);
    }

        // Update is called once per frame
    void Update () {
		
	}
}
