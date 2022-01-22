using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private HealthBar healthbar;
    [SerializeField] private int startingHealth = 100;
    [SerializeField] internal int currentHealth;

    private void Start()
    {
        currentHealth = startingHealth;
        healthbar.SetMaxHealth(startingHealth);
    }

    internal void TakeDmg(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }

    internal void AddHealth(int heal)
    {
        currentHealth += heal;
        healthbar.SetHealth(currentHealth);
    }
}