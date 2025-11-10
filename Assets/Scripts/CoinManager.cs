using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }
    private int coins;
    private int maxCoins = 100;
    public GameObject coinPrefab;
    
    private void Awake()
    {
        if (Instance) Destroy(Instance.gameObject);
        Instance = this;
    }

    private void Start()
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                var instance = Instantiate(coinPrefab);
                instance.transform.position = new Vector3(x, 2f, y);
            }
        }
    }

    // Update is called once per frame
    public void AddCoins(int amount)
    {
        coins = Mathf.Min(maxCoins, coins + amount);
        Debug.Log($"Total Coins: {coins}");
    }
}
