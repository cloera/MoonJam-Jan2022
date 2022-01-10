using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // GameObjects
    private PlayerManager pmInstance = null;
    private static Enemy enemyGO = null;

    // Instance
    private static GameManagerScript _instance;
    public static GameManagerScript Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
        DontDestroyOnLoad(_instance.gameObject);
    }

    void Start()
    {
        pmInstance = PlayerManager.Instance;
    }

    void Update()
    {
        
    }
}
