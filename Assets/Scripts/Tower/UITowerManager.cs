using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITowerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textLevelInfo;
    [SerializeField] private Button _btnUpgradeButton;
    [SerializeField] private TextMeshProUGUI _textPrice;
    [SerializeField] private Button _btnRemoveButton;
    [SerializeField] private Tower _tower;

    [SerializeField] private Transform _rightPos;
    [SerializeField] private Transform _leftPos;

    private void Start()
    {
        SetCanvasPosition();
        _btnUpgradeButton.onClick.AddListener(UpgradeTower);
        _btnRemoveButton.onClick.AddListener(DestroyTower);
        _tower.OnUpgrade += UpdateUI;
    }

    void UpdateUI()
    {
        if (_tower.currentLevel != _tower.towerData.maxLevel)
        {
            _textLevelInfo.text = $"Lvl {_tower.currentLevel} -> Lvl {_tower.currentLevel + 1}";

            int price = _tower.towerData.GetTowerPrice(_tower.currentLevel);
            _textPrice.text = $"{price}";
        }
        else
        {
            _textLevelInfo.text = "Max Level";
            _textPrice.text = "Max Level";
            _btnUpgradeButton.enabled = false;
        }
        
    }

    public void UpgradeTower()
    {
        IUpgradable upgradable = _tower.GetComponent<IUpgradable>();
        upgradable.Upgrade();
    }

    public void DestroyTower()
    {
        Destroy( _tower.gameObject);
    }

    void SetCanvasPosition()
    {
        if (_tower.gameObject.transform.position.x <= 0)
        {
            gameObject.transform.position = _rightPos.transform.position;
        }
        else
        {
            gameObject.transform.position = _leftPos.transform.position;

        }
    }
}
