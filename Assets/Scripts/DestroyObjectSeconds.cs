using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectSeconds : MonoBehaviour
{

    public float numberSeconds;
    
    void Update()
    {
        StartCoroutine("DestroyObject");
    }

    public IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(numberSeconds);

        Destroy(gameObject);
    }
}
