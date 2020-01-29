using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsController : MonoBehaviour
{

    

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    // SKILLS
   

     // HELL ALTAR
    public void AddProjectile()
    {

        if (CharacterTracker.instance.hellTokensNo >= 1)
        {

            CharacterTracker.instance.projectileCount++;
            CharacterTracker.instance.playerDMG *= .65f;

            CharacterTracker.instance.hellTokensNo--;

            Debug.Log("Projectile added");


            //StartCoroutine("HideAltarUI");
        }
    }

    public void IncreaseAttack()
    {
        if (CharacterTracker.instance.hellTokensNo >= 1)
        {

            CharacterTracker.instance.playerDMG *= 1.15f;

            CharacterTracker.instance.hellTokensNo--;

            Debug.Log("Attack Increased");


            //StartCoroutine("HideAltarUI");
        }
    }


    // HEAVEN ALTAR
    public void IncreaseMoveSpeed()
    {
        if (CharacterTracker.instance.heavenTokensNo >= 1)
        {

            CharacterTracker.instance.playerMoveSpeed++;

            CharacterTracker.instance.heavenTokensNo--;

            Debug.Log("MoveSpeed increased");


            //StartCoroutine("HideAltarUI");
        }
    }

    public void IncreaseHealthRegen()
    {
        if (CharacterTracker.instance.heavenTokensNo >= 1)
        {

            CharacterTracker.instance.playerHealthRegen++;
            CharacterTracker.instance.heavenTokensNo--;

            Debug.Log("HealthRegenIncreased");


            //StartCoroutine("HideAltarUI");
        }
    }
    






    public IEnumerator HideAltarUI()
    {
        yield return new WaitForSeconds(0.1f);
        
        UIController.instance.heavenAltarUI.SetActive(false);
        UIController.instance.hellAltarUI.SetActive(false);


    }
}
