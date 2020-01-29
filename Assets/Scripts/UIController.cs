using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    
    public Slider healthSlider;
    public Text healthText;

    //public GameObject PauseUI;
    //public GameObject GameUI;

    public GameObject bossHPBar;

    public Slider bossHealthSlider;

    //public Text healthBuffText, attackBuffText;

    //public Text hellTokensText, heavenTokensText, commonTokensText;

    public Text[] commonTokenTexts, hellTokenTexts, heavenTokenTexts;

    public Text dungeonsClearedText;

    public GameObject maxedOutSize, maxedOutProjectile;

    public GameObject levelDoneUI;
    public GameObject GameUI;
    public GameObject PauseUI;
    public GameObject clickToWinUI;
    public GameObject saveOrSacrificeUI;
    public GameObject hellAltarUI, heavenAltarUI;
    public GameObject deathUI;

    public GameObject interactButtonHell, interactButtonHeaven;




    private void Awake()
    {
        instance = this;
    }

    
    void Start()
    {
        
    }

    
    
    
    
    void Update()
    {
        

        //hellTokensText.text = CharacterTracker.instance.hellTokensNo.ToString() + "x";
        //heavenTokensText.text = CharacterTracker.instance.heavenTokensNo.ToString() + "x";

        foreach(Text comTok in commonTokenTexts)
        {
            comTok.text = CharacterTracker.instance.commonTokenNo.ToString() + "x";
        }

        foreach(Text hellTok in hellTokenTexts)
        {
            hellTok.text = CharacterTracker.instance.hellTokensNo.ToString() + "x";
        }

        foreach(Text heavTok in heavenTokenTexts)
        {
            heavTok.text = CharacterTracker.instance.heavenTokensNo.ToString() + "x";
        }




        dungeonsClearedText.text = "Dungeons cleared: " + CharacterTracker.instance.levelsCompletedNo.ToString();

        /*
        if(CharacterTracker.instance.projectileCount == 3 && levelDoneUI.activeInHierarchy)
        {
            
            maxedOutProjectile.SetActive(true);
        }
        if (CharacterTracker.instance.projectileSize == 3 && levelDoneUI.activeInHierarchy)
        {

            maxedOutSize.SetActive(true);
        }
        */
    }


    

}
