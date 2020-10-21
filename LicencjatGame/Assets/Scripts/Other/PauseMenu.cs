using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject controlsMenu;
    public GameObject Ui;
    public GameObject deathUi;
    public GameObject winUi;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Ui.SetActive(false);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Ui.SetActive(true);
        controlsMenu.SetActive(false);
    }

    public void Controls()
    {
        controlsMenu.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
    
    public void Back()
    {
        controlsMenu.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
    public void Restart()
    {
        deathUi.SetActive(false);
        winUi.SetActive(false);
        SceneManager.LoadScene("Game");
    }
    public void Death()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        deathUi.SetActive(true);
    }
    public void Win()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        winUi.SetActive(true);
    }
}
