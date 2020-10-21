using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controlsMenu;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void Controls() 
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Back()
    {
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    
}
