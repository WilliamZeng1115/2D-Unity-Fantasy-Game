using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowMan : BaseClass {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // override
    public override int ultimate()
    {
        return 0;
    }

    // override
    public override int basicAttack()
    {
        Debug.Log("Attacked");
        // Instantiate(skillProjectile, transform.position + new Vector3(2f, 0, -0.001f), Quaternion.Euler(new Vector3(0, 0, -90)));
        return 0;
    }
}
