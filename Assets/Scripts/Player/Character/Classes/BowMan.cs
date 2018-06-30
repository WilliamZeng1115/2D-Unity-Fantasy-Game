using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BowMan : BaseClass {

    private ProjectileManager projectiles;
    private GameObject player;
    private float health;
    private float maxHealth;

    // Use this for initialization
    public BowMan (GameObject player) {
        projectiles = new ProjectileManager("Prefabs/Projectile/Laser_Projectile"); // get projectilie Manager
        this.player = player;
        maxHealth = 100f; //setting full health
        health = 100f;
    }

    // override
    public override void ultimate()
    {

    }

    // override
    public override void basicAttack()
    {
        projectiles.newProjectile(player);
    }

    public override void takeDamage(int damageTaken)
    {
        health -= damageTaken;
        updateHealth();
    }

    public void updateHealth()
    {
        float healthRatio = health / maxHealth;
        Debug.Log(health);
        Debug.Log(maxHealth);
        Debug.Log(healthRatio);
        RectTransform[] currHPTransform = GameObject.Find("HP").GetComponents<RectTransform>();
        currHPTransform[0].localScale = new Vector3(1, healthRatio, 1);
        GameObject.Find("HPText").GetComponent<UnityEngine.UI.Text>().text = (Mathf.Round(healthRatio * 100)) + "%";
    }
}
