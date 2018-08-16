using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager {

    // none static
    private readonly GameObject projectile;
    private GameObject owner;
    private List<GameObject> projectiles;


    public ProjectileManager(string sprite, GameObject owner)
    {
        projectile = Resources.Load<GameObject>(sprite);
        this.owner = owner;
        projectiles = new List<GameObject>();
    }

    public void newProjectile()
    {
        var newProjectile = GameObject.Instantiate(projectile, owner.transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));
        newProjectile.transform.parent = owner.transform;
        projectiles.Add(newProjectile);
    }
}
