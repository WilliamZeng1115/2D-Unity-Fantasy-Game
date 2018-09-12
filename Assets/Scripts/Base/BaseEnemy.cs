using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour {

    //protected CharacterManager player;
    protected int health, energy, damage, difficultyMultipler, score = 1, experience = 10, spiritStone = 50;
    protected bool isBoss;
    protected LevelManager levelManager;
    protected bool melee, ranged;
    // Enemy movement
    private bool direction = true;
    private float xSpeed = 3.0f;
    protected float spriteWidth;
    private Animator animator;
    protected bool disableMovement = false;
    // Weapon Manager
    protected Dictionary<string, WeaponManager> weaponManagers;
    protected WeaponManager selectedWeapon;

    protected GameObject player;

    public GameObject shootPosition;
    public LayerMask EnemyMask;

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

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public int takeDamage(int damageTaken)
    {
        health -= damageTaken;
        return health;
    }

    public int getScore()
    {
        return score;
    }

    public int getExperience()
    {
        return experience;
    }

    public int getSpiritStone()
    {
        return spiritStone;
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
        if (direction)
        {
            rotateTransformY(0);
        }
        else
        {
            rotateTransformY(180f);
        }
    }

    protected void setDirection(bool newDirection)
    {
        direction = newDirection;
    }

    protected void checkPlatformEnd()
    {
        Vector2 lineCastPos = transform.position - transform.up * spriteWidth;
        if (direction)
        {
            lineCastPos += new Vector2(spriteWidth, 0);
        } else
        {
            lineCastPos -= new Vector2(spriteWidth, 0);
        }
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down * 3);
        bool isPlatform = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down * 2, LayerMask.GetMask("Default"));
        if (!isPlatform)
        {
            if (!player)
            {
                changeDirections();
            }
            else
            {
                disableMovement = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player = col.gameObject;
            InvokeRepeating("abilityAttack", 0.3f, 2f);
        }

     }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player = null;
            CancelInvoke("abilityAttack");
            disableMovement = false;
        }
    }

    protected void checkInAttackRange()
    {
        if (player != null)
        {
            var distance = new Vector3(0, 0, 0);
            if (melee)
            {
                distance = player.transform.position - transform.transform.position;
            }
            else
            {
                distance = player.transform.position - shootPosition.transform.position;
            }

            if ((direction && distance.x < 0) || (!direction && distance.x > 0))
            {
                changeDirections();
            }

            if (ranged)
            {
                float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                shootPosition.transform.rotation = Quaternion.RotateTowards(shootPosition.transform.rotation, rotation, Time.deltaTime * 1000f);
            }
        }
    }

    private void rotateTransformY(float degree)
    {
        transform.rotation = Quaternion.Euler(0, degree, 0);
    }

    void FixedUpdate()
    {
        checkPlatformEnd();
        checkInAttackRange();
    }
}
