using UnityEngine;
using System.Collections;

public class SpawnNextMapScript : MonoBehaviour {
	
	public GameObject Map1;
	float MapWidth;
	float TriggerPosX;
	// Use this for initialization
	void Start () {
		MapWidth = Map1.GetComponent<SpriteRenderer>().bounds.size.x;
		TriggerPosX = transform.position.x;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		
		Vector2 triggerPos = this.transform.position;
		Vector2 mapPos = transform.parent.position;
		Vector2 nextMapPos =  triggerPos - mapPos;
		Instantiate(Map1, new Vector2 (MapWidth + this.transform.parent.position.x, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
