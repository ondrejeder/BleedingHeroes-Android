using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    private Vector3 direction;

    //public float dmgToGive;
    public GameObject playerShotEffect;

    public ShootingSkeletonEnemyController shootingSkeletonEC;

    public float dmgToGiveBase, actualDmgToGive ;




    void Start()
    {
        dmgToGiveBase = shootingSkeletonEC.dmgToGiveBase;
        //actualDmgToGive = shootingSkeletonEC.actualDmgToGive;
        
        direction = PlayerController.instance.transform.position - transform.position;
        direction.Normalize();

        float dmgAddation = CharacterTracker.instance.levelsCompletedNo * 0.1f;  // same for dmg
        actualDmgToGive = dmgToGiveBase * (1 + dmgAddation);
    }

    




    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;




    }

    // damage player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePlayer(actualDmgToGive);   // takes dmg value from shootingskeleton script
            Instantiate(playerShotEffect, transform.position, transform.rotation);

            //Debug.Log("shot");
        }

        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
