using UnityEngine;

public class TE_CritChanceBuff : TowerEffect
{
    private float _add;

    public TE_CritChanceBuff(float add)
    {
        _add = add;
    }

    public override void Apply(TowerStats stats) => stats.critChance += _add;

    public override void Remove(TowerStats stats) => stats.critChance -= _add;
}
