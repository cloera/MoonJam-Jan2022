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
    [SerializeField] int longAttackDamage = 25;
    [SerializeField] int longAttackPollIntervalSeconds = 10;

    [Header("Difficulty Stuff")]
    [SerializeField] int minimumNumberOfWordsToGenerate = 1;
    [SerializeField] int maximumNumberOfWordsToGenerate = 10;
    [SerializeField] [Range(0, 100)] int percentageChanceOfStoryGeneration = 10;

    [Header("UI Stuff")]
    [SerializeField] HealthBar healthBarUI = null;
    [SerializeField] HealthBar attackBarUI = null;

    [Header("FX Stuff")]
    [SerializeField] AudioClip deathSound = null;
    [SerializeField] AudioClip quickAttackSound = null;
    [SerializeField] AudioClip longAttackSound = null;

    // Cache
    private TextGenerator textGenerator = null;
    private Player player = null;
    private AudioSource audioSource = null;

    // State
    int currentHealth;
    float secondsUntilLongAttack;
    bool isDying = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        healthBarUI.Initialize(maxHealth);
        attackBarUI.SetMaxHealth(longAttackPollIntervalSeconds);
        attackBarUI.UseHealthString(false);

        player = FindObjectOfType<Player>();
        textGenerator = FindObjectOfType<TextGenerator>();
        audioSource = GetComponent<AudioSource>();

        secondsUntilLongAttack = longAttackPollIntervalSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.GetGameIsPaused())
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
            audioSource.PlayOneShot(quickAttackSound);
        }

        if (ShouldDie())
        {
            StartCoroutine(Die());
        }

        if (!isDying)
        {
            HandleLongAttack();
        }

        healthBarUI.SetHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    private void GenerateNextPrompt()
    {
        int randomNumberOfWordsToGenerate =
            UnityEngine.Random.Range(minimumNumberOfWordsToGenerate, maximumNumberOfWordsToGenerate);

        textGenerator.GenerateNextTextPrompt(percentageChanceOfStoryGeneration, randomNumberOfWordsToGenerate);
    }

    private void HandleLongAttack()
    {
        secondsUntilLongAttack -= Time.deltaTime;

        attackBarUI.SetHealth(longAttackPollIntervalSeconds - secondsUntilLongAttack);

        if (secondsUntilLongAttack <= 0f)
        {
            audioSource.PlayOneShot(longAttackSound);
            player.TakeDamage(longAttackDamage);
            secondsUntilLongAttack = longAttackPollIntervalSeconds;
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

    private bool ShouldDie()
    {
        return currentHealth <= 0 && !isDying;
    }

    // Just load next scene.
    private IEnumerator Die()
    {
        isDying = true;

        audioSource.PlayOneShot(deathSound);

        yield return new WaitWhile(() => audioSource.isPlaying);

        SceneManagerScript.LoadNextScene();
    }
}
