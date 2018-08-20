using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMelee : MonoBehaviour {

    protected LevelManager levelManager;
    protected float swingSpeed = 10f;
    protected int damage = 25;

    public int getDamage()
    {
        return damage;
    }
}
