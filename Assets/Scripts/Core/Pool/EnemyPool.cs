using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _maxActive = 5;

    private readonly Queue<GameObject> _pool = new Queue<GameObject>();
    private int _activeCount = 0;

    public bool CanSpawn => _activeCount < _maxActive;

    public GameObject Get(Vector3 position, Transform[] waypoints)
    {
        if (!CanSpawn) return null;

        GameObject enemy;

        if (_pool.Count > 0)
        {
            enemy = _pool.Dequeue();
        }
        else
        {
            enemy = Instantiate(_enemyPrefab, transform);
        }

        enemy.transform.position = position;
        enemy.SetActive(true);
        enemy.GetComponent<Enemy>().Init(waypoints);
        _activeCount++;
        return enemy;
    }

    public void Return(GameObject enemy)
    {
        enemy.SetActive(false);
        _pool.Enqueue(enemy);
        _activeCount--;
    }
}
