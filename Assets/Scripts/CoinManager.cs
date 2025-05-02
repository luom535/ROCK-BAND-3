using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    [Header("UI Display")]
    public Text coinText;

    [Header("Default Coin Target (optional)")]
    public Transform defaultTarget;

    [Header("Coin Spawn Settings")]
    public float coinScaleMultiplier = 1f; 

    public int CurrentCoins { get; private set; } = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        CurrentCoins += amount;
        UpdateUI();
    }

    public void SpendCoins(int amount)
    {
        CurrentCoins = Mathf.Max(0, CurrentCoins - amount);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = $"Coins: {CurrentCoins}";
        }
        else
        {
            Debug.LogWarning("⚠️ no coin text setting!");
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="target"></param>
    /// <param name="coinPrefab"></param>
    public void Spawn3DCoin(Vector3 startPos, Transform target, GameObject coinPrefab)
    {
        if (coinPrefab == null)
        {
            Debug.LogWarning("⚠️ no coin setting Prefab！");
            return;
        }

        if (target == null)
        {
            Debug.LogWarning("⚠️ no target setting！use default target.");
            target = defaultTarget;
        }

        GameObject coin = Instantiate(coinPrefab, startPos, Quaternion.identity);

    
        coin.transform.localScale *= coinScaleMultiplier;


        
        LeanTween.move(coin, target.position, 0.5f)
            .setEaseInCubic()
            .setOnComplete(() => Destroy(coin));
    }
}
