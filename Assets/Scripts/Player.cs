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


    [Header("UI Stuff")]
    [SerializeField] HealthBar healthBarUI = null;

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

        LoadEnemyAndUI();

        textGenerator = FindObjectOfType<TextGenerator>();
        audioSource = GetComponent<AudioSource>();
        screenShakeController = FindObjectOfType<ScreenShakeController>();

        GameState.SetPlayerIsInitialized(true);
    }

    // Update is called once per frame
    void Update()
    {
        if ((healthBarUI == null) || (enemy == null))
        {
            LoadEnemyAndUI();
        }

        if (GameState.GetGameIsPaused() || enemy.IsDying())
        {
            return;
        }

        healthBarUI.SetHealth(currentHealth);

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

        return result && (currentHealth != 0);
    }

    public void ResetStats()
    {
        currentHealth = maxHealth;
        healthBarUI.SetHealth(currentHealth);
        currentNumberOfMessups = 0;
        typer.ResetPrompt();
    }

    public void LoadEnemyAndUI()
    {
        enemy = FindObjectOfType<Enemy>();
        if (!GameState.GetHasBeatGame())
        {
            healthBarUI = GameObject.FindGameObjectWithTag("PlayerHPBarUI").GetComponent<HealthBar>();
        }

        healthBarUI.SetHealth(currentHealth);
        healthBarUI.SetMaxHealth(maxHealth);
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

    private IEnumerator DestroyObject()
    {
        yield return new WaitUntil(() => !GameState.GetHasGameStarted());
        GameState.SetPlayerIsInitialized(false);
        Destroy(this.gameObject);
    }
}
