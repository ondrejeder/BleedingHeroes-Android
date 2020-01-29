using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBarrageProjectiles : MonoBehaviour
{
    public float speed = 10f;

    public Rigidbody2D theRB;

    public GameObject stunEffect;

    public int dmgToGive;

    private float stunCounter;
    public float stunPeriod;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.right * speed;

        stunCounter -= Time.deltaTime;

    }

    //damage enemy
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        

        
        if (other.CompareTag("Enemy"))   // impact enemy
        {
            other.GetComponent<EnemyController>().DamageEnemy(dmgToGive);
            //other.GetComponent<EnemyController>().moveSpeed = 1;

            Instantiate(stunEffect, transform.position, transform.rotation);
        }

        if (other.CompareTag("EnemyMelee"))    // impact melee enemy
        {
            other.GetComponent<EnemyControllerMelee>().DamageEnemy(dmgToGive);
            //other.GetComponent<EnemyControllerMelee>().moveSpeed = 1;

            Instantiate(stunEffect, transform.position, transform.rotation);
        }

        if (stunCounter <= 0f)
        {
            other.GetComponent<EnemyController>().moveSpeed = 3;
            other.GetComponent<EnemyControllerMelee>().moveSpeed = 3;

            stunCounter = stunPeriod;

        }

    }

    

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
