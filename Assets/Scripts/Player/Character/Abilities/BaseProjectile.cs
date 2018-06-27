using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour {

    protected float xSpeed = 50f;
    protected int damage = 25;

    public int getDamage()
    {
        return damage;
    }
}
