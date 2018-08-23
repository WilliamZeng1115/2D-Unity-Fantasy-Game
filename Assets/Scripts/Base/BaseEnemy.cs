using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour {

    //protected CharacterManager player;
    protected int health, energy, damage, difficultyMultipler, worth = 1;
    protected bool isBoss;
    protected LevelManager levelManager;

    // Enemy movement
    private bool direction = true;
    private float xSpeed = 3.0f;
    private float spriteWidth;
    // Weapon Manager
    protected Dictionary<string, WeaponManager> weaponManagers;
    protected WeaponManager selectedWeapon;

    public abstract void abilityAttack();
    public abstract int touchAttack();

    protected void loadWeaponManagers()
    {
        var allChild = gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in allChild)
        {
            if (child.CompareTag("WeaponManager"))
            {
                weaponManagers.Add(child.gameObject.name, child.gameObject.GetComponent<WeaponManager>());
                if (child.gameObject.activeSelf)
                {
                    selectedWeapon = child.gameObject.GetComponent<WeaponManager>();
                }
            }
        }
    }

    public int takeDamage(int damageTaken)
    {
        health -= damageTaken;
        return health;
    }

    public int getWorth()
    {
        return worth;
    }

    protected void addEnergy(int energyAdd)
    {
        energy += energyAdd;
    }

    protected void addDamage(int damageAdd)
    {
        damage += damageAdd;
    }

    protected void addHealth(int healthAdd)
    {
        health += healthAdd;
    }

    protected void enemyMovement()
    {
        if (direction)
        {
            var displacement = Vector3.right * xSpeed * Time.deltaTime;
            transform.position +=  displacement;
        } else
        {
            var displacement = Vector3.left * xSpeed * Time.deltaTime;
            transform.position += displacement;
        }
    }

    protected void changeDirections()
    {
        direction = !direction;
    }

    protected void setDirection(bool newDirection)
    {
        direction = newDirection;
    }

    void Start()
    {
        spriteWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }
    public LayerMask EnemyMask;

    protected void checkPlatformEnd()
    {

        Vector2 lineCastPos = transform.position - transform.up * spriteWidth;
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isPlatform = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, LayerMask.GetMask("Default"));

        if (!isPlatform)
        {
            changeDirections();
        }
    }

    protected void checkInAttackRange()
    {
        RaycastHit2D isInRange = Physics2D.CircleCast(transform.position, 20f, transform.position);
        if (isInRange && isInRange.transform.gameObject.tag == "Player")
        {
            if (isInRange.transform.position.x - this.transform.position.x > 0)
            {
                // Rotate enemy object
                if (transform.eulerAngles.y == 0)
                    rotateTransformY(180f);
            }
            else
            {
                if (transform.eulerAngles.y == 180)
                    rotateTransformY(-180f);
            }
            Debug.Log("In range to attack");
        }
    }

    private void rotateTransformY(float degree)
    {
        Vector3 currRotation = transform.eulerAngles;
        currRotation.y += degree;
        transform.eulerAngles = currRotation;
    }

    void FixedUpdate()
    {
        checkPlatformEnd();
        checkInAttackRange();
    }
}
