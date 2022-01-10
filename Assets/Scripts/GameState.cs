using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState
{
    // Game state variables
    private static bool playerIsDead = false;
    private static bool playerIsInitialized = false;
    private static bool gameIsPaused = false;
    private static bool enemyIsDead = false;
    private static bool hasGameStarted = false;
    private static bool hasBeatGame = false;

    public static void SetPlayerIsDead(bool state)
    {
        playerIsDead = state;
        Debug.Log("playerIsDead set to " + state);
    }
    
    public static bool GetPlayerIsDead()
    {
        return playerIsDead;
    }

    public static void SetPlayerIsInitialized(bool state)
    {
        playerIsInitialized = state;
        Debug.Log("playerIsInitialized set to " + state);
    }

    public static bool GetPlayerIsInitialized()
    {
        return playerIsInitialized;
    }

    public static void SetGameIsPaused(bool state)
    {
        gameIsPaused = state;
        Debug.Log("gameIsPaused set to " + state);
    }

    public static bool GetGameIsPaused()
    {
        return gameIsPaused;
    }

    public static void SetEnemyIsDead(bool state)
    {
        enemyIsDead = state;
        Debug.Log("enemyIsDead set to " + state);
    }

    public static bool GetEnemyIsDead()
    {
        return enemyIsDead;
    }

    public static void SetHasGameStarted(bool state)
    {
        hasGameStarted = state;
        Debug.Log("hasGameStarted set to " + state);
    }

    public static bool GetHasGameStarted()
    {
        return hasGameStarted;
    }

    public static bool GetHasBeatGame()
    {
        return hasBeatGame;
    }

    public static void SetHasBeatGame(bool state)
    {
        hasBeatGame = state;
        Debug.Log("hasBeatGame set to " + state);
    }
}
