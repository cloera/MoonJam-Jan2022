using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Configs
    [SerializeField] GameObject pauseMenuUI = null;
    [SerializeField] float pauseTimeScale = 0.0f;

    // State
    private static bool gameIsPaused = false;
    private static float timeScaleBeforePause = 1.0f;

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

    /** Indicates if game is paused. */
    public static bool isPaused()
    {
        return gameIsPaused;
    }

    /** Pauses game by setting time scale back to one. */
    private void Resume()
    {
        gameIsPaused = false;

        Time.timeScale = timeScaleBeforePause;

        pauseMenuUI.SetActive(gameIsPaused);
    }

    /** Pauses game by setting time to zero. */
    private void Pause()
    {
        gameIsPaused = true;
        timeScaleBeforePause = Time.timeScale;

        Time.timeScale = pauseTimeScale;

        pauseMenuUI.SetActive(gameIsPaused);
    }
}
