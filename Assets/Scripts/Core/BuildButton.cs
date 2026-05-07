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
        _costText.text = _buildCost.ToString();

        if (_buildButton == null)
        {
            _buildButton = GetComponent<Button>();
        }

        CurrencyCheck();

        LevelManager.Instance.OnCoinChange += CurrencyCheck;
    }

    void CurrencyCheck()
    {
        if (LevelManager.Instance.coins >= _buildCost)
        {
            _costText.color = Color.black;
            _buildButton.interactable = true;
        }
        else
        {
            _costText.color = Color.red;
            _buildButton.interactable = false;
        }
    }

    private void OnEnable()  // or Start(), wherever you subscribe
    {
        LevelManager.Instance.OnCoinChange += CurrencyCheck;
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent the event from firing on a dead object
        if (LevelManager.Instance != null)
            LevelManager.Instance.OnCoinChange -= CurrencyCheck;
    }

}
