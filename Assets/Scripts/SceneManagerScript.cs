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
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Last scene
        if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            Debug.Log("On Last Scene!");

            return;
        }

        Debug.Log("Loading scene index: " + currentSceneIndex);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadScene(Scene scene)
    {
        Debug.Log("Loading scene name: " + scene.ToString());
        SceneManager.LoadScene(scene.ToString());
    }
}
