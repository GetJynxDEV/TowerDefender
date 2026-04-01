using UnityEngine;

public class TE_AttackSpeedBuff : TowerEffect
{
    private float _multiplier;

    public TE_AttackSpeedBuff(float multiplier)
    {
        _multiplier = multiplier;
    }

    public override void Apply(TowerStats stats) => stats.attackSpeed *= _multiplier;

    public override void Remove(TowerStats stats) => stats.attackSpeed /= _multiplier;
}
