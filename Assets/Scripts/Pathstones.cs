using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathstones : MonoBehaviour
{
    

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" )
        {
            this.gameObject.SetActive(false);
        }
    }
}
