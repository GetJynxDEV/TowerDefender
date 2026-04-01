using UnityEngine;

public class TE_CritDamageBuff : TowerEffect
{
    private float _add;

    public TE_CritDamageBuff(float add)
    {
        _add = add;
    }

    public override void Apply(TowerStats stats) => stats.critDamage += _add;

    public override void Remove(TowerStats stats) => stats.critDamage -= _add;
}
