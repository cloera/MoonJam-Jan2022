using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Class will automatically instantiate when a method is called */
public class PlayerManager : MonoBehaviour
{
    // Cache
    private Player playerGO = null;
    private HealthBar healthBarUI = null;

    // State
    private bool isHealthBarUILoaded = false;

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

        if (!isHealthBarUILoaded)
        {
            LoadHealthBarUI();
        }
        else
        {
            healthBarUI.SetHealth(playerGO.GetCurrentHealth());
        }
    }

    public void ResetPlayer()
    {
        if (playerGO != null)
        {
            playerGO.ResetStats();
            healthBarUI.SetHealth(playerGO.GetCurrentHealth());
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

    private void LoadHealthBarUI()
    {
        if(GameState.GetHasGameStarted())
        {
            // Find UI health bar in scene
            healthBarUI = GameObject.FindGameObjectWithTag("PlayerHPBarUI").GetComponent<HealthBar>();
            // Set health bar values
            healthBarUI.SetHealth(playerGO.GetCurrentHealth());
            healthBarUI.SetMaxHealth(playerGO.GetMaxHealth());
            isHealthBarUILoaded = true;

            Debug.Log("Health Bar UI has loaded");
        }
    }
}