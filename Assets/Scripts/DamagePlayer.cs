using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public float dmgToGive;

    public GameObject spikesHurtEffect;

    
    
    
    
    
    
    
    
    private void OnTriggerEnter2D(Collider2D other)   // enter the trigger zone
    {
        if(other.CompareTag("Player"))
        {

            PlayerHealthController.instance.DamagePlayer(dmgToGive);
            Instantiate (spikesHurtEffect, other.transform.position, other.transform.rotation);  // hurt effect
        }
    }

    private void OnTriggerStay2D(Collider2D other)   // stay in trigger zone
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePlayer(dmgToGive);
            Instantiate(spikesHurtEffect, other.transform.position, other.transform.rotation);    // hurt effect
        } 
    }
}

