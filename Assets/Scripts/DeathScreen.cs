using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] GameObject deathScreenUI = null;

    private bool isDeathScreenEnabled = false;

    // Update is called once per frame
    void Update()
    {
        if (GameState.GetPlayerIsDead() && !isDeathScreenEnabled)
        {
            EnableDeathScreen();
        }
    }

    /** Continue game and reset enemy stats, player stats, and text prompt. */
    public void Continue()
    {
        // Reset player stats
        GameManagerScript.ResetPlayer();

        // Reset enemy stats

        // Reset text prompt

        DisableDeathScreen();
    }

    /** Quit to main menu */
    public void Quit()
    {
        LoadMainMenu();
    }

    /** Loads main menu. */
    private void LoadMainMenu()
    {
        // Reset player stats

        // Reset all enemy stats

        // Reset text prompt

        GameState.SetHasGameStarted(false);
        DisableDeathScreen();
        SceneManagerScript.LoadScene(SceneManagerScript.Scene.MainMenu);
    }

    private void DisableDeathScreen()
    {
        GameState.SetPlayerIsDead(false);
        isDeathScreenEnabled = false;
        Time.timeScale = 1f;
        deathScreenUI.SetActive(false);
    }

    private void EnableDeathScreen()
    {
        GameState.SetPlayerIsDead(true);
        isDeathScreenEnabled = true;
        Time.timeScale = 0f;
        deathScreenUI.SetActive(true);
    }
}
