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

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.MainMenu.ToString());

        foreach (Player player in FindObjectsOfType<Player>())
        {
            Destroy(player.gameObject);
            GameState.SetPlayerIsInitialized(false);
            GameState.SetPlayerIsDead(false);
        }

        foreach (BackgroundMusic backgroundMusic in FindObjectsOfType<BackgroundMusic>())
        {
            Destroy(backgroundMusic.gameObject);
        }
    }

    public static void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public static bool IsFinalBossScene()
    {
        return SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 2;
    }

    public static void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
