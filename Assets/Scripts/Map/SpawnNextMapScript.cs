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
		//Destroy(other.gameObject);
		Debug.Log ("!!!");
		Debug.Log (MapWidth);
		Debug.Log (TriggerPosX);
		Debug.Log (MapWidth - TriggerPosX);
		Debug.Log ((MapWidth - TriggerPosX) + TriggerPosX);
		Vector2 triggerPos = this.transform.position;
		Vector2 mapPos = transform.parent.position;
		Vector2 nextMapPos =  triggerPos - mapPos;
		Debug.Log (mapPos);
		Debug.Log (nextMapPos);
		Debug.Log (MapWidth + this.transform.parent.position.x);
		//Instantiate(Map1, new Vector3((MapWidth - TriggerPosX) + TriggerPosX, transform.position.y, 0), Quaternion.identity);
		Instantiate(Map1, new Vector2 (MapWidth + this.transform.parent.position.x, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
