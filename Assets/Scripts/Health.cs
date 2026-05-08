// Health.cs
using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public Element elementType = Element.None;
    public Element element => elementType;

    public int maxHealth;
    public int currentHealth;
    public bool isDead;

    private int _baseMaxHealth;

    public event Action onHit;
    public event Action onDeath;
    public event Action onEffectiveDamage;

    private void Start()
    {
        maxHealth = 20; // Default value, can be overridden in the Inspector

        currentHealth = maxHealth;
        _baseMaxHealth = maxHealth;
    }

    private void OnEnable()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    public void SetMaxHealth(float multiplier)
    {
        maxHealth = Mathf.RoundToInt(_baseMaxHealth * multiplier);
    }

    public void TakeDamage(int damage) => ApplyDamage(damage);

    public void TakeDamage(int damage, Element attackerElement)
    {
        ApplyDamage(DamageCalculator.ElementalDamage(elementType, attackerElement, damage));
        if (DamageCalculator.IsEffective(elementType, attackerElement)) onEffectiveDamage?.Invoke();
    }

    private void ApplyDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        onHit?.Invoke();
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        onDeath?.Invoke();
        Debug.Log($"{gameObject.name} has died.");
    }
}