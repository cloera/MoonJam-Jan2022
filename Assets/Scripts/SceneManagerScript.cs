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
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartGame()
    {
        GameState.SetHasGameStarted(true);
        SceneManager.LoadScene(Scene.testScene.ToString());
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.MainMenu.ToString());
    }

    public static void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
