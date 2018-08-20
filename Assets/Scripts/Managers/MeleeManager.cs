using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeManager : WeaponManager {

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override void attack()
    {
        anim.SetFloat("AttackSpeed", atkSpeed);
        anim.Play(animationName);
    }
}
