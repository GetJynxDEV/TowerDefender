using System.Collections;
using UnityEngine;

public class Prj_AreaOfEffect : Projectile
{
    [Header("AoE Settings")]
    [SerializeField] private float maxRadius = 2f;
    [SerializeField] private float aoeSpreadSpeed = 5f;
    //private CircleCollider2D aoeCollider;
    [SerializeField] private float baseRadius;

    public override void Start()
    {
        base.Start();
        //aoeCollider = GetComponent<CircleCollider2D>();
        //baseRadius = aoeCollider.radius;
        transform.localScale = new Vector3(baseRadius, baseRadius, baseRadius);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage, element);
            StartCoroutine(TriggerAoEEffect());
        }
    }

    IEnumerator TriggerAoEEffect()
    {
        rb.linearVelocity = Vector2.zero;
        float elapsed = 0f;
        float spreadDuration = maxRadius / aoeSpreadSpeed;

        while (elapsed < spreadDuration)
        {
            //aoeCollider.radius = Mathf.Lerp(baseRadius, maxRadius, elapsed / spreadDuration);
            transform.localScale = Vector3.Lerp(new Vector3(baseRadius, baseRadius, baseRadius), new Vector3(maxRadius, maxRadius, maxRadius), elapsed / spreadDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = new Vector3(maxRadius, maxRadius, maxRadius);
        //aoeCollider.radius = maxRadius;
        ReturnToPool();
    }

    public override void OnDisable()
    {
        base.OnDisable();
        transform.localScale = new Vector3(baseRadius, baseRadius, baseRadius);
        //aoeCollider.radius = baseRadius;
    }
}
