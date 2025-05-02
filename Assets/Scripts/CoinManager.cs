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
    public float coinScaleMultiplier = 1f;  // 可用于整体缩放飞行金币

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
            Debug.LogWarning("⚠️ Coin Text UI 未绑定！");
        }
    }

    /// <summary>
    /// 生成金币并飞向目标位置（带动画）
    /// </summary>
    /// <param name="startPos">生成起点（通常为乐器位置）</param>
    /// <param name="target">金币飞向的目标位置</param>
    /// <param name="coinPrefab">金币Prefab（必须是3D模型）</param>
    public void Spawn3DCoin(Vector3 startPos, Transform target, GameObject coinPrefab)
    {
        if (coinPrefab == null)
        {
            Debug.LogWarning("⚠️ 没有设置金币 Prefab！");
            return;
        }

        if (target == null)
        {
            Debug.LogWarning("⚠️ 没有指定目标点！使用默认 target。");
            target = defaultTarget;
        }

        GameObject coin = Instantiate(coinPrefab, startPos, Quaternion.identity);

        // ✅ 保留 Prefab 本身的缩放，但可选乘以一个系数
        coin.transform.localScale *= coinScaleMultiplier;

        // 自动播放 Animator（如旋转），无需额外处理

        // 移动金币至目标点后销毁
        LeanTween.move(coin, target.position, 0.5f)
            .setEaseInCubic()
            .setOnComplete(() => Destroy(coin));
    }
}
