using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float xSpeed = 50f;
    public int damage = 25;

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

    public int getDamage()
    {
        return damage;
    }

    // Update is called once per frame
    void Update () {
        transform.position += Vector3.right * xSpeed * (Time.deltaTime* 1.75f);
    }
}
