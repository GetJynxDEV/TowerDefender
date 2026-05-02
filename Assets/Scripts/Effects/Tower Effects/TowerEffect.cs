using UnityEngine;

[System.Serializable]
public abstract class TowerEffect
{
    public abstract void Apply(TowerStats stats);
    public abstract void Remove(TowerStats stats);
}