using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance { get; private set; }

    private Dictionary<int, Queue<GameObject>> _pool = new Dictionary<int, Queue<GameObject>>();

    private void Awake()
    {
        Instance = this;
    }

    public GameObject Get(GameObject prefab)
    {
        int id = prefab.GetComponent<Projectile>().poolID;

        if (_pool.TryGetValue(id, out Queue<GameObject> queue) && queue.Count > 0)
        {
            GameObject recycled = queue.Dequeue();
            recycled.SetActive(true);
            return recycled;
        }

        return Instantiate(prefab);
    }

    public void Return(GameObject projectileObj)
    {
        if (!projectileObj.activeSelf) return; // already returned, ignore

        int id = projectileObj.GetComponent<Projectile>().poolID;
        projectileObj.SetActive(false);
        projectileObj.transform.SetParent(transform);

        if (!_pool.ContainsKey(id))
            _pool[id] = new Queue<GameObject>();

        _pool[id].Enqueue(projectileObj);
    }
}