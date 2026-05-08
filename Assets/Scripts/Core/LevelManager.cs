using UnityEngine;
using System;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private BaseHealth _baseHealth;
    [SerializeField] private GameObject _gameOverPanel;
    public int coins = 100;
    public bool isGameOver = false;

    public event Action OnCoinChange;
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        UpdateCoinText();
        _baseHealth.OnBaseDestroyed += GameOver;
    }

    private void OnDestroy()
    {
        _baseHealth.OnBaseDestroyed -= GameOver;
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

    void GameOver() //NEW
    {
        isGameOver = true;
        Time.timeScale = 0f;
        if (_gameOverPanel != null) _gameOverPanel.SetActive(true);
    }
}