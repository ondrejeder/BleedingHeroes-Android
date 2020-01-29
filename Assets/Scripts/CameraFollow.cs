using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    //public float xMin;

    
    //public float xMax;

    
    //public float yMin;

    
   // public float yMax;

    private Transform targetPlayer;


    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!targetPlayer)
        {
            targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void LateUpdate()
    {
        transform.position = new Vector3(targetPlayer.position.x, targetPlayer.position.y, transform.position.z);
    }
}
