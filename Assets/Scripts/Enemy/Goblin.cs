using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : BaseEnemy {

	// Use this for initialization
	void Start () {
        health = 100 + baseHealth;
        Destroy(gameObject, 5f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        /*if (other.gameObject.tag == "BasicAttack") {
            Projectile projectileScript = other.gameObject.GetComponent<Projectile>();
            
            
            if (health - projectileScript.getDamage() <= 0)
            {
                Destroy(gameObject);
                Debug.Log(health);
            } else
            {
                Debug.Log("Damage taken: " + takeDamage(projectileScript.getDamage()));
                Debug.Log(health);
            }
        }*/
    }

    public override void useBasicAtt() {
        
    }

    public override void useSkillAtt() {
    }

}
