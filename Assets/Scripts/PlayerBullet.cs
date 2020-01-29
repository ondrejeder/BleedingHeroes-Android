using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;

    public Rigidbody2D theRB;

    public GameObject impactEffect;
    public GameObject enemyShotEffect;

    //public int dmgToGive;

    public Vector2 bulletDir;

    

    
    
    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    
    
    
    
    // Update is called once per frame
    void Update()
    {
        
        
        theRB.velocity = transform.right * speed;




    }


    //damage enemy
    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);

        Destroy(gameObject);

        if (other.CompareTag("Enemy"))   // impact enemy
        { 
            other.GetComponent<ShootingSkeletonEnemyController>().DamageEnemy(PlayerController.instance.dmgToGive);

            Instantiate(enemyShotEffect, transform.position, transform.rotation);
        }

        if (other.CompareTag("EnemyMelee"))    // impact melee enemy
        {
            other.GetComponent<MeleeSkeletonEnemyController>().DamageEnemy(PlayerController.instance.dmgToGive);

            Instantiate(enemyShotEffect, transform.position, transform.rotation);
            
        }
    }


    private void OnBecameInvisible()
    {
        
        Destroy(gameObject);
    }
}
