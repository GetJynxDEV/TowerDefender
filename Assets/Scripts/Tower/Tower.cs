using System;
using UnityEngine;

public abstract class Tower : MonoBehaviour, IUpgradable
{
    [Header("Tower Data")]
    public TowerDefinition towerData;
    public int currentLevel = 0;
    public TowerStats stats;

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
            // Rebuild runtime stats and reapply any active buffs
            GetComponent<EffectHandler>().ReapplyAll(towerData.GetStats(currentLevel));
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
        TowersInScene.Instance.towers.Add(this);
        LevelManager.Instance.RemoveCoins(towerData.cost[0]);
    }

}

[System.Serializable]
public class TowerStats
{
    public int damage;
    public float attackSpeed;
    public float critChance;
    public float critDamage;

    public TowerStats(LevelStats baseStats)
    {
        damage = baseStats.damage;
        attackSpeed = baseStats.attackSpeed;
        critChance = baseStats.critChance;
        critDamage = baseStats.critDamage;
    }
}
