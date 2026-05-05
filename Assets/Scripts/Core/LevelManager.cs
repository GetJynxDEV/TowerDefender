using UnityEngine;
using System;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    public int coins = 100;

    public event Action OnCoinChange;

    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        UpdateCoinText();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        OnCoinChange?.Invoke();
        UpdateCoinText();
    }

    public void RemoveCoins(int amount)
    {
        coins -= amount;
        OnCoinChange?.Invoke();
        UpdateCoinText();
    }

    void UpdateCoinText()
    {
        _coinsText.text = $"Coins: {coins}";
    }
}
