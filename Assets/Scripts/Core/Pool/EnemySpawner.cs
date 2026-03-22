using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private WaveData[] _waves;

    private int _currentWave = 0;

    private void Start()
    {
        if (_spawnPoints == null || _spawnPoints.Count == 0)
        {
            Debug.LogWarning("[EnemySpawner] No spawn points assigned.");
            return;
        }

        StartCoroutine(RunWaves());
    }

    private IEnumerator RunWaves()
    {
        while (_currentWave < _waves.Length)
        {
            WaveData wave = _waves[_currentWave];

            for (int i = 0; i < wave.enemyCount; i++)
            {
                // Wait until the pool has room before spawning the next enemy
                yield return new WaitUntil(() => _enemyPool.CanSpawn);

                Transform spawnPoint = GetRandomSpawnPoint();
                _enemyPool.Get(spawnPoint.position);

                yield return new WaitForSeconds(wave.timeBetweenSpawns);
            }

            _currentWave++;

            if (_currentWave < _waves.Length)
                yield return new WaitForSeconds(_waves[_currentWave - 1].timeBetweenWaves);
        }

        // TODO: Trigger game win condition or next level once all waves are done
    }

    private Transform GetRandomSpawnPoint()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Count)];
    }
}