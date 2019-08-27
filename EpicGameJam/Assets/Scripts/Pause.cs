using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseUI;
    public void PauseGame()
    {
        if(!pauseUI.activeInHierarchy)
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
        }
        else
        {
            Resume();
        }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
