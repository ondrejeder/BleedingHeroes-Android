using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float destroySeconds;


    void Start()
    {
        StartCoroutine("DestroyTimer"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(destroySeconds);
        //this.gameObject.SetActive(false);
        Destroy(gameObject);



    }
}
