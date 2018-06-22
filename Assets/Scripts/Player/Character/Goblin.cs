using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : BaseEnemy {

	// Use this for initialization
	void Start () {
        health = 100 + baseHealth;
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "BasicAttack") {
            Projectile projectileScript = other.gameObject.GetComponent<Projectile>();
            Debug.Log("Hit monster!");
            Debug.Log("Damage taken: " + takeDamage(projectileScript.getDamage()));
        }
    }

    public override void useBasicAtt() {
        
    }

    public override void useSkillAtt() {
    }

}
