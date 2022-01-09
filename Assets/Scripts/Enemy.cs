using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Configs
    [SerializeField] int maxHealth = 100;
    [SerializeField] HealthBar healthBarUI = null;
    [SerializeField] int minimumNumberOfWordsToGenerate = 0;
    [SerializeField] int maximumNumberOfWordsToGenerate = 10;
    [SerializeField] [Range(0, 100)] int percentageChanceOfStoryGeneration = 10;

    // Cache
    private TextGenerator textGenerator = null;
    private Player player = null;

    // State
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBarUI.Initialize(maxHealth);
        player = FindObjectOfType<Player>();
        textGenerator = FindObjectOfType<TextGenerator>();

        int randomNumberOfWordsToGenerate =
            UnityEngine.Random.Range(minimumNumberOfWordsToGenerate, maximumNumberOfWordsToGenerate);

        textGenerator.GenerateNextTextPrompt(percentageChanceOfStoryGeneration, randomNumberOfWordsToGenerate);
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isPaused())
        {
            return;
        }

        // TODO: remove this test code
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(20);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            TakeHealing(20);
        }

        healthBarUI.SetHealth(currentHealth);
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    private void TakeHealing(int healing)
    {
        currentHealth += healing;

        if (maxHealth < currentHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
