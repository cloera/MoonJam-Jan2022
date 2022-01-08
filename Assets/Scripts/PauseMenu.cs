using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Configs
    [SerializeField] GameObject pauseMenuUI = null;

    // State
    private static bool gameIsPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
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
        gameIsPaused = false;

        Time.timeScale = 1f;


        pauseMenuUI.SetActive(gameIsPaused);
    }

    /** Pauses game by setting time to zero. */
    public void Pause()
    {
        gameIsPaused = true;

        Time.timeScale = 0f;

        pauseMenuUI.SetActive(gameIsPaused);
    }

    /** Loads main menu. */
    public void LoadMainMenu()
    {
        if (gameIsPaused)
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

    /** Indicates if game is paused. */
    public static bool isPaused()
    {
        return gameIsPaused;
    }
}
