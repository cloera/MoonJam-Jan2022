using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configs
    [SerializeField] int maxHealth = 100;
    [SerializeField] HealthBar healthBarUI = null;
    [SerializeField] TextGenerator textGenerator = null;

    // State
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBarUI.Initialize(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isPaused())
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
        else if (Input.GetKeyDown(KeyCode.G))
        {
            string generatedResponse = textGenerator.GenerateNextTextPrompt(40, 15);

            Debug.Log(string.Format("Last Generated: {0}", textGenerator.GetLastGeneratedPrompt()));
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
