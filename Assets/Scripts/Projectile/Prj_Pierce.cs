using UnityEngine;

public class Prj_Pierce : Projectile
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
            damageable.TakeDamage(damage, element);

        // Does not return to pool allowing it to pierce through enemies.
        // Only returns to pool after lifetime expires
    }
}
