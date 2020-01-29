using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    public bool hellAltar, heavenAltar;

    public GameObject noTokensText, tokenlessText, getSomeTokenText;
    private GameObject textToShow;

    //private bool textShown;

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
            //PlayerController.instance.canMove = false;

            ShowInteractButton();

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        UIController.instance.interactButtonHell.SetActive(false);
        UIController.instance.interactButtonHeaven.SetActive(false);
    }

    private void SelectText()  // select text to show if player has no tokens
    {
        int selectedText = Random.Range(1, 4);

        switch (selectedText)
        {
            case 1:
                textToShow = noTokensText;
                break;
            case 2:
                textToShow = tokenlessText;
                break;
            case 3:
                textToShow = getSomeTokenText;
                break;

        }
    }

    public void ConvertTokens()
    {
        if (CharacterTracker.instance.commonTokenNo >= 3)
        {

            if (hellAltar)
            {
                CharacterTracker.instance.commonTokenNo -= 3;

                CharacterTracker.instance.hellTokensNo++;

                Debug.Log("Hell token +");
            }

            if (heavenAltar)
            {
                CharacterTracker.instance.commonTokenNo -= 3;

                CharacterTracker.instance.heavenTokensNo++;

                Debug.Log("Heaven token +");
            }
        }
    }

    public void ShowInteractButton()
    {

        if(hellAltar)
        {
            UIController.instance.interactButtonHell.SetActive(true);
        }
        if(heavenAltar)
        {
            UIController.instance.interactButtonHeaven.SetActive(true);
        }
    }

    public void ShowAltarUI()  // attached to interact button
    {

        if (hellAltar)
        {

            if (CharacterTracker.instance.hellTokensNo > 0 || CharacterTracker.instance.commonTokenNo >= 3)   // check if player has any hell token
            {

                UIController.instance.hellAltarUI.SetActive(true);

            }
            else
            {
                SelectText();

                
                    Instantiate(textToShow, gameObject.transform.position, transform.rotation);
                    //textShown = true;
                

            }
        }

        if (heavenAltar)
        {

            if (CharacterTracker.instance.heavenTokensNo > 0 || CharacterTracker.instance.commonTokenNo >= 3)  // check if player has any heaven token 
            {

                UIController.instance.heavenAltarUI.SetActive(true);
            }
            else
            {
                SelectText();

                
                    Instantiate(textToShow, gameObject.transform.position, transform.rotation);
                    //textShown = true;
                
            }
        }
    }
}
