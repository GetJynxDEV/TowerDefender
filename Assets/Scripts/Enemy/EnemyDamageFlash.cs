using System.Collections;
using UnityEngine;

public class EnemyDamageFlash : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _flashDuration = 0.1f;
    [SerializeField] private Color _flashColor = Color.red;

    private Color _originalColor;
    private Coroutine _flashCoroutine;
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _originalColor = _spriteRenderer.color;
    }

    private void OnEnable()
    {
        _spriteRenderer.color = _originalColor;
        _health.onHit += TriggerFlash;
    }

    private void OnDisable()
    {
        if (_flashCoroutine != null) StopCoroutine(_flashCoroutine);
        _health.onHit -= TriggerFlash;
    }

    private void TriggerFlash()
    {
        if (_flashCoroutine != null) StopCoroutine(_flashCoroutine);
        _flashCoroutine = StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        _spriteRenderer.color = _flashColor;
        yield return new WaitForSeconds(_flashDuration);
        _spriteRenderer.color = _originalColor;
        _flashCoroutine = null;
    }
}