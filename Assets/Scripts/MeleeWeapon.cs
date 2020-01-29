using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    //public float dmgToGive;
    public GameObject playerHitEffect;

    public MeleeSkeletonEnemyController meleeSkeletonEC;
        
    void Start()
    {

    }



    void Update()
    {

    }

    // damage player
    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePlayer(meleeSkeletonEC.actualDmgToGive);
            Instantiate(playerHitEffect, other.transform.position, other.transform.rotation);
        }


    }

}
