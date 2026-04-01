using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private Element elementType = Element.None;
    public Element element => elementType;

    public int maxHealth;
    public int currentHealth;
    public bool isDead;

    public event Action onHit;
    public event Action onDeath;

    private void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    // Basic Damage without element consideration
    public void TakeDamage(int damage)
    {
        ApplyDamage(damage);
    }

    // Damage with element consideration
    public void TakeDamage(int damage, Element attackerElement)
    {
        ApplyDamage(DamageCalculator.ElementalDamage(elementType, attackerElement, damage));
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
