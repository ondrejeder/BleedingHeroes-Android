using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public float currentHealth;
    public float maxHealth;

    public GameObject deathEffectPlayer;

    public float dmgInvincLength = 1f;
    private float dmgInvincCounter;

    // maybe change to GM ???
    //public GameObject gameUI;
    //public GameObject deathUI;

    private float regenCounter;
    public float timeToRegen;

    //public SkillsController skillController;

    public float regenAmount;

    private float maxHealthDefault;
    //private float maxHealthNew;

    
    


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        maxHealth = CharacterTracker.instance.maxHealth;
        
        currentHealth = maxHealth;  // set health to max

        //maxHealthDefault = maxHealth;  

        // set values to UI slider + text
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.minValue = 0;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = "HP: " + currentHealth.ToString() + " / " + maxHealth.ToString();

        
    }

    // Update is called once per frame
    void Update()
    {
        maxHealth = CharacterTracker.instance.maxHealth;
        regenAmount = CharacterTracker.instance.playerHealthRegen;
        
        
        if (dmgInvincCounter > 0)
        {
            dmgInvincCounter -= Time.deltaTime;

            if (dmgInvincCounter <= 0)
            {
                PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, 1f);   // return alpha value to 1 if invinc counter runs out
            }
        }


        // REGENERATION
        if (currentHealth < maxHealth)
        {
            RegenerateHealth(regenAmount);
        }


        // adjust values if max health or so changes
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = "HP: " + currentHealth.ToString() + " / " + maxHealth.ToString();


    }


    public void DamagePlayer(float damage)
    {
        if (dmgInvincCounter <= 0)  // do dmg only when inv counter ran out
        {

            currentHealth -= damage;

            

            if (damage != PlayerController.instance.shootSelfDmg)  // dmg != given values then do the code
            {
                dmgInvincCounter = dmgInvincLength;  // start inv counter after taking dmg
            }

            if (damage != PlayerController.instance.shootSelfDmg)   // dmg != given values then do the code
            {
                PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, 0.5f);   // set alpha value to .5 when invincible after hit 
            }
            
            if (currentHealth <= 0f)
            {
                Instantiate(deathEffectPlayer, PlayerController.instance.transform.position, PlayerController.instance.transform.rotation);
                PlayerController.instance.gameObject.SetActive(false);
                Time.timeScale = 0.4f;
                currentHealth = 0;

                //gameUI.gameObject.SetActive(false);
                UIController.instance.GameUI.SetActive(false);
                //deathUI.gameObject.SetActive(true);
                UIController.instance.deathUI.SetActive(true);

            }

            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = "HP: " + currentHealth.ToString() + " / " + maxHealth.ToString();
        }

        
    }

    public void RegenerateHealth(float regenAmount)
    {
        regenCounter -= Time.deltaTime;

        if(regenCounter <= 0f)
        {
            currentHealth += regenAmount;

            regenCounter = timeToRegen;
        }

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = "HP: " + currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = "HP: " + currentHealth.ToString() + " / " + maxHealth.ToString();

    }




    public void IncreaseMaxHealth()
    {

        
        CharacterTracker.instance.maxHealth += 20;
        //currentHealth = maxHealth;

        Debug.Log("HealthIncreased");

    }

    

    
}
