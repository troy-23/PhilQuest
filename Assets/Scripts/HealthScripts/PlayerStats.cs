using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [Header("Health Settings")]
    public int maxHealth = 3;
    public int currentHealth;

    public Action onHealthChangedCallback;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ResetHealth(); // Set full health at start
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        Debug.Log($"💔 Took {amount} damage. Current health: {currentHealth}");
        onHealthChangedCallback?.Invoke();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        Debug.Log($"💖 Health fully restored to {maxHealth}");
        onHealthChangedCallback?.Invoke();
    }
}
