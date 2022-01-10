using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Class will automatically instantiate when a method is called */
public class PlayerManager : MonoBehaviour
{
    // Cache
    private Player playerGO = null;

    // Instance
    private static PlayerManager _instance = null;
    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Load prefab from file and instantiate it in scene
                GameObject prefab = Resources.Load<GameObject>("Prefabs/PlayerManager");
                GameObject goInstance = Instantiate<GameObject>(prefab);
                // Assign prefab component to this instance
                _instance = goInstance.GetComponent<PlayerManager>();
                // Add component if it does not exist
                if (!_instance)
                {
                    _instance = goInstance.AddComponent<PlayerManager>();
                }
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public PlayerManager Init()
    {
        return Instance;
    }

    void Update()
    {
        CheckPlayerState();
    }

    public void ResetPlayer()
    {
        if (playerGO != null)
        {
            playerGO.ResetStats();
        }
    }

    private void DestroyPlayerGO()
    {
        if (playerGO != null)
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
            // Is player reference set?
            if (playerGO == null)
            {
                playerGO = FindObjectOfType<Player>();
            }
            // Destroy player reference if game has not started
            if (!GameState.GetHasGameStarted())
            {
                DestroyPlayerGO();
            }
        }
    }
}