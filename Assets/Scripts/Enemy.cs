using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Configs
    [Header("Enemy Info Stuff")]
    [SerializeField] int maxHealth = 100;
    [SerializeField] int quickAttackDamage = 1;
    [SerializeField] int quickAttackPollIntervalSeconds = 1;
    [SerializeField] int longAttackDamage = 25;
    [SerializeField] int longAttackPollIntervalSeconds = 10;

    [SerializeField] float generalPollInterval = 0.5f;

    [Header("Difficulty Stuff")]
    [SerializeField] int minimumNumberOfWordsToGenerate = 1;
    [SerializeField] int maximumNumberOfWordsToGenerate = 10;
    [SerializeField] [Range(0, 100)] int percentageChanceOfStoryGeneration = 10;

    [Header("UI Stuff")]
    [SerializeField] HealthBar healthBarUI = null;

    // Cache
    private TextGenerator textGenerator = null;
    private Player player = null;

    // State
    int currentHealth;
    float secondsUntilQuickAttack;
    float secondsUntilLongAttack;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBarUI.Initialize(maxHealth);
        player = FindObjectOfType<Player>();
        textGenerator = FindObjectOfType<TextGenerator>();
        secondsUntilQuickAttack = quickAttackPollIntervalSeconds;
        secondsUntilLongAttack = longAttackPollIntervalSeconds;

        StartCoroutine(AttackCoroutine());
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

        if (player.NeedsNextPrompt())
        {
            GenerateNextPrompt();
        }

        if (player.ShouldGetAttackedForMessUp())
        {
            player.TakeDamage(quickAttackDamage);
        }

        healthBarUI.SetHealth(currentHealth);
    }

    private void GenerateNextPrompt()
    {
        int randomNumberOfWordsToGenerate =
            UnityEngine.Random.Range(minimumNumberOfWordsToGenerate, maximumNumberOfWordsToGenerate);

        textGenerator.GenerateNextTextPrompt(percentageChanceOfStoryGeneration, randomNumberOfWordsToGenerate);
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

    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            Debug.Log(string.Format("Long Attack in: {0}", secondsUntilLongAttack));

            yield return new WaitForSeconds(generalPollInterval);

            secondsUntilLongAttack -= generalPollInterval;

            if (IsZero(secondsUntilLongAttack))
            {
                player.TakeDamage(longAttackDamage);
                secondsUntilLongAttack = longAttackPollIntervalSeconds;
            }
        }
    }

    private static bool IsZero(float f)
    {
        return Mathf.Abs(f) <= 0.00001;
    }
}
