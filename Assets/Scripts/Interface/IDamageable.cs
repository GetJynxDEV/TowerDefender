using UnityEngine;

public interface IDamageable
{
    Element element { get; }
    void TakeDamage(int damage);
    void TakeDamage(int damage, Element attackerElement);
}

public enum Element
{
    Fire,
    Earth,
    Air,
    Lightning,
    Water,
    None
}
