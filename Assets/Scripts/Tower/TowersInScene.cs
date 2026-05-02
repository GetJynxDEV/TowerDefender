using System.Collections.Generic;
using UnityEngine;

public class TowersInScene : MonoBehaviour
{
    public List<Tower> towers = new List<Tower>();

    public static TowersInScene Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
