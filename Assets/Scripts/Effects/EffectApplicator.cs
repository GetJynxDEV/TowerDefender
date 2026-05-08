using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EffectApplicator : MonoBehaviour
{
    [SerializeField] private EffectToApply _effectToApply;
    [SerializeField] private float _effectMagnitude;
    [SerializeField] private float _effectDuration;
    [SerializeField] private float _cooldownDuration;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _cooldownText;
    [SerializeField] private Color _activeEffectColor = Color.green; // Set in Inspector

    private Dictionary<Tower, (Coroutine coroutine, TowerEffect effect)> _activeEffects = new();
    private Coroutine _cooldownCoroutine;
    private Color _defaultColor;

    private void Start()
    {
        _defaultColor = _button.image.color;
    }

    public void ApplyEffect()
    {
        if (TowersInScene.Instance.towers.Count == 0)
            return;
        _button.interactable = false;
        _button.image.color = _activeEffectColor;

        if (_cooldownCoroutine != null)
            StopCoroutine(_cooldownCoroutine);
        _cooldownCoroutine = StartCoroutine(CooldownTimer());

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

    private IEnumerator CooldownTimer()
    {
        float remaining = _cooldownDuration;

        while (remaining > 0f)
        {
            _cooldownText.text = Mathf.CeilToInt(remaining).ToString();
            remaining -= Time.deltaTime;
            yield return null;
        }

        _cooldownText.text = string.Empty;
        _button.interactable = true;
        _cooldownCoroutine = null;
    }

    private IEnumerator EffectTimer(Tower tower, EffectHandler handler, TowerEffect effect, float duration)
    {
        yield return new WaitForSeconds(duration);
        handler.RemoveEffect(effect);
        _activeEffects.Remove(tower);

        // Revert color only when all effects have expired
        if (_activeEffects.Count == 0)
            _button.image.color = _defaultColor;
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