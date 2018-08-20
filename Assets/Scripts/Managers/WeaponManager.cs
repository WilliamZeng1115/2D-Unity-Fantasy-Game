using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponManager : Manager
{
    public string animationName;
    protected Animator anim;
    protected float atkSpeed;

    public abstract void attack();


    public void drop()
    {
        Destroy(this);
    }

    public void applyStats(float animSpeed)
    {
        atkSpeed = animSpeed / 10; //0.1 Stats Multliper
    }
}
