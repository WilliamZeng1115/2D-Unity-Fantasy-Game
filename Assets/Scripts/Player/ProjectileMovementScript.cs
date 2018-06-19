using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovementScript : MonoBehaviour {

    public float xSpeed = 15f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 2f);
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        //Destory the projectile immediately upon hitting another game object
        Debug.Log("!!!!!");
        Destroy(gameObject);
        
    }

    void OnCollisionStay2D(Collision2D col)
    {
        //Destory the projectile immediately upon hitting another game object
        Debug.Log("!!!!!");
        Destroy(gameObject);
       
    }

    // Update is called once per frame
    void Update () {
        transform.position += Vector3.right * xSpeed * Time.deltaTime;
    }
}
