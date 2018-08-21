using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : WeaponManager {
    
    public GameObject projectile;
    private Transform shotPos;
    private List<GameObject> projectiles;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        shotPos = transform.Find("ShotPosition");
        stats = new Dictionary<string, float>();
        projectiles = new List<GameObject>();
    }

    public override void attack()
    {
        if (animationName != "None")
        {
            anim.Play(animationName);
        }
        newProjectile();
    }

    private void newProjectile()
    {
        var newProjectile = GameObject.Instantiate(projectile, shotPos.position, transform.parent.gameObject.transform.rotation);
        newProjectile.transform.parent = transform;
        projectiles.Add(newProjectile);
    }
}
