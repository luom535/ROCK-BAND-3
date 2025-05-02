using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnyKeyToStart : MonoBehaviour
{
    [Header("UI settings")]
    public Text pressAnyKeyText; 
    [Header("Scene settings")]
    public string mainSceneName = "MainScene"; 

    [Header("Blink settings")]
    public float blinkSpeed = 2f; 

    private bool hasStarted = false;
    private Color originalColor;

    void Start()
    {
        if (pressAnyKeyText != null)
        {
            pressAnyKeyText.enabled = true;
            originalColor = pressAnyKeyText.color;
        }
    }

    void Update()
    {
        if (!hasStarted)
        {
            if (pressAnyKeyText != null)
            {
                float alpha = Mathf.Abs(Mathf.Sin(Time.time * blinkSpeed));
                pressAnyKeyText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            }

            if (Input.anyKeyDown)
            {
                hasStarted = true;

                if (pressAnyKeyText != null)
                {
                    pressAnyKeyText.enabled = false;
                }
                SceneManager.LoadScene(mainSceneName);
            }
        }
    }
}
