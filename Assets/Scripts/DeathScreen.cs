using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    // Configs
    [SerializeField] GameObject deathScreenUI = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Pause();
        }
    }

    /** Continue game and reset enemy stats, player stats, and text prompt. */
    public void Continue()
    {
        GameState.playerIsDead = false;

        // Reset player stats

        // Reset enemy stats

        // Reset text prompt

        Time.timeScale = 1f;

        deathScreenUI.SetActive(GameState.playerIsDead);
    }

    /** Quit to main menu */
    public void Quit()
    {
        LoadMainMenu();
    }

    /** Pauses game by setting time to zero. */
    private void Pause()
    {
        GameState.playerIsDead = true;

        Time.timeScale = 0f;

        deathScreenUI.SetActive(GameState.playerIsDead);
    }

    /** Loads main menu. */
    private void LoadMainMenu()
    {
        Debug.Log("TODO: Load Main Menu!");
    }
}
