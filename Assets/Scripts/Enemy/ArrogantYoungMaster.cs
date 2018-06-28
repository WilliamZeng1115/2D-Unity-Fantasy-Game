using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrogantYoungMaster : BaseEnemy {

	// Use this for initialization
	void Start () {
        health = 100;
        energy = 100;
        damage = 10;
        difficultyMultipler = 1;
        isBoss = false;
    }
	
	// Update is called once per frame
	void Update () {
		// move towards a platform -> fly down TODO
        
        // if see player attack with chance of using skill TODO
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "BasicAttack") {
            BaseProjectile projectileScript = other.gameObject.GetComponent<BaseProjectile>();
            
            takeDamage(projectileScript.getDamage());
        }
    }

    public override void useBasicAttack() {
        
    }

    public override void useSkill() {
    }

}
