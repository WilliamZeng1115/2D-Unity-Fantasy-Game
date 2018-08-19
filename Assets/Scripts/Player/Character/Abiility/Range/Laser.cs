using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : BaseProjectile {
   
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector2.up * xSpeed * (Time.deltaTime * 1.75f));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
