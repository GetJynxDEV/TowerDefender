using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement Settings")]
    public Rigidbody2D rb;
    public float speed;
    public float lifetime = 5f;

    [Header("Combat & Stats")]
    public Element element;
    [HideInInspector] public int damage;

    [Header("Pooling System")]
    public int poolID;
    public float poolReturnDelay = 0.5f;

    [Header("Visuals")]
    public SpriteRenderer sprite;


    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void OnEnable()
    {
        sprite.enabled = true;
        StartCoroutine(LifetimeCoroutine());
    }

    private IEnumerator LifetimeCoroutine()
    {
        yield return new WaitForSeconds(lifetime);
        ReturnToPool();
    }

    public virtual void SetDirection(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * speed;
    }

    public virtual void OnDisable()
    {
        // StopAllCoroutines is called automatically on disable,
        // so LifetimeCoroutine is always cleaned up when pooled.
        rb.linearVelocity = Vector2.zero;
    }

    public IEnumerator ReturnToPoolAfterDelay()
    {
        rb.linearVelocity = Vector2.zero;
        sprite.enabled = false;
        yield return new WaitForSeconds(poolReturnDelay);
        ReturnToPool();
    }

    public virtual void ReturnToPool()
    {
        ProjectilePool.Instance.Return(this.gameObject);
    }

    
}