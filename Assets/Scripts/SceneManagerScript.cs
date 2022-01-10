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
            if(_instance == null)
            {
                // Load prefab from file and instantiate it in scene
                GameObject prefab = Resources.Load<GameObject>("Prefabs/SceneManager");
                GameObject goInstance = Instantiate<GameObject>(prefab);
                // Assign prefab component to this instance
                _instance = goInstance.GetComponent<SceneManagerScript>();
                // Add component if it does not exist
                if(!_instance)
                {
                    _instance = goInstance.AddComponent<SceneManagerScript>();
                }
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public void Awake()
    {
        //int numInstances = FindObjectsOfType<SceneManagerScript>().Length;

        //if (numInstances > 1 || (_instance != this && _instance != null))
        //{
        //    ResetSMScript();
        //}
        //else
        //{
        //    _instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
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
        SceneManager.LoadScene(Scene.MainMenu.ToString());

        // Destroy game objects
        GameManagerScript.Instance.DestroyPlayerGO();
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
