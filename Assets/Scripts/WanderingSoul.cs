using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class WanderingSoul : MonoBehaviour
{
    public GameObject prisoner;
    public GameObject attackText;
    public GameObject healthText;
    private GameObject someText;

    private bool moveRight, moveLeft;

    public Rigidbody2D theRB;

    private int selectedDirection;

    public float moveSpeed;

    private float timeToSwitchDir = 2;
    private float timeCounter = 1;



    void Start()
    {
        selectedDirection = Random.Range(1, 3);
        if (selectedDirection == 1)
        {
            moveRight = true;

        }
        if (selectedDirection == 2)
        {
            moveLeft = true;
        }
    }




    void Update()
    {
        if (moveRight)
        {
            theRB.velocity = Vector2.right * moveSpeed;

            timeCounter -= Time.deltaTime;

            if (timeCounter <= 0)
            {
                moveRight = false;
                moveLeft = true;
                timeCounter = timeToSwitchDir;
            }

        }
        if (moveLeft)
        {
            theRB.velocity = Vector2.left * moveSpeed;

            timeCounter -= Time.deltaTime;

            if (timeCounter <= 0)
            {
                moveRight = true;
                moveLeft = false;
                timeCounter = timeToSwitchDir;
            }
        }

        
        if (CrossPlatformInputManager.GetButton("save") || CrossPlatformInputManager.GetButton("sacrifice"))
        {
            /*
            if (CrossPlatformInputManager.GetButton("save"))
            {

                StartCoroutine("WaitAndDeactivate");
                someText = healthText;
                //Instantiate(healthText, transform.position, transform.rotation);
                StartCoroutine("PopUpText");
            }
            if (CrossPlatformInputManager.GetButton("sacrifice"))
            {

                StartCoroutine("WaitAndDeactivate");
                someText = attackText;
                //Instantiate(attackText, transform.position, transform.rotation);
                StartCoroutine("PopUpText");
            }
            */
            StartCoroutine("WaitAndDeactivate");

        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            UIController.instance.saveOrSacrificeUI.SetActive(true);
            moveSpeed = 0;



        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            UIController.instance.saveOrSacrificeUI.SetActive(false);
        }
    }

    private IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(0.15f);
        UIController.instance.saveOrSacrificeUI.SetActive(false);



        prisoner.SetActive(false);
    }

    private IEnumerator PopUpText()
    {
        yield return Instantiate(someText, transform.position, transform.rotation);
    }
}