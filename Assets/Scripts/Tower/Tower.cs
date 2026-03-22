using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [Header("Tower Data")]
    public TowerDefinition towerData;
    public int currentLevel = 0;
    public LevelStats stats;

    [Header("References")]
    public Building building;
    public GameObject canvasUpgradeOption;

    public bool isPlaced = false;


    public void Start()
    {
        building.OnBuildingPlace += OnPlace;
        // Set Level 1 Stats
        Upgrade();
    }

    // For testing only
    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Upgrade();
        }
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
