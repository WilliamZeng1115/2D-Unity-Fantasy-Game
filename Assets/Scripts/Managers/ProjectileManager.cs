using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour {

    // none static
    public readonly GameObject projectile;

    private List<GameObject> projectiles;

    public ProjectileManager(string sprite)
    {
        projectile = Resources.Load<GameObject>(sprite);
        projectiles = new List<GameObject>();
    }

    public void newProjectile(GameObject player)
    {
        var newProjectile = Instantiate(projectile, player.transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));
        newProjectile.transform.parent = player.transform;
        projectiles.Add(newProjectile);
    }
}
