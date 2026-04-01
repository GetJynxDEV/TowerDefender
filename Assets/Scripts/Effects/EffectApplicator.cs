using System.Collections.Generic;
using System.Collections;
using UnityEngine;

// As a Button's OnClick Event
public class EffectApplicator : MonoBehaviour
{
    [SerializeField] private EffectToApply _effectToApply;
    [SerializeField] private float _effectMagnitude;
    [SerializeField] private float _effectDuration;
    [SerializeField] private List<Tower> _allTowers = new List<Tower>();

    private Dictionary<Tower, Coroutine> _activeCoroutines = new();

    public void ApplyEffect()
    {
        foreach (Tower tower in _allTowers)
        {
            EffectHandler handler = tower.GetComponent<EffectHandler>();
            TowerEffect effect = CreateEffect();

            if (_activeCoroutines.TryGetValue(tower, out Coroutine existing))
            {
                StopCoroutine(existing);
                _activeCoroutines[tower] = StartCoroutine(EffectTimer(handler, effect, _effectDuration));
            }
            else
            {
                handler.AddEffect(effect);
                _activeCoroutines[tower] = StartCoroutine(EffectTimer(handler, effect, _effectDuration));
            }
        }
    }

    private IEnumerator EffectTimer(EffectHandler handler, TowerEffect effect, float duration)
    {
        yield return new WaitForSeconds(duration);
        handler.RemoveEffect(effect);
        // Clean up the entry after expiry
        foreach (var key in _activeCoroutines.Keys)
        {
            if (_activeCoroutines[key] == null)
            {
                _activeCoroutines.Remove(key);
                break;
            }
        }
    }

    private TowerEffect CreateEffect() => _effectToApply switch
    {
        EffectToApply.DamageBuff => new TE_DamageBuff(_effectMagnitude),
        EffectToApply.AttackSpeedBuff => new TE_AttackSpeedBuff(_effectMagnitude),
        EffectToApply.CritChanceBuff => new TE_CritChanceBuff(_effectMagnitude),
        EffectToApply.CritDamageBuff => new TE_CritDamageBuff(_effectMagnitude),
        _ => null
    };
}

public enum EffectToApply { DamageBuff, AttackSpeedBuff, CritChanceBuff, CritDamageBuff }