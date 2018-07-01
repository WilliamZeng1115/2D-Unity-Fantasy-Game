using System;
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
        //transform.position += Vector3.right * Time.deltaTime * 2f;

        // if see player attack with chance of using skill TODO
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "BasicAttack") {
            BaseProjectile projectileScript = other.gameObject.GetComponent<BaseProjectile>();
            
            takeDamage(projectileScript.getDamage());
        }
    }


    public override int useBasicAttack() {
        return damage;
    }

    public override void useSkill() {
    }

    protected override void takeDamage(int damageTaken)
    {
        health -= damageTaken;
        if (health <= 0)
        {
            Destroy(gameObject);
            String currScore = GameObject.Find("Score").GetComponent<UnityEngine.UI.Text>().text;
            GameObject.Find("Score").GetComponent<UnityEngine.UI.Text>().text = (Convert.ToInt32(currScore) + 1).ToString();
        }
    }

}
