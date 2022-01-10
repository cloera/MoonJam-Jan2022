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

    // Instance
    private static SceneManagerScript _instance = null;
    public static SceneManagerScript Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
        DontDestroyOnLoad(_instance.gameObject);
    }


    public void StartGame()
    {
        GameState.SetHasGameStarted(true);
        SceneManager.LoadScene(Scene.testScene.ToString());
        Debug.Log("Game Loaded");
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        GameState.SetHasGameStarted(false);
        SceneManager.LoadScene(Scene.MainMenu.ToString());

        foreach (BackgroundMusic backgroundMusic in FindObjectsOfType<BackgroundMusic>())
        {
            Destroy(backgroundMusic.gameObject);
        }
    }

    public void LoadNextScene()
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
        Debug.Log("Loading scene name: " + scene.ToString());
        SceneManager.LoadScene(scene.ToString());
    }
}
