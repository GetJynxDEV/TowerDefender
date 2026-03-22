using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EnemyMovement))]
public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { Chase, Death, Victory }

    public EnemyState currentState = EnemyState.Chase;

    public float deathIdleDuration = 0.6f;

    private EnemyStats _stats;
    private EnemyMovement _movement;
    private EnemyPool _enemyPool;
    private Transform _baseTowerTarget;

    private void Awake()
    {
        _stats = GetComponent<EnemyStats>();
        _movement = GetComponent<EnemyMovement>();
        _enemyPool = FindAnyObjectByType<EnemyPool>();

        GameObject baseTower = GameObject.FindGameObjectWithTag("BaseTower");
        if (baseTower != null)
            _baseTowerTarget = baseTower.transform;
        else
            Debug.LogError("[EnemyAI] No GameObject tagged 'BaseTower' found.");
    }

    public void Init()
    {
        currentState = EnemyState.Chase;
        _movement.SetSpeed(_stats.movementSpeed);
        // TODO: Play Chase / Walk animation
        _movement.MoveToTarget(_baseTowerTarget);
    }

    private void Update()
    {
        if (currentState == EnemyState.Chase) _movement.MoveToTarget(_baseTowerTarget);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (currentState != EnemyState.Chase) return;

        if (other.CompareTag("BaseTower"))
        {
            // TODO: Call other.GetComponent<BaseTowerHealth>().TakeDamage(1) once BaseTower health is ready
            TriggerDeath();
        }
    }

    public void TriggerDeath()
    {
        if (currentState == EnemyState.Death || currentState == EnemyState.Victory) return;

        currentState = EnemyState.Death;
        // TODO: Play Death animation
        _movement.StopMoving();
        StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(deathIdleDuration);
        _enemyPool.Return(gameObject);
    }

    public void SetVictory()
    {
        if (currentState == EnemyState.Death) return;

        currentState = EnemyState.Victory;
        // TODO: Play Victory animation
        _movement.StopMoving();
    }
}