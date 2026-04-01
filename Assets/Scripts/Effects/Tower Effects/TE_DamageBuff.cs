using UnityEngine;

public class TE_DamageBuff : TowerEffect
{
    private float _multiplier;

    public TE_DamageBuff(float multiplier)
    {
        _multiplier = multiplier;
    }

    public override void Apply(TowerStats stats) => stats.damage = Mathf.RoundToInt(stats.damage * _multiplier);

    public override void Remove(TowerStats stats) => stats.damage = Mathf.RoundToInt(stats.damage / _multiplier);
}
