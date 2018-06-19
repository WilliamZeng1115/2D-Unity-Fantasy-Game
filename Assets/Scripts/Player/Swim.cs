using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : MonoBehaviour {

    private float swimForce = 500f;
    public float xSpeed = 10.0f;
    public float ySpeed = 10.0f;
    public float padding = 1.0f;
    float xmin;
    private Rigidbody2D rigidBody2D;

    public GameObject skillProjectile;

    private KeyCode swimKey;
    public bool enableMovement;
    private int direction = 1; //0 = left, 1 = right;
    public bool flyingOn = false;

    // Use this for initialization
    void Start () {
        float zdistance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zdistance));
        xmin = leftMost.x + padding;
        rigidBody2D = GetComponent<Rigidbody2D>();
        swimKey = KeyCode.Space;
        enableMovement = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(swimKey)) swimUp();

        //If flying on some object, allowed to hold space to gain height
        if (Input.GetKey(swimKey) && flyingOn) swimUp();

        if (Input.GetKeyUp(KeyCode.Z)) enableMovement = !enableMovement;
           
        if (Input.GetKeyUp(KeyCode.X))  reverseDirection();

        if (Input.GetKeyUp(KeyCode.C)) skillShootLaser();

        if (enableMovement)
        {
            if (direction == 1)
            {
                Vector3 moveRight = Vector3.right * xSpeed * Time.deltaTime;
                transform.position += moveRight;
            }
            else 
                {
                    Vector3 moveLeft = Vector3.left * xSpeed * Time.deltaTime;
                    transform.position += moveLeft;
                }
            }   
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Vector3 moveRight = Vector3.right * xSpeed * Time.deltaTime;
                transform.position += moveRight;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    Vector3 moveLeft = Vector3.left * xSpeed * Time.deltaTime;
                    transform.position += moveLeft;
                }
            }
        //float newX = Mathf.Clamp(transform.position.x, xmin, float.MaxValue);
        //transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void swimUp()
    {
        rigidBody2D.velocity = Vector2.zero;
        rigidBody2D.AddForce(new Vector2(0, swimForce));
    }

    //Make the character swap moving directions
    private void reverseDirection()
    {
        direction = 1 - direction;
        Debug.Log(direction);
    }

    //Use the equiped skill to damage enemies
    //Spawn projectile or some martial arts skill
    private void useSkill()
    {

    }

    private void skillShootLaser() {
        Instantiate(skillProjectile, transform.position + new Vector3(2f, 0, -0.001f), Quaternion.Euler(new Vector3(0, 0, -90)));
    }
}
