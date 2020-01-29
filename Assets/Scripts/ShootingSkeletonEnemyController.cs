using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSkeletonEnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;

    public float rangeToChasePlayer;
    private Vector3 moveDirection;
    private Vector3 relativeMoveDirection;

    public Animator theAnimator;

    public float enemyHealthBase;
    [SerializeField] private float actualEnemyHealth;

    public GameObject deathEffect;
    public GameObject[] deathStains;

    //private bool shouldShoot;
    public float distanceToShoot;
    public GameObject enemyBullet;

    public Transform firePoint1;
    public Transform firePoint2;
    //public Transform firePoint3;
        

    public float timeToShoot;
    private float fireCounter;

    public SpriteRenderer theBody;

    public DropLoot myItemDrop;

    public float knockbackForce;
    public float knockbackDuration;
    private float knockbackCounter;

    private bool knockback;

    private bool facingR;
    private bool facingL;

    public float dmgToGiveBase;
    public float actualDmgToGive;

    public bool bossEnemy;

    


    // Start is called before the first frame update
    void Start()
    {
        //SCALING
        float healthAddition = CharacterTracker.instance.levelsCompletedNo * 0.25f;
        actualEnemyHealth = enemyHealthBase * (1 + healthAddition);

        float dmgAddation = CharacterTracker.instance.levelsCompletedNo * 0.15f;  // same for dmg
        actualDmgToGive = dmgToGiveBase * (1 + dmgAddation);




        // determines wheter enemy facing L or R
        if (transform.localScale.x < 0)
        {
            facingL = true;
        }
        if (transform.localScale.x > 0)
        {
            facingR = true;
        }

        if (bossEnemy)   // eneble boss hp bar
        {
            UIController.instance.bossHPBar.SetActive(true);
            UIController.instance.bossHealthSlider.maxValue = actualEnemyHealth;
            UIController.instance.bossHealthSlider.value = actualEnemyHealth;



        }
    }







    
    void Update()
    {






        if (theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            // setting move direction 
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;
                relativeMoveDirection = PlayerController.instance.transform.position - transform.position;

            }
            else
            {
                moveDirection = Vector3.zero;
                relativeMoveDirection = PlayerController.instance.transform.position - transform.position;
            }



            // change skeleton orientation 

            if (PlayerController.instance.transform.position.x < transform.position.x)  // player --- enemy
            {
                if (facingR)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);  // flip x value
                    facingR = false;
                    facingL = true;
                }
            }

            if (PlayerController.instance.transform.position.x > transform.position.x)   // enemy --- player
            {
                if (facingL)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);  // flip x value
                    facingL = false;
                    facingR = true;
                }
            }



            moveDirection.Normalize();

            if (knockback)
            {
                knockbackCounter -= Time.deltaTime;

                theRB.velocity = knockbackForce * relativeMoveDirection * -1f;

                if (knockbackCounter <= 0)
                {
                    knockback = false;
                }
                return;

            }
            else
            {
                theRB.velocity = moveDirection * moveSpeed;
            }



            // shooting
            if (Vector3.Distance(PlayerController.instance.transform.position, transform.position) < distanceToShoot)
            {
                fireCounter -= Time.deltaTime;

                if (fireCounter <= 0f)
                {
                    fireCounter = timeToShoot;
                    Instantiate(enemyBullet, firePoint1.position, firePoint1.rotation);
                    if (firePoint2 != null)
                    {
                        Instantiate(enemyBullet, firePoint2.position, firePoint2.rotation);
                    }
                        //Instantiate(enemyBullet, firePoint3.position, firePoint3.rotation);
                }
            }
        }
        else
        {
            theRB.velocity = Vector3.zero;
        }



        // setting animator states
        if (moveDirection != Vector3.zero)
        {
            theAnimator.SetBool("isMoving", true);
        }
        else
        {
            theAnimator.SetBool("isMoving", false);
        }



       


    }





    // HEALTH 

    public void DamageEnemy(float damage)
    {
        
        actualEnemyHealth -= damage;

        Knockback();

        if (actualEnemyHealth <= 0)
        {
            
            
            Destroy(gameObject);

            int selectedDeathStain = Random.Range(0, deathStains.Length);
            int rotation = Random.Range(0, 4);

            Instantiate(deathEffect, transform.position, transform.rotation);
            Instantiate(deathStains[selectedDeathStain], transform.position, Quaternion.Euler(0f, 0f, rotation * 90f));

            myItemDrop.DropItem();

            
        }

        if (bossEnemy)
        {

            UIController.instance.bossHealthSlider.value = actualEnemyHealth;

            if(UIController.instance.bossHealthSlider.value <= 0)
            {
                UIController.instance.bossHPBar.SetActive(false);
            }
        }
    }

    public void Knockback()
    {
        knockbackCounter = knockbackDuration;

        theRB.velocity = knockbackForce * relativeMoveDirection * -1f;

        knockback = true;
    }
}
