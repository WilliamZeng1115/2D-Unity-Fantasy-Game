using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : BaseProjectile {
   
	// Update is called once per frame
	void Update ()
    {
        transform.position += Vector3.right * xSpeed * (Time.deltaTime * 1.75f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Destory the projectile immediately upon hitting another game object
        Destroy(gameObject);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        //Destory the projectile immediately upon hitting another game object
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
