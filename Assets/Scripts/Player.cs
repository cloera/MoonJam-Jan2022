using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configs
    [Header("Player Info")]
    [SerializeField] int maxHealth = 100;
    [SerializeField] Typer typer = null;
    [SerializeField] Enemy enemy = null;
    [SerializeField] int damageMultiplier = 1;
    [SerializeField] float attackShakePower = .05f;
    [SerializeField] float attackShakeDuration = .1f;

    [Header("FX Stuff")]
    [SerializeField] AudioClip attackSound = null;

    // Cache
    private TextGenerator textGenerator = null;
    private AudioSource audioSource = null;
    private ScreenShakeController screenShakeController = null;

    // State
    private int currentHealth = 0;
    private int currentNumberOfMessups = 0;
    private int numberOfPromptsDone = 0;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        int numberOfInstances = FindObjectsOfType<Player>().Length;

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
        currentHealth = maxHealth;

        LoadEnemy();

        textGenerator = FindObjectOfType<TextGenerator>();
        audioSource = GetComponent<AudioSource>();
        screenShakeController = FindObjectOfType<ScreenShakeController>();

        GameState.SetPlayerIsInitialized(true);
    }

    private void OnDestroy()
    {
        // Reset player state
        GameState.SetPlayerIsDead(false);
        GameState.SetPlayerIsInitialized(false);
        Debug.Log("Player state is reset");
    }

    // Update is called once per frame
    void Update()
    {
        if ((enemy == null))
        {
            LoadEnemy();
        }

        if (GameState.GetGameIsPaused() || enemy.IsDying())
        {
            return;
        }

        if (currentHealth <= 0 &&
            GameState.GetHasGameStarted() &&
            !GameState.GetPlayerIsDead())
        {
            GameState.SetPlayerIsDead(true);
        }

        if (NeedsNextPrompt())
        {
            AttackEnemy();

            numberOfPromptsDone += 1;
        }
    }

    public bool NeedsNextPrompt()
    {
        return typer.IsFinished();
    }

    public bool ShouldGetAttackedForMessUp()
    {
        bool result = currentNumberOfMessups != typer.GetNumberOfMessUps();

        currentNumberOfMessups = typer.GetNumberOfMessUps();

        return result;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void ResetStats()
    {
        currentHealth = maxHealth;
        currentNumberOfMessups = 0;
    }

    public void LoadEnemy()
    {
        enemy = FindObjectOfType<Enemy>();
        
    }

    public void TakeDamage(int damage)
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

    private void AttackEnemy()
    {
        if (enemy.IsDying())
        {
            return;
        }

        int damageToDeal = typer.GetNumberOfCharactersTyped() * damageMultiplier;

        enemy.TakeDamage(damageToDeal);

        screenShakeController.StartShaking(attackShakeDuration, attackShakePower);

        // Ignore first prompt done
        if (1 <= numberOfPromptsDone)
        {
            audioSource.PlayOneShot(attackSound);
        }
    }
}
