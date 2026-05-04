using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private WaveData[] _waves;
    [SerializeField] private Transform[] _pathWaypoints;

    private int _currentWave = 0;

    private void Start()
    {
        if (_spawnPoint == null)
        {
            Debug.LogWarning("[EnemySpawner] No spawn point assigned.");
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
                yield return new WaitUntil(() => _enemyPool.CanSpawn);
                _enemyPool.Get(_spawnPoint.position, _pathWaypoints);
                yield return new WaitForSeconds(wave.timeBetweenSpawns);
            }

            yield return new WaitUntil(() => _enemyPool.AllCleared);

            _currentWave++;

            if (_currentWave < _waves.Length)
                yield return new WaitForSeconds(_waves[_currentWave - 1].timeBetweenWaves);
        }
    }
}