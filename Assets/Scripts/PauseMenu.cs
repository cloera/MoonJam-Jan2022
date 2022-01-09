using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Configs
    [SerializeField] GameObject pauseMenuUI = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameState.GetGameIsPaused())
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    /** Pauses game by setting time scale back to one. */
    public void Resume()
    {
        GameState.SetGameIsPaused(false);
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }

    /** Pauses game by setting time to zero. */
    public void Pause()
    {
        GameState.SetGameIsPaused(true);
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
    }

    /** Loads main menu. */
    public void LoadMainMenu()
    {
        if (GameState.GetGameIsPaused())
        {
            Resume();
        }

        Debug.Log("TODO: Load Main Menu!");
    }

    /** Quits game. */
    public void QuitGame()
    {
        Application.Quit();
    }
}
