using UnityEngine;
using System;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    public event Action<int> OnCoinsChanged;

    private int coins;
    [SerializeField] private int maxCoins = 100;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        // Opcional: DontDestroyOnLoad(gameObject);
    }

    public void AddCoins(int amount)
    {
        coins = Mathf.Clamp(coins + amount, 0, maxCoins);
        Debug.Log($"Total Coins: {coins}");
        OnCoinsChanged?.Invoke(coins); // 🔔 avisa a la UI
    }

    public int GetCoins() => coins;
}
