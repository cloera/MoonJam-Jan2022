using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configs
    [SerializeField] int maxHealth = 100;
    [SerializeField] HealthBar healthBarUI = null;
    [SerializeField] Typer typer = null;

    // Cache
    private TextGenerator textGenerator = null;

    // State
    private int currentHealth = 0;
    private int currentNumberOfMessups = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBarUI.Initialize(maxHealth);
        textGenerator = FindObjectOfType<TextGenerator>();
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(DestroyObject());
        GameState.SetPlayerIsInitialized(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.GetGameIsPaused())
        {
            return;
        }

        // TODO: remove this test code
        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(20);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            TakeHealing(20);
        }

        healthBarUI.SetHealth(currentHealth);

        if (currentHealth <= 0 &&
            GameState.GetHasGameStarted() &&
            !GameState.GetPlayerIsDead())
        {
            GameState.SetPlayerIsDead(true);
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

    private IEnumerator DestroyObject()
    {
        yield return new WaitUntil(() => !GameState.GetHasGameStarted());
        GameState.SetPlayerIsInitialized(false);
        Destroy(this.gameObject);
    }
}
