using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    void OnEnable()
    {
        if (CoinManager.Instance != null)
            CoinManager.Instance.OnCoinsChanged += UpdateCoinText;

        // pinta el valor actual por si ya había monedas
        if (CoinManager.Instance != null)
            UpdateCoinText(CoinManager.Instance.GetCoins());
    }

    void OnDisable()
    {
        if (CoinManager.Instance != null)
            CoinManager.Instance.OnCoinsChanged -= UpdateCoinText;
    }

    void UpdateCoinText(int value)
    {
        if (coinText) coinText.text = value.ToString();
    }

    void Update()
    {
        if (CoinManager.Instance && coinText)
            coinText.text = CoinManager.Instance.GetCoins().ToString();
    }
}
