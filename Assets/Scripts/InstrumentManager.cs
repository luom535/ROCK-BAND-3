using UnityEngine;
using System.Linq;

public class InstrumentManager : MonoBehaviour
{
    public static InstrumentManager Instance;

    [Header("乐器列表")]
    public InstrumentInput[] allInstruments;

    [Header("背景切换")]
    public GameObject level1Background;
    public GameObject level2Background;
    public GameObject level3Background;

    private int lastDetectedLevel = 1; 

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        CheckAndSwitchBackground();
    }

    public int GetCoinMultiplier()
    {
        if (allInstruments.All(i => i.currentLevel >= 3))
            return 3;
        else if (allInstruments.All(i => i.currentLevel >= 2))
            return 2;
        else
            return 1;
    }

    void CheckAndSwitchBackground()
    {
        int newLevel = 1;
        if (allInstruments.All(i => i.currentLevel >= 3))
            newLevel = 3;
        else if (allInstruments.All(i => i.currentLevel >= 2))
            newLevel = 2;

        if (newLevel != lastDetectedLevel)
        {
            lastDetectedLevel = newLevel;
            SwitchBackground(newLevel);
        }
    }

    void SwitchBackground(int level)
    {
        
        if (level1Background != null) level1Background.SetActive(false);
        if (level2Background != null) level2Background.SetActive(false);
        if (level3Background != null) level3Background.SetActive(false);


        switch (level)
        {
            case 1:
                if (level1Background != null) level1Background.SetActive(true);
                break;
            case 2:
                if (level2Background != null) level2Background.SetActive(true);
                break;
            case 3:
                if (level3Background != null) level3Background.SetActive(true);
                break;
        }

        Debug.Log($"background switch to Level {level}");
    }
}
