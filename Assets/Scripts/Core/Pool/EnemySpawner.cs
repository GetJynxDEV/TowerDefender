using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform[] _pathWaypoints;

    private const int _baseEnemyCount = 5;
    private const float _baseSpawnInterval = 2f;
    private const float _baseWaveInterval = 2f;
    private const float _minInterval = 1.2f;

    private int _currentWave = 0;
    private float _spawnInterval;
    private float _waveInterval;

    private void Start()
    {
        if (_spawnPoint == null)
        {
            Debug.LogWarning("[EnemySpawner] No spawn point assigned.");
            return;
        }

        _spawnInterval = _baseSpawnInterval;
        _waveInterval = _baseWaveInterval;

        StartCoroutine(RunWaves());
    }

    private IEnumerator RunWaves()
    {
        while (true)
        {
            _currentWave++;

            if (_currentWave % 5 == 0)
            {
                _enemyPool.IncreaseMaxActive(2);
                _spawnInterval = Mathf.Max(_minInterval, _spawnInterval - 0.2f);
                _waveInterval = Mathf.Max(_minInterval, _waveInterval - 0.2f);
            }

            int enemyCount = _baseEnemyCount + (_currentWave - 1);

            for (int i = 0; i < enemyCount; i++)
            {
                yield return new WaitUntil(() => _enemyPool.CanSpawn);
                _enemyPool.Get(_spawnPoint.position, _pathWaypoints);
                yield return new WaitForSeconds(_spawnInterval);
            }

            yield return new WaitUntil(() => _enemyPool.AllCleared);
            yield return new WaitForSeconds(_waveInterval);
        }
    }
}