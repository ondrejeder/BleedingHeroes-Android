using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTracker : MonoBehaviour
{
    public static CharacterTracker instance;

    public float maxHealth; 
    public float  playerDMG;
    public float playerSelfDMG;
    public float playerMoveSpeed;
    public float playerHealthRegen;

    public int attackBuffNo = 0, healthBuffNo = 0;
    public int hellTokensNo = 0, heavenTokensNo = 0, commonTokenNo = 0;

    public int levelsCompletedNo = 0;
    public int dungeonsClearedCounter = 0;

    public int projectileCount = 1;
    public float projectileSize = 1;

    public int roomsClearedNo = 0;

    

    //public LevelGenerator lvlGen;

    //public GameObject symbolCross, symbolSword;




    private void Awake()
    {
        instance = this;

        playerSelfDMG = PlayerController.instance.shootSelfDmg;
        playerDMG = PlayerController.instance.dmgToGive;
        //maxHealth = PlayerHealthController.instance.maxHealth;
        playerMoveSpeed = PlayerController.instance.moveSpeed;
        //playerHealthRegen = PlayerHealthController.instance.regenAmount;
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        //UIController.instance.attackBuffText.text = attackBuffNo.ToString() + "x";
        //UIController.instance.healthBuffText.text = healthBuffNo.ToString() + "x";

        //UIController.instance.hellTokensText.text = hellTokensNo.ToString() + "x";
        //UIController.instance.heavenTokensText.text = heavenTokensNo.ToString() + "x";
        //UIController.instance.commonTokensText.text = commonTokenNo.ToString() + "x";


        /*
        if (projectileCount >= 3)
        {
            projectileCount = 3;
        }

        if(projectileSize >= 3 )
        {
            projectileSize = 3;
        }
        */

        if (dungeonsClearedCounter == 3)
        {
            playerSelfDMG += 1;
            dungeonsClearedCounter = 0;
        }


        DontDestroyOnLoad(this.gameObject);
    }

    private void FixedUpdate()
    {
        


    }


    // increase Health + attack
    public void IncreaseHealthAndAttack()
    {
        
        playerDMG += 10;
        maxHealth += 20;

        Debug.Log("StatsIncreased");
    }


}


