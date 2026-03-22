using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    public void MoveToTarget(Transform target)
    {
        if (target == null) return;
        _agent.isStopped = false;
        _agent.SetDestination(target.position);
    }

    public void StopMoving()
    {
        _agent.isStopped = true;
        _agent.ResetPath();
    }

    public void SetSpeed(float speed)
    {
        _agent.speed = speed;
    }
}