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
    private float spriteWidth;
    private Animator animator;
    // Weapon Manager
    protected Dictionary<string, WeaponManager> weaponManagers;
    protected WeaponManager selectedWeapon;

    protected GameObject player;

    public GameObject shootPosition;
    private bool targeting = false;

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
        spriteWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
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

    public LayerMask EnemyMask;

    protected void checkPlatformEnd()
    {

        Vector2 lineCastPos = transform.position - transform.up * spriteWidth;
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down * 2);
        bool isPlatform = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down * 2, LayerMask.GetMask("Default"));
        if (!isPlatform && !targeting)
        {
            changeDirections();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && melee) {
            selectedWeapon.attack();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player = col.gameObject;
            targeting = true;
            if (ranged) InvokeRepeating("abilityAttack", 0.3f, 1.0f);
        }

     }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player = null;
            targeting = false;
            if (ranged) CancelInvoke("abilityAttack");
        }
    }

    protected void checkInAttackRange()
    {
        RaycastHit2D isInRange = Physics2D.CircleCast(transform.position, 75f, new Vector2(-0.1f,-0.1f));
        if (isInRange && isInRange.transform.gameObject.tag == "Player")
        {

            var childs = transform.GetComponentsInChildren<Transform>();
            var distance = isInRange.transform.position - transform.position;
            foreach (var child in childs)
            {
                if (child.gameObject.tag == "ShootPosition")
                {
                    float xPosDiff = isInRange.transform.position.x - this.transform.position.x;
                    if ((direction && distance.x < 0) || (!direction && distance.x > 0))
                    {
                        changeDirections();
                    }
                    float hypotenuse = Mathf.Sqrt(Mathf.Pow(2, distance.x ) + Mathf.Pow(2, distance.y));
                    float sineDistance = distance.y / hypotenuse;
                    if (sineDistance > 1)
                        sineDistance = Mathf.Floor(sineDistance);
                    float zDegree = Mathf.Asin(sineDistance) * Mathf.Rad2Deg;
                    if (zDegree <= 90)
                    {
                        if (xPosDiff > 0)
                            zDegree *= -1;
                    }

                    child.rotation = Quaternion.Euler(0, 0, zDegree);
                }
            }
        }
    }

    private void rotateTransformY(float degree)
    {
        transform.rotation = Quaternion.Euler(0, degree, 0);
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            var distance = new Vector3(0,0,0);
            if (melee) {
                distance = player.transform.position - transform.transform.position;
            } else
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
                //Debug.Log(angle);
                shootPosition.transform.rotation = Quaternion.RotateTowards(shootPosition.transform.rotation, rotation, Time.deltaTime * 1000f);
            }
        }
        checkPlatformEnd();
        // checkInAttackRange();
    }
}
