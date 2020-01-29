using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenPickup : MonoBehaviour
{
    public bool heavenToken, hellToken, commonToken;

    public GameObject heavenTPickEffect, hellTPickEffect, commonTPickEffect;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PickupAndAddToken();
        }
    }

    public void PickupAndAddToken()
    {
        if(heavenToken)
        {
            CharacterTracker.instance.heavenTokensNo++;

            Instantiate(heavenTPickEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }

        if(hellToken)
        {
            CharacterTracker.instance.hellTokensNo++;

            Instantiate(hellTPickEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }

        if (commonToken)
        {
            CharacterTracker.instance.commonTokenNo++;

            Instantiate(commonTPickEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }

}
