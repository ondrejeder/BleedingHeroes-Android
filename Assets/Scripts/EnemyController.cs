using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;

    public float rangeToChasePlayer;
    private Vector3 moveDirection;
    private Vector3 relativeMoveDirection;

    public Animator theAnimator;

    public int enemyHealth = 150;

    public GameObject deathEffect;
    public GameObject[] deathStains;

    //private bool shouldShoot;
    public float distanceToShoot;
    public GameObject enemyBullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;

    public SpriteRenderer theBody;

    public DropLoot myItemDrop;

    public float knockbackForce;
    public float knockbackDuration;
    private float knockbackCounter;

    private bool knockback;

    private bool facingR;
    private bool facingL;


    // Start is called before the first frame update
    void Start()
    {
        // determines wheter enemy facing L or R
        if (transform.localScale.x < 0)
        {
            facingL = true;
        }
        if (transform.localScale.x > 0)
        {
            facingR = true;
        }
    }







    // Update is called once per frame
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
                if(facingR)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);  // flip x value
                    facingR = false;
                    facingL = true;
                }
            }

            if (PlayerController.instance.transform.position.x > transform.position.x)   // enemy --- player
            {
                if(facingL)
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

                if(knockbackCounter <= 0 )
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
                    fireCounter = fireRate;
                    Instantiate(enemyBullet, firePoint.position, firePoint.rotation);
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


    



    public void DamageEnemy(int damage)
    {
        enemyHealth -= damage;

        Knockback();

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);

            int selectedDeathStain = Random.Range(0, deathStains.Length);
            int rotation = Random.Range(0, 4);

            Instantiate(deathEffect, transform.position, transform.rotation);
            Instantiate(deathStains[selectedDeathStain], transform.position, Quaternion.Euler(0f, 0f, rotation * 90f));

            myItemDrop.DropItem();
        }
    }

    public void Knockback()
    {
        knockbackCounter = knockbackDuration;

        theRB.velocity = knockbackForce * relativeMoveDirection * -1f;

        knockback = true;
    }

    

}
