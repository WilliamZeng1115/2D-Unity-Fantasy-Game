using UnityEngine;
using System.Collections;

public class CameraMovementScript : MonoBehaviour {
	
    public GameObject player;
    private Vector3 offset;

    // Use this for initialization
    void Start () {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate () {
        var position = transform.position;
        position.x = player.transform.position.x + offset.x;
        transform.position = position;
    }
}
