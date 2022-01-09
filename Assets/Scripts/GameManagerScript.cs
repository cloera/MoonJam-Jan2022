using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // GameObjects
    private static Player playerGO = null;

    public void Awake()
    {

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        CheckPlayerState();
    }

    public static void ResetPlayer()
    {
        if (playerGO != null)
        {
            playerGO.ResetStats();
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
            if (GameState.GetHasGameStarted() == false)
            {
                GameState.SetPlayerIsInitialized(false);
                playerGO = null;
            }
        }
    }
}
