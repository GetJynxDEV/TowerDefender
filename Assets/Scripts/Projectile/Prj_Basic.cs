using UnityEngine;

public class Prj_Basic : Projectile
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage, element);
            StartCoroutine(ReturnToPoolAfterDelay());
        }
    }
}
