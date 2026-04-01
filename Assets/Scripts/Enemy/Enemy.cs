using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public enum EnemyState { Chase, Death, Victory }

    public EnemyState currentState = EnemyState.Chase;
    public float deathIdleDuration = 0.6f;
    public float movementSpeed = 2f;

    protected EnemyMovement movement;
    protected EnemyPool enemyPool;
    protected Health health;

    protected virtual void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        enemyPool = FindAnyObjectByType<EnemyPool>();
        health = GetComponent<Health>();

        if (health != null)
        {
            health.onDeath += TriggerDeath;
        }
    }

    public virtual void Init(Transform[] waypoints)
    {
        if (movement == null) return;

        currentState = EnemyState.Chase;
        movement.SetSpeed(movementSpeed);
        movement.waypoints = waypoints;
    }

    public abstract void Attack();

    protected void TriggerDeath()
    {
        if (currentState == EnemyState.Death || currentState == EnemyState.Victory) return;
        currentState = EnemyState.Death;
        if (movement != null) movement.StopMoving();
        StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(deathIdleDuration);
        if (enemyPool != null) enemyPool.Return(gameObject);
    }

    public void SetVictory()
    {
        if (currentState == EnemyState.Death) return;
        currentState = EnemyState.Victory;
        if (movement != null) movement.StopMoving();
    }
}
