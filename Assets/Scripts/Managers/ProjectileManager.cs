using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : WeaponManager {
    
    public GameObject projectile;
    private Transform shootPos;
    private List<GameObject> projectiles;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        shootPos = transform.Find("ShootPosition");
        stats = new Dictionary<string, float>();
        projectiles = new List<GameObject>();
    }

    public override void attack()
    {
        base.attack();
        if (animationName != "None")
        {
            anim.Play(animationName);
        }
        newProjectile();
    }

    private void newProjectile()
    {
        var newProjectile = GameObject.Instantiate(projectile, shootPos.position, shootPos.rotation);
        newProjectile.transform.parent = transform;
        projectiles.Add(newProjectile);
    }
}
