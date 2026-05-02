using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public List<Enemy> enemyInRange = new List<Enemy>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            enemyInRange.Add(enemy);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            enemyInRange.Remove(enemy);
    }

    private void LateUpdate()
    {
        SortEnemy();
    }

    void SortEnemy()
    {
        enemyInRange.RemoveAll(enemy => enemy == null || !enemy.enabled || !enemy.gameObject.activeInHierarchy);
        enemyInRange.Sort((a, b) =>
        {
            float distA = Vector2.Distance(transform.position, a.transform.position);
            float distB = Vector2.Distance(transform.position, b.transform.position);
            return distA.CompareTo(distB);
        });
    }
}