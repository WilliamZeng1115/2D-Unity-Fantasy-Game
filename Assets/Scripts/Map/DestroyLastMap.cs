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
        Debug.Log(gos[0].name);
        if (gos.Length > 1) Destroy(gos[0]);
    }

        // Update is called once per frame
    void Update () {
		
	}
}
