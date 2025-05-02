using UnityEngine;
using UnityEngine.UI;

public class AudienceFireManager : MonoBehaviour
{
    public static AudienceFireManager Instance;

    [Header("Hype Setting")]
    public Slider hypeSlider;
    public float hypeIncreaseSpeed = 20f; 
    public float hypeDecaySpeed = 10f;    
    public float hypeLerpSpeed = 5f;      
    public float fireThreshold = 100f;

    [Header("Fire 状态")]
    public GameObject fireVFX;
    public bool IsInFireMode { get; private set; }
    public float fireGraceTime = 5f; 

    private float currentHype = 0f;
    private float targetHype = 0f;
    private float timeSinceLastClick = 0f;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        Instance = this;
    }

    void Update()
    {
        timeSinceLastClick += Time.deltaTime;

        if (!IsInFireMode && timeSinceLastClick > 1f)
        {
            targetHype -= hypeDecaySpeed * Time.deltaTime;
            targetHype = Mathf.Clamp(targetHype, 0f, fireThreshold);
        }

        if (IsInFireMode)
        {
            if (timeSinceLastClick > fireGraceTime)
            {
                targetHype -= hypeDecaySpeed * Time.deltaTime;
                targetHype = Mathf.Clamp(targetHype, 0f, fireThreshold);

                if (targetHype <= 0f)
                {
                    ExitFireMode();
                }
            }
        }

        if (!IsInFireMode && targetHype >= fireThreshold)
        {
            EnterFireMode();
        }

        currentHype = Mathf.Lerp(currentHype, targetHype, Time.deltaTime * hypeLerpSpeed);
        if (hypeSlider != null)
        {
            hypeSlider.value = currentHype / fireThreshold;
        }

        // 🔥 保证 fireVFX 状态与 Fire 模式一致
        if (fireVFX != null)
        {
            fireVFX.SetActive(IsInFireMode);
        }
    }

    public void AddHypeByClick()
    {
        timeSinceLastClick = 0f;

        if (!IsInFireMode)
        {
            targetHype += hypeIncreaseSpeed;
            targetHype = Mathf.Clamp(targetHype, 0f, fireThreshold);
        }
        else
        {
            targetHype = fireThreshold;
        }
    }

    void EnterFireMode()
    {
        IsInFireMode = true;
        Debug.Log("🔥 Fire Mode Activated!");
    }

    void ExitFireMode()
    {
        IsInFireMode = false;
        Debug.Log("🔥 Fire Mode Ended");
    }
}
