using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    [SerializeField] private int _buildCost = 100;
    [SerializeField] private Button _buildButton;
    [SerializeField] private TextMeshProUGUI _costText;

    private void Start()
    {
        if (_buildButton == null) _buildButton = GetComponent<Button>();
        _costText.text = _buildCost.ToString();
        CurrencyCheck();
    }

    private void OnEnable()
    {
        if (LevelManager.Instance == null) return;
        LevelManager.Instance.OnCoinChange += CurrencyCheck;
    }

    private void OnDisable()
    {
        if (LevelManager.Instance == null) return;
        LevelManager.Instance.OnCoinChange -= CurrencyCheck;
    }

    private void OnDestroy()
    {
        if (LevelManager.Instance != null)
            LevelManager.Instance.OnCoinChange -= CurrencyCheck;
    }

    void CurrencyCheck()
    {
        bool canAfford = LevelManager.Instance.coins >= _buildCost;
        _costText.color = canAfford ? Color.black : Color.red;
        _buildButton.interactable = canAfford;
    }
}