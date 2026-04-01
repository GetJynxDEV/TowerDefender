using UnityEngine;

public class Tower_SingleTarget : Tower
{
    [Header("Attack Variables")]
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
            _rangeAttack.SpawnProjectile(_detector.enemyInRange[0].transform, stats.damage);
            _attackTimer = 1f / stats.attackSpeed;
        }
    }
}