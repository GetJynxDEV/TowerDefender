using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] Transform _firePoint;

    public void SpawnProjectile(Transform targetPos, int damage)
    {
        GameObject p = ProjectilePool.Instance.Get(_projectilePrefab);
        p.transform.SetParent(null);
        p.transform.position = _firePoint.position;


        Vector2 dir = (targetPos.position - _firePoint.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        p.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Projectile prj = p.GetComponent<Projectile>();
        prj.damage = damage;
        prj.SetDirection(dir);
    }
}