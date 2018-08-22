using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Actions : MonoBehaviour
{
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
    private KeyCode moveLeftKey;
    private KeyCode moveRightKey;

    // components
    private Rigidbody2D rigidBody2D;

    // animation
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        // components
        initializeComponents();

        // movement
        initializeMovementVariables();

        // keys
        initializeKeys();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(autoMoveKey)) enableAutoMovement = !enableAutoMovement;

        if (Input.GetKeyUp(directionKey)) direction = !direction;

        move();
        // clampPosition();
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            anim.SetBool("landed", true);
            isJumping = false;
        }
    }

    // Initializers
    private void initializeComponents()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void initializeKeys()
    {
        jumpKey = KeyCode.Space;
        directionKey = KeyCode.X;
        autoMoveKey = KeyCode.Z;
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
            
        // Start animation
        anim.Play("player_movement_right");
    }

    // Helpers
    private void move()
    {
        //If flying on some object, allowed to hold space to gain height
        if (Input.GetKey(jumpKey))
        {
            anim.SetBool("landed", false);
            anim.SetBool("jump", true);
            moveVertical();
        }
        else if (Input.GetKeyUp(jumpKey))
        {
            anim.SetBool("jump", false);
        }
        if (enableAutoMovement)
        {
            var vectorDirection = direction ? Vector3.right : Vector3.left;
            moveHorizontal(vectorDirection);
        }
        else if (Input.GetKey(moveRightKey))
        {
            moveHorizontal(Vector3.right);
            anim.SetFloat("speed", Mathf.Abs(xSpeed));
        }
        else if (Input.GetKeyUp(moveRightKey))
        {
            anim.SetFloat("speed", 0);
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
}