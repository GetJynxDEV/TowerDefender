using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    [Header("References")]
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;

    public event Action OnBuildingPlace;

    public Color GetColor()
    {
        return spriteRenderer.color;
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public Vector2 CenterPoint()
    {
        return (Vector2)transform.TransformPoint(boxCollider.offset);
    }

    public Vector2 ColliderWorldSize()
    {
        var size = boxCollider.size;
        size.x *= transform.lossyScale.x;
        size.y *= transform.lossyScale.y;
        return size;
    }

    // Returns all 4 world-space corners of the BoxCollider2D
    public Vector2[] GetColliderCorners()
    {
        Vector2 center = CenterPoint();
        Vector2 halfSize = ColliderWorldSize() / 2f;

        return new Vector2[]
        {
            center + new Vector2(-halfSize.x, -halfSize.y), // bottom-left
            center + new Vector2( halfSize.x, -halfSize.y), // bottom-right
            center + new Vector2(-halfSize.x,  halfSize.y), // top-left
            center + new Vector2( halfSize.x,  halfSize.y)  // top-right
        };
    }

    public void InvokeBuildingPlace()
    {
        OnBuildingPlace?.Invoke();
    }
}