using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // Instance
    private static GameManagerScript _instance;
    public static GameManagerScript Instance
    {
        get
        {
            if (_instance == null)
            {
                // Load prefab from file and instantiate it in scene
                GameObject prefab = Resources.Load<GameObject>("Prefabs/GameManager");
                GameObject goInstance = Instantiate<GameObject>(prefab);
                // Assign prefab component to this instance
                _instance = goInstance.GetComponent<GameManagerScript>();
                // Add component if it does not exist
                if (!_instance)
                {
                    _instance = goInstance.AddComponent<GameManagerScript>();
                }
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    // GameObjects
    private static Player playerGO = null;
    private static Enemy enemyGO = null;

    public void Awake()
    {
        //int numInstances = FindObjectsOfType<GameManagerScript>().Length;

        //if (numInstances > 1 || (_instance != this && _instance != null))
        //{
        //    ResetGMScript();
        //}
        //else
        //{
        //    _instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
    }

    void Update()
    {
        CheckPlayerState();
    }

    private void ResetGMScript()
    {
        this.gameObject.SetActive(false);

        Destroy(this.gameObject);
    }

    #region Player
    public void ResetPlayer()
    {
        if (playerGO != null)
        {
            playerGO.ResetStats();
        }
    }

    public void DestroyPlayerGO()
    {
        if(playerGO != null)
        {
            // Destroy gameobject
            Destroy(playerGO.gameObject);
            playerGO = null;
            Debug.Log("Player object destroyed.");
        }
    }

    private void CheckPlayerState()
    {
        if (GameState.GetPlayerIsInitialized())
        {
            if (playerGO == null)
            {
                playerGO = FindObjectOfType<Player>();
            }
        }
    }
    #endregion
}
