using System;
using UnityEngine;

public abstract class Tower : MonoBehaviour, IUpgradable
{
    [Header("Tower Data")]
    public TowerDefinition towerData;
    public int currentLevel = 0;
    public LevelStats stats;

    [Header("References")]
    public Building building;
    public GameObject canvasUpgradeOption;

    [HideInInspector] public bool isPlaced = false;
    public event Action OnUpgrade;


    public void Start()
    {
        building.OnBuildingPlace += OnPlace;
        // Set Level 1 Stats
        Upgrade();
    }


    public void OpenUpgradePanel(bool setActive)
    {
        if (!isPlaced)
            return;

        canvasUpgradeOption.SetActive(setActive);
    }

    public void Upgrade()
    {
        currentLevel++;
        if (currentLevel <= towerData.maxLevel)
        {
            stats = towerData.GetStats(currentLevel);
            OnUpgrade?.Invoke();
        }
        else
        {
            currentLevel = towerData.maxLevel;
        }
    }

    public void OnPlace()
    {
        isPlaced = true;
    }

}
