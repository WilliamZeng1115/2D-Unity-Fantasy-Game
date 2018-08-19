using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRay : BaseProjectile {

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * xSpeed * (Time.deltaTime * 1.75f));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag != "Monster")
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.tag != "Monster")
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
