using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    private Tower _tower;
    [SerializeField] private List<TowerEffect> _activeEffects = new List<TowerEffect>();

    private void Awake()
    {
        _tower = GetComponent<Tower>();
    }

    public void AddEffect(TowerEffect effect)
    {
        _activeEffects.Add(effect);
        effect.Apply(_tower.stats);
    }

    public void RemoveEffect(TowerEffect effect)
    {
        if (!_activeEffects.Contains(effect)) return;
        effect.Remove(_tower.stats);
        _activeEffects.Remove(effect);
    }

    // Call this if a level up happens, rebase all buffs onto fresh stats
    public void ReapplyAll(LevelStats baseStats)
    {
        _tower.stats = new TowerStats(baseStats);
        foreach (var effect in _activeEffects)
            effect.Apply(_tower.stats);
    }
}
