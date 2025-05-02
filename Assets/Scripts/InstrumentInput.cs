using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class InstrumentInput : MonoBehaviour
{
    [Header("升级与金币设置")]
    public int currentLevel = 0;
    public int maxLevel = 3;
    public int[] upgradeCosts;       
    public int[] coinOutputs;         

    [Header("金币相关设置")]
    public GameObject[] coinPrefabs;    
    public Transform coinStartPoint;    
    public Transform coinTargetPoint;  

    private CoinManager coinManager;
    private AudienceFireManager fireManager;

    void Start()
    {
        coinManager = CoinManager.Instance;
        fireManager = AudienceFireManager.Instance;

        if (upgradeCosts.Length != maxLevel || coinOutputs.Length != maxLevel || coinPrefabs.Length != maxLevel)
        {
            Debug.LogWarning($"{gameObject.name}：升级数组长度和 maxLevel 不一致，请检查设置！");
        }
    }

    public void OnClick()
    {
        GenerateCoins();
        fireManager.AddHypeByClick();
        TryUpgrade();
    }

    void GenerateCoins()
    {
        int baseCoins = coinOutputs[Mathf.Clamp(currentLevel, 0, coinOutputs.Length - 1)];


        int multiplier = InstrumentManager.Instance != null ? InstrumentManager.Instance.GetCoinMultiplier() : 1;

        int totalCoins = baseCoins * multiplier;

        if (fireManager.IsInFireMode)
        {
            totalCoins *= 2;
        }

        coinManager.AddCoins(totalCoins);

        GameObject coinPrefab = coinPrefabs[Mathf.Clamp(currentLevel, 0, coinPrefabs.Length - 1)];

        if (coinPrefab != null && coinStartPoint != null && coinTargetPoint != null)
        {
          
            for (int i = 0; i < multiplier; i++)
            {
                coinManager.Spawn3DCoin(coinStartPoint.position, coinTargetPoint, coinPrefab);
            }
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} 缺少 coinPrefab、起点或终点引用！");
        }
    }

    void TryUpgrade()
    {
        if (currentLevel >= maxLevel || currentLevel >= upgradeCosts.Length)
            return;

        int cost = upgradeCosts[currentLevel];
        if (coinManager.CurrentCoins >= cost)
        {
            coinManager.SpendCoins(cost);
            currentLevel++;
            Debug.Log($"{gameObject.name} 升级到 Lv.{currentLevel}");
        }
    }
}
