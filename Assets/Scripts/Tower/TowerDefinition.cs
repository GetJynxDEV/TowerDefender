using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TowerDefinition", menuName = "Tower/TowerDefinition")]
public class TowerDefinition : ScriptableObject
{
    public int maxLevel;
    public int[] cost;
    public LevelStats[] statsPerLevel;

    private void OnValidate()
    {
        Array.Resize(ref cost, maxLevel);
        Array.Resize(ref statsPerLevel, maxLevel);
    }

    public LevelStats GetStats(int level)
    {
        if (level <= 0 || level > statsPerLevel.Length)
        {
            Debug.LogWarning("Level out of bounds");
            return statsPerLevel[0];
        }
        return statsPerLevel[level - 1];
    }

    public int GetTowerPrice(int nextLevel)
    {
        return cost[nextLevel];
    }
}

[System.Serializable]
public class LevelStats
{
    public int damage;
    public float attackSpeed;
    public float critChance;
    public float critDamage;
}
