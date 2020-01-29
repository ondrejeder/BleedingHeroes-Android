using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public string levelToLoad;



    void Start()
    {
        



    }

    




    void Update()
    {
        



    }


    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }



    public void ExitGame()
    {
        Application.Quit();

    }
}
