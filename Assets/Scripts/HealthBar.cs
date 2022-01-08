using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Config
    [SerializeField] private Slider slider = null;
    [SerializeField] TextMeshProUGUI currentHealthText = null;

    /** Initializes HealthBar UI to be at the given max health. */
    public void Initialize(int maxHealth)
    {
        SetMaxHealth(maxHealth);
        SetHealth(maxHealth);
        SetHealthString();
    }

    /** Set the current health of the HealthBar UI. */
    public void SetHealth(int health)
    {
        slider.value = health;
        SetHealthString();
    }

    /** Set the current health of the HealthBar UI. */
    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
    }

    private void SetHealthString()
    {
        currentHealthText.text = string.Format("HP: {0}/{1}", slider.value, slider.maxValue);
    }
}
