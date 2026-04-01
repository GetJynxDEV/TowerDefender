using System.Collections.Generic;
using UnityEngine;

// Make a sphere collider as trigger and put as a child object.
public class Detector : MonoBehaviour
{
    public List<Enemy> enemyInRange = new List<Enemy>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            enemyInRange.Add(enemy);
    }

    void OnTriggerExit(Collider other)
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
        // Remove disabled enemies
        enemyInRange.RemoveAll(enemy => enemy == null || !enemy.enabled || !enemy.gameObject.activeInHierarchy);

        // Sort by 2D distance
        enemyInRange.Sort((a, b) =>
        {
            float distA = Vector2.Distance(transform.position, a.transform.position);
            float distB = Vector2.Distance(transform.position, b.transform.position);
            return distA.CompareTo(distB);
        });
    }
}