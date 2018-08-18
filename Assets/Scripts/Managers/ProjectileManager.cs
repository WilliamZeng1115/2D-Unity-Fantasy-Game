using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager {

    // none static
    private readonly GameObject projectile;
    private GameObject owner;
    private List<GameObject> projectiles;
    private Vector3 offset;


    public ProjectileManager(string sprite, GameObject owner)
    {
        projectile = Resources.Load<GameObject>(sprite);
        this.owner = owner;
        projectiles = new List<GameObject>();
        if (owner.gameObject.name == "Player") offset = Vector3.right;
        if (owner.tag == "Monster") offset = Vector3.left * 3;
    }

    public void newProjectile(bool direction = true)
    {
        var newProjectile = GameObject.Instantiate(projectile, owner.transform.position + offset, Quaternion.Euler(new Vector3(0, 0, -90)));
        newProjectile.transform.parent = owner.transform;
        if (!direction && owner.tag == "Monster")
        {
            newProjectile.GetComponent<BaseProjectile>().changeDirection();
        }
        projectiles.Add(newProjectile);
    }
}
