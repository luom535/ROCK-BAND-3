using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyPressCounter : MonoBehaviour
{
    // 指定可被统计的按键
    private readonly KeyCode[] targetKeys = new KeyCode[]
    {
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T,
        KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.A, KeyCode.S,
        KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.Z, KeyCode.X,
        KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M
    };

    public int requiredPressCount = 20; // 需要按的次数
    public string nextSceneName;        // 目标场景名

    private int currentCount = 0;

    void Update()
    {
        foreach (KeyCode key in targetKeys)
        {
            if (Input.GetKeyDown(key))
            {
                currentCount++;
                Debug.Log($"按键 {key} 被按下，总次数：{currentCount}");

                if (currentCount >= requiredPressCount)
                {
                    SceneManager.LoadScene(nextSceneName);
                }

                break; // 每帧只统计一次按键
            }
        }
    }
}
