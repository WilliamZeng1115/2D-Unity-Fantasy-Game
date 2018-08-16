using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private MapManager mapManager;

	// Use this for initialization
	void Start () {
        mapManager = GetComponentInParent(typeof(MapManager)) as MapManager;
    }

    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            if ((transform.position.x - other.transform.position.x) < 0)
            {
                mapManager.createMap(false); // create left
            }
            else if ((transform.position.x - other.transform.position.x) > 0)
            {
                mapManager.createMap(true); // create right
            }
        }
    }

    /*
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (transform.position.x < other.transform.position.x)
            {
                mapManager.createNewMap(true); // create right
            }
            else if (transform.position.x > other.transform.position.x)
            {
                mapManager.createNewMap(false); // create left
            }
        }
    }
    */

}
