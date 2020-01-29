using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLoot : MonoBehaviour
{
    // drop bools
    public bool dropHPPotion;
    public bool dropCommonToken;
    //public bool dropHellToken, dropHeavenToken;
    
    
    
    // HP potion
    public float hpPotionDropPercent;
    public GameObject hpPotion;



    // tokens
    public GameObject hellToken, heavenToken, commonToken;
    public float commonTokenDropPercent;

    //private Transform wsPosition;




    // drop certain item (selected on GO with bool)
    public void DropItem()
    {
        float dropChance = Random.Range(0f, 100f);

        if (dropHPPotion && dropChance < hpPotionDropPercent)
        {
            float xOffset = Random.Range(-2, 3);
            float yOffset = Random.Range(-2, 3);

            Vector3 dropPosition = new Vector3((transform.position.x - xOffset), (transform.position.y - yOffset), transform.position.z);

            Instantiate(hpPotion, dropPosition, transform.rotation);
        }

        if(dropCommonToken && dropChance < commonTokenDropPercent)
        {
            float xOffset = Random.Range(-2, 3);
            float yOffset = Random.Range(-2, 3);

            Vector3 dropPosition = new Vector3((transform.position.x - xOffset), (transform.position.y - yOffset), transform.position.z);

            Instantiate(commonToken, dropPosition, transform.rotation);
        }

        
    }


    // TOKENS drop
    public void DropHellToken()
    {
         Transform wsPosition = FindObjectOfType<WanderingSoul>().transform;
        
        float xOffset = Random.Range(-1, 2);
        float yOffset = Random.Range(-1, 2);

        //Vector3 dropPosition = new Vector3((transform.position.x - xOffset), (transform.position.y - yOffset), transform.position.z);
        Vector3 wsDropPosition = new Vector3((wsPosition.position.x - xOffset), (wsPosition.position.y - yOffset), wsPosition.position.z);

        //Vector3 wsDropPosition = new Vector3((transform.position.x - xOffset), (transform.position.y - yOffset), transform.position.z);

        Instantiate(hellToken, wsDropPosition, transform.rotation);
    }
    public void DropHeavenToken()
    {
        //wsPosition.position = FindObjectOfType<WanderingSoul>().transform.position;
        Transform wsPosition = FindObjectOfType<WanderingSoul>().transform;

        float xOffset = Random.Range(-1, 2);
        float yOffset = Random.Range(-1, 2);

        Vector3 wsDropPosition = new Vector3((wsPosition.position.x - xOffset), (wsPosition.position.y - yOffset), wsPosition.position.z);

        //Vector3 wsDropPosition = new Vector3((transform.position.x - xOffset), (transform.position.y - yOffset), transform.position.z);

        Instantiate(heavenToken, wsDropPosition, transform.rotation);
    }
}
