using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public enum Scene
    {
        MainMenu,
        testScene,
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        GameState.SetHasGameStarted(true);
        SceneManager.LoadScene(Scene.testScene.ToString());
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.MainMenu.ToString());

        foreach (Player player in FindObjectsOfType<Player>())
        {
            Destroy(player.gameObject);
            GameState.SetPlayerIsInitialized(false);
            GameState.SetPlayerIsDead(false);
        }
    }

    public static void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Last scene
        if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            Debug.Log("On Last Scene!");

            return;
        }

        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public static void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
