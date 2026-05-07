using System.Collections;
using UnityEngine;

public class Prj_AreaOfEffect : Projectile
{
    [Header("AoE Settings")]
    [SerializeField] private float maxRadius = 2f;
    [SerializeField] private float aoeSpreadSpeed = 5f;
    private CircleCollider2D aoeCollider;
    [SerializeField] private float baseRadius;

    // Bug fix #2: guard flag to prevent multiple coroutine triggers
    private bool isExploding = false;

    public override void Start()
    {
        base.Start();
        aoeCollider = GetComponent<CircleCollider2D>(); 
        transform.localScale = new Vector3(baseRadius, baseRadius, baseRadius);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage, element);

            if (!isExploding)
            {
                isExploding = true;
                StartCoroutine(TriggerAoEEffect());
            }
        }

        
    }

    IEnumerator TriggerAoEEffect()
    {
        rb.linearVelocity = Vector2.zero;
        float elapsed = 0f;
        float spreadDuration = maxRadius / aoeSpreadSpeed;

        while (elapsed < spreadDuration)
        {
            transform.localScale = Vector3.Lerp(
                new Vector3(baseRadius, baseRadius, baseRadius),
                new Vector3(maxRadius, maxRadius, maxRadius),
                elapsed / spreadDuration
            );
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = new Vector3(maxRadius, maxRadius, maxRadius);
        StartCoroutine(ReturnToPoolAfterDelay());
    }

    public override void OnEnable()
    {
        base.OnEnable();
        isExploding = false; // Reset flag when pulled from pool
        if (aoeCollider != null)
            aoeCollider.enabled = true;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        transform.localScale = new Vector3(baseRadius, baseRadius, baseRadius);
    }
}