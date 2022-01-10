using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] AudioSource audioSource = null;
    [SerializeField] AudioClip bossMusic = null;
    [SerializeField] AudioClip finalBossMusic = null;

    private bool playingFinalBossMusic = false;

    void Awake()
    {
        int numberOfInstances = FindObjectsOfType<BackgroundMusic>().Length;

        if (numberOfInstances > 1)
        {
            gameObject.SetActive(false);

            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManagerScript.IsFinalBossScene() && !playingFinalBossMusic)
        {
            audioSource.Stop();
            audioSource.clip = finalBossMusic;
            audioSource.Play();
            playingFinalBossMusic = true;
        }
    }
}
