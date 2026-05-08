// EnemySpawner.cs
using System.Collections;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform[] _pathWaypoints;
    [SerializeField] private TextMeshProUGUI _waveText;

    private const int _baseEnemyCount = 5;
    private const float _baseSpawnInterval = 2f;
    private const float _baseWaveInterval = 2f;
    private const float _minInterval = 1.2f;

    private int _currentWave = 0;
    private float _spawnInterval;
    private float _waveInterval;

    [Header("Enemy Scaling")]
    public float healthMultiplier = 1.2f;

    private float _currentHealthMultiplier = 1f;

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

    private void UpdateWaveText()
    {
        if (_waveText != null) _waveText.text = $"Wave {_currentWave}";
    }

    private IEnumerator RunWaves()
    {
        while (!LevelManager.Instance.isGameOver)
        {
            _currentWave++;
            UpdateWaveText();

            if (_currentWave % 5 == 0)
            {
                _enemyPool.IncreaseMaxActive(2);
                _spawnInterval = Mathf.Max(_minInterval, _spawnInterval - 0.2f);
                _waveInterval = Mathf.Max(_minInterval, _waveInterval - 0.2f);
                _currentHealthMultiplier *= healthMultiplier;
            }

            int enemyCount = _baseEnemyCount + (_currentWave - 1);

            for (int i = 0; i < enemyCount; i++)
            {
                if (LevelManager.Instance.isGameOver) yield break; 
                yield return new WaitUntil(() => _enemyPool.CanSpawn || LevelManager.Instance.isGameOver);
                if (LevelManager.Instance.isGameOver) yield break;
                _enemyPool.Get(_spawnPoint.position, _pathWaypoints, _currentHealthMultiplier);
                yield return new WaitForSeconds(_spawnInterval);
            }

            yield return new WaitUntil(() => _enemyPool.AllCleared || LevelManager.Instance.isGameOver);
            if (LevelManager.Instance.isGameOver) yield break;
            yield return new WaitForSeconds(_waveInterval);
        }
    }
}