using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configs
    [SerializeField] int maxHealth = 100;
    [SerializeField] HealthBar healthBarUI = null;
    [SerializeField] Typer typer = null;
    [SerializeField] Enemy enemy = null;

    // Cache
    private TextGenerator textGenerator = null;

    // State
    private int currentHealth = 0;
    private int currentNumberOfMessups = 0;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        int numberOfGameStatusInstances = FindObjectsOfType<Player>().Length;

        if (numberOfGameStatusInstances > 1)
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
        healthBarUI.Initialize(maxHealth);
        textGenerator = FindObjectOfType<TextGenerator>();

        GameState.SetPlayerIsInitialized(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.GetGameIsPaused())
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

    public void ResetStats()
    {
        currentHealth = maxHealth;
        healthBarUI.SetHealth(currentHealth);
        currentNumberOfMessups = 0;
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
        int damageToDeal = typer.GetNumberOfCharactersTyped();

        enemy.TakeDamage(damageToDeal);
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitUntil(() => !GameState.GetHasGameStarted());
        GameState.SetPlayerIsInitialized(false);
        Destroy(this.gameObject);
    }
}
