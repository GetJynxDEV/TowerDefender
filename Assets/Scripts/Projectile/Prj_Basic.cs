using UnityEngine;

public class Prj_Basic : Projectile
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
            damageable.TakeDamage(damage, element);
            ReturnToPool();
    }
}
