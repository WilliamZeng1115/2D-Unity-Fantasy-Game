using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponManager : Manager
{
    public string animationName;
    protected Animator anim;

    public abstract void attack();

    public void drop()
    {
        Destroy(this);
    }
}
