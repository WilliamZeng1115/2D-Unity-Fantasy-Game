using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : BaseProjectile {

    private Rigidbody2D rb;
	// Update is called once per frame

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }
	void Update ()
    {
        transform.Translate(Vector2.up * xSpeed * (Time.deltaTime * 1.75f));
        //rb.AddForce(transform.up * xSpeed);
        //transform.Translate(transform.eulerAngles * xSpeed * (Time.deltaTime * 1.75f));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
