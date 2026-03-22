using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public TowerDefinition towerData;
    public int currentLevel = 0;
    public LevelStats stats;

    public void Start()
    {
        // Set Level 1 Stats
        UpgradeTower();
    }

    // For testing only
    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpgradeTower();
        }
    }

    public void UpgradeTower()
    {
        currentLevel++;
        if (currentLevel <= towerData.maxLevel)
        {
            stats = towerData.GetStats(currentLevel);
        }
        else
        {
            currentLevel = towerData.maxLevel;
        }
    }

}
