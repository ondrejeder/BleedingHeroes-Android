using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerMelee : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;

    public float rangeToChasePlayer;
    private Vector3 moveDirection;
    private Vector2 relativeMoveDirection;

    public Animator theAnimator;

    public int enemyHealth = 200;

    public GameObject deathEffect;
    public GameObject[] deathStains;

    //private bool shouldShoot;
    public float distanceToAttack;
    //public GameObject enemyBullet;
    //public Transform firePoint;
    public float attackRate;
    //private float attackCounter;

    public SpriteRenderer theBody;

    public DropLoot myItemDrop;

    
    public float knockbackForce;
    public float knockbackDuration;
    private float knockbackCounter;

    public bool knockback;



    // Start is called before the first frame update
    void Start()
    {

    }







    // Update is called once per frame
    void Update()
    {



        if (theBody.isVisible  && PlayerController.instance.gameObject.activeInHierarchy)
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


            if(knockback)  // if knockaback
            {
                knockbackCounter -= Time.deltaTime;

                theRB.velocity = knockbackForce * relativeMoveDirection * -1f;

                if (knockbackCounter <= 0)
                {
                    knockback = false;
                }
                return;
            }
            else   // no knockback
            {
                theRB.velocity = moveDirection * moveSpeed;
            }

            moveDirection.Normalize();


            


            // change skeleton orientation

            if (PlayerController.instance.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1.35f, 1.35f, 1.35f);

            }
            if (PlayerController.instance.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1.35f, 1.35f, 1.35f);
            }


            // attacking
            if (Vector3.Distance(PlayerController.instance.transform.position, transform.position) < distanceToAttack)
            {
                //theAnimator.SetTrigger("attack");
                theAnimator.SetBool("isAttacking", true);
                 float moveSpeedInMelee = moveSpeed * 0.65f;          // slows the speed of enemy when attacking
                theRB.velocity = moveDirection * moveSpeedInMelee;
                
                
            }
            else
            {
                
                theAnimator.SetBool("isAttacking", false);
                theRB.velocity = moveDirection * moveSpeed;

            }
        }
        else      // if enemy visible and player not active in hierarchy
        {
            theRB.velocity = Vector3.zero;
            theAnimator.SetBool("isAttacking", false);
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
        enemyHealth -= damage;  // does amount of dmg

        Knockback();  // use knockback

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


    public void Knockback()  // knockback for when get hit
    {
        knockbackCounter = knockbackDuration;

        //theRB.velocity = new Vector2((-knockbackForce) * transform.localScale.x, (-knockbackForce) * transform.localScale.y);
        theRB.velocity = knockbackForce * relativeMoveDirection * -1f;

        knockback = true;

    }
    
}
