using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private ParticleSystem _effectiveParticle;
    [SerializeField] private ParticleSystem _hitParticle;

    private void OnEnable()
    {
        _health.onHit += PlayHitEffect;
        _health.onEffectiveDamage += PlayEffectiveEffect;
    }

    void OnDisable()
    {
        _health.onHit -= PlayHitEffect;
        _health.onEffectiveDamage -= PlayEffectiveEffect;
    }

    public void PlayEffectiveEffect()
    {
        if (_effectiveParticle != null)
        {
            _effectiveParticle.Play();
        }
    }

    public void PlayHitEffect()
    {
        if (_hitParticle != null)
        {
            _hitParticle.Play();
        }
    }

    public void StopAllEffect()
    {
        if (_effectiveParticle != null)
        {
            _effectiveParticle.Stop();
        }
        if (_hitParticle != null)
        {
            _hitParticle.Stop();
        }
    }
}
