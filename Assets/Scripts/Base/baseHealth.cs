using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public int baseHealth = 10;

    public void TakeDamage(int damage)
    {
        baseHealth -= damage;
        Debug.Log($"Base took {damage} damage. Remaining HP: {baseHealth}");
        if (baseHealth <= 0) Debug.Log("Base destroyed!");
    }
}