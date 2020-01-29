using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount;

    public GameObject hpAddedEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerHealthController.instance.HealPlayer(healAmount);

            Destroy(gameObject);

            Instantiate(hpAddedEffect, other.transform.position, other.transform.rotation);
        }
    }
}
