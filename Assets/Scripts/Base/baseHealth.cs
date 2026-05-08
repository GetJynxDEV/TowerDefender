using System;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public int baseHealth = 10;
    public event Action OnBaseDestroyed;

    public void TakeDamage(int damage)
    {
        baseHealth -= damage;
        Debug.Log($"Base took {damage} damage. Remaining HP: {baseHealth}");
        if (baseHealth <= 0)
        {
            baseHealth = 0;
            OnBaseDestroyed?.Invoke();
        }
    }
}