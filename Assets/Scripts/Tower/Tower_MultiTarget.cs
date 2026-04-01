using UnityEngine;

public class Tower_MultiTarget : Tower
{
    [Header("Attack Variables")]
    [SerializeField] private int _numOfTarget = 3;
    [SerializeField] private RangeAttack _rangeAttack;
    [SerializeField] private Detector _detector;

    private float _attackTimer;

    private void Update()
    {
        if (!isPlaced)
            return;

        _attackTimer -= Time.deltaTime;

        if (_attackTimer <= 0f && _detector.enemyInRange.Count != 0)
        {
            AttackTargets();
            _attackTimer = 1f / stats.attackSpeed;
        }
    }

    void AttackTargets()
    {
        for (int i = 0; i < _numOfTarget; i++)
        {
            if (i < _detector.enemyInRange.Count)
            {
                _rangeAttack.SpawnProjectile(_detector.enemyInRange[i].transform, stats.damage);
            }
            else
            {
                break;
            }
        }
    }
}
