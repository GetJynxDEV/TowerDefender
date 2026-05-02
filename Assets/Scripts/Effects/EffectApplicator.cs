using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// As a Button's OnClick Event
public class EffectApplicator : MonoBehaviour
{
    [SerializeField] private EffectToApply _effectToApply;
    [SerializeField] private float _effectMagnitude;
    [SerializeField] private float _effectDuration;
    [SerializeField] private Button _button;

    private Dictionary<Tower, (Coroutine coroutine, TowerEffect effect)> _activeEffects = new();

    public void ApplyEffect()
    {
        _button.interactable = false;

        foreach (Tower tower in TowersInScene.Instance.towers)
        {
            EffectHandler handler = tower.GetComponent<EffectHandler>();
            TowerEffect newEffect = CreateEffect();

            if (_activeEffects.TryGetValue(tower, out var existing))
            {
                StopCoroutine(existing.coroutine);
                handler.RemoveEffect(existing.effect);
            }

            handler.AddEffect(newEffect);
            Coroutine coroutine = StartCoroutine(EffectTimer(tower, handler, newEffect, _effectDuration));
            _activeEffects[tower] = (coroutine, newEffect);
        }
    }

    private IEnumerator EffectTimer(Tower tower, EffectHandler handler, TowerEffect effect, float duration)
    {
        yield return new WaitForSeconds(duration);
        handler.RemoveEffect(effect);
        _activeEffects.Remove(tower);

        if (_activeEffects.Count == 0)
            _button.interactable = true;
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