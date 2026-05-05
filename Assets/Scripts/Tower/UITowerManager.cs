using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITowerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textLevelInfo;
    [SerializeField] private Button _btnUpgrade;
    [SerializeField] private TextMeshProUGUI _textPrice;
    [SerializeField] private Button _btnRemove;
    [SerializeField] private Tower _tower;
    [SerializeField] private Transform _rightPos;
    [SerializeField] private Transform _leftPos;

    private int _price = 0;

    private void Start()
    {
        SetCanvasPosition();
        _btnUpgrade.onClick.AddListener(UpgradeTower);
        _btnRemove.onClick.AddListener(DestroyTower);
        _tower.OnUpgrade += UpdatePriceUI;
        LevelManager.Instance.OnCoinChange += CurrencyCheck;

        UpdatePriceUI(); // Initialize price on start so _price isn't 0 on first upgrade
    }

    void UpdatePriceUI()
    {
        if (_tower.currentLevel != _tower.towerData.maxLevel)
        {
            _textLevelInfo.text = $"Lvl {_tower.currentLevel} -> Lvl {_tower.currentLevel + 1}";
            _price = _tower.towerData.GetTowerPrice(_tower.currentLevel);
            _textPrice.text = $"{_price}";
            CurrencyCheck(); // Re-check affordability whenever price updates
        }
        else
        {
            _textLevelInfo.text = "Max Level";
            _textPrice.text = "Max Level";
            _btnUpgrade.interactable = false;
        }
    }

    void CurrencyCheck()
    {
        // Don't run affordability check if already at max level
        if (_tower.currentLevel >= _tower.towerData.maxLevel) return;

        if (LevelManager.Instance.coins >= _price)
        {
            _textLevelInfo.color = Color.black;
            _btnUpgrade.interactable = true;
        }
        else
        {
            _textLevelInfo.color = Color.red;
            _btnUpgrade.interactable = false;
        }
    }

    public void UpgradeTower()
    {
        if (LevelManager.Instance.coins < _price) return; // Safety guard

        IUpgradable upgradable = _tower.GetComponent<IUpgradable>();
        LevelManager.Instance.RemoveCoins(_price);
        upgradable.Upgrade();
    }

    public void DestroyTower()
    {
        TowersInScene.Instance.towers.Remove(_tower);
        Destroy(_tower.gameObject);
    }

    void SetCanvasPosition()
    {
        if (_tower.gameObject.transform.position.x <= 0)
            gameObject.transform.position = _rightPos.transform.position;
        else
            gameObject.transform.position = _leftPos.transform.position;
    }
}