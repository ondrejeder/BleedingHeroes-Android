using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    
   // public GameObject GameUI;
   // public GameObject PauseUI;
   // public GameObject lvlDoneUI;
   // public GameObject winClickUI;
   // public GameObject saveOrSacrificeUI;

    public string levelToLoad;

    //public float maxHealth;
    //public float playerDMG;

    private string activeScene;



    private void Awake()
    {
        instance = this;

        
    }

    
    void Start()
    {
        Application.targetFrameRate = 60;    // well, look at the line, its quite obvious...
        Time.timeScale = 1f;

        
    }

   
    void Update()
    {
        //maxHealth = PlayerHealthController.instance.maxHealth;
        //playerDMG = PlayerController.instance.dmgToGive;


    }

    public void ReloadLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;

        
        UIController.instance.PauseUI.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        UIController.instance.PauseUI.SetActive(false);
    }

    public void LevelCompleted()
    {
        Time.timeScale = 0.1f;
        CharacterTracker.instance.maxHealth = PlayerHealthController.instance.maxHealth;
        CharacterTracker.instance.playerDMG = PlayerController.instance.dmgToGive;
        CharacterTracker.instance.levelsCompletedNo++;
        CharacterTracker.instance.dungeonsClearedCounter++;
        CharacterTracker.instance.roomsClearedNo = 0;

        UIController.instance.levelDoneUI.SetActive(true);
        CharacterTracker.instance.IncreaseHealthAndAttack();
    }

    public void GoToMainmenu()
    {
        SceneManager.LoadScene("MainMenu");

    }

    public void LoadNextLevel()
    {
        
        SceneManager.LoadScene(levelToLoad);
    }


    public void CloseAltarUI()
    {
        new WaitForSeconds(0.1f);
        UIController.instance.hellAltarUI.SetActive(false);
        UIController.instance.heavenAltarUI.SetActive(false);

        PlayerController.instance.canMove = true;

    }

}
