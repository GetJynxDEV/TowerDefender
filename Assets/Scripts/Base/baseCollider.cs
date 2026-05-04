using UnityEngine;

public class BaseCollider : MonoBehaviour
{
    [SerializeField] private BaseHealth _baseHealth;
    [SerializeField] private EnemyPool _enemyPool;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Enemy enemy)) return;
        enemy.SetVictory();
        _baseHealth.TakeDamage(1);
        _enemyPool.Return(other.gameObject);
    }
}