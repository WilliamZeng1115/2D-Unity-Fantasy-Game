using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager {

    // none static
    private readonly GameObject projectile;
    private GameObject owner;
    private Transform shotPos;
    private List<GameObject> projectiles;
    private Vector3 offset;


    public ProjectileManager(string sprite, GameObject owner)
    {
        projectile = Resources.Load<GameObject>(sprite);
        this.owner = owner;
        shotPos = owner.transform.FindChild("ShotPosition");
        projectiles = new List<GameObject>();
    }

    public void newProjectile()
    {
        var newProjectile = GameObject.Instantiate(projectile, shotPos.position, owner.transform.rotation);
        newProjectile.transform.parent = owner.transform;
        projectiles.Add(newProjectile);
    }
}
