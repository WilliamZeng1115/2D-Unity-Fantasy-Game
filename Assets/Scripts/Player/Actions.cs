using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour {
  
    // movement
    private bool enableAutoMovement;
    private bool direction; // false = left, true = right;
    private bool enableFlying;
    private bool isJumping;

    private float jumpForce;
    private float xSpeed;
    private float ySpeed;
    // max height and width
    private float clampX;
    private float clampY;

    // keys
    private KeyCode jumpKey;
    private KeyCode directionKey;
    private KeyCode autoMoveKey;
    private KeyCode skillKey;
    private KeyCode moveLeftKey;
    private KeyCode moveRightKey;

    // components
    private ClassManager classManager;
    private Rigidbody2D rigidBody2D;

    // Use this for initialization
    void Start () {
        // movement
        initializeMovementVariables();

        // keys
        initializeKeys();

        // components
        initializeComponents();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(autoMoveKey)) enableAutoMovement = !enableAutoMovement;
           
        if (Input.GetKeyUp(directionKey))  direction = !direction;

        //if (Input.GetKeyDown(skillKey)) useSkill();
        //Delay 1 second before using skill, waiting for animation to start
        if (Input.GetKeyDown(skillKey)) Invoke("useSkill", 0.5f);

        move();
        clampPosition();
     }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground") isJumping = false;
    }

    // Initializers
    private void initializeComponents()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        classManager = (ClassManager)GetComponent<ClassManager>();
    }

    private void initializeKeys()
    {
        jumpKey = KeyCode.Space;
        directionKey = KeyCode.X;
        autoMoveKey = KeyCode.Z;
        skillKey = KeyCode.V;
        moveLeftKey = KeyCode.LeftArrow;
        moveRightKey = KeyCode.RightArrow;
    }

    private void initializeMovementVariables()
    {
        enableAutoMovement = false;
        direction = true;
        enableFlying = true;
        isJumping = false;

        jumpForce = 500.0f;
        xSpeed = 10.0f;
        ySpeed = 10.0f;

        clampX = 8;
        clampY = 8;
    }

    // Helpers
    private void move()
    {
        //If flying on some object, allowed to hold space to gain height
        if (Input.GetKey(jumpKey)) moveVertical();

        if (enableAutoMovement)
        {
            var vectorDirection = direction ? Vector3.right : Vector3.left;
            moveHorizontal(vectorDirection);
        }
        else if (Input.GetKey(moveRightKey))
        {
            moveHorizontal(Vector3.right);
        }
        else if (Input.GetKey(moveLeftKey))
        {
            moveHorizontal(Vector3.left);
        }
    }

    private void moveHorizontal(Vector3 direction)
    {
        var displacement = direction * xSpeed * Time.deltaTime;
        transform.position += displacement;
    }

    private void moveVertical()
    {
        if (enableFlying || !isJumping)
        {
            rigidBody2D.velocity = Vector2.zero;
            rigidBody2D.AddForce(new Vector2(0, jumpForce));
            isJumping = true;
        }
    }

    private void clampPosition()
    {
        var clampPosition = transform.position;
        clampPosition.y = Mathf.Clamp(clampPosition.y, -clampY, clampY);
        transform.position = clampPosition;
    }
    
    private void useSkill()
    {
        var currentClass = classManager.getClass();
        currentClass.basicAttack();
    }
}
