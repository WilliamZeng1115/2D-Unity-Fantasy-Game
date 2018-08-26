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
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isPlatform = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, LayerMask.GetMask("Default"));

        if (!isPlatform)
        {
            changeDirections();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && melee) {
            selectedWeapon.attack();
            Debug.Log("player entered enemy trigger");
        }
    }

    protected void checkInAttackRange()
    {
        RaycastHit2D isInRange = Physics2D.CircleCast(transform.position, 75f, new Vector2(-0.1f,-0.1f));
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, 10f, LayerMask.GetMask("Sword"));
        if (hitCollider)
        {
            Debug.Log("!!!");
        }
        if (isInRange && isInRange.transform.gameObject.tag == "Player")
        {

            var childs = transform.GetComponentsInChildren<Transform>();
            var distance = isInRange.transform.position - transform.position;
            foreach (var child in childs)
            {
                if (child.gameObject.tag == "ShootPosition")
                {
                    float xPosDiff = isInRange.transform.position.x - this.transform.position.x;
                    //if (xPosDiff > 0)
                    //{
                    //    // Rotate enemy object
                    //    if (child.eulerAngles.y == 0f)
                    //    {
                    //        child.rotation = Quaternion.Euler(0, 180f, 0);
                    //    }
                    //}
                    //else if (xPosDiff < 0)
                    //{
                    //    if (child.eulerAngles.y == 180f)
                    //    {
                    //        child.rotation = Quaternion.Euler(0, 0f, 0);
                    //    }

                    //}
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
                    //float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
                    //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    //Debug.Log(angle);
                    //child.rotation = Quaternion.RotateTowards(child.rotation, rotation, Time.deltaTime * 50f);
                    //Debug.Log("rotation shoot:" + child.eulerAngles);
                    Debug.Log(zDegree);
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
        checkPlatformEnd();
        checkInAttackRange();
    }
}
