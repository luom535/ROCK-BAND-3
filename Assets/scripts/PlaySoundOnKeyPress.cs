using UnityEngine;

public class PlaySoundOnKeyPress : MonoBehaviour
{
    [Header("输入设置")]
    public KeyCode keyToPress = KeyCode.Space;     

    [Header("声音设置")]
    public AudioClip clip;                        

    [Header("视觉高亮设置")]
    public GameObject targetObject;               
    public float outlineThickness = 2f;            
    public float outlineDuration = 0.3f;           

    private AudioSource audioSource;             
    private Material spriteMat;            

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.clip = clip;

        if (targetObject != null)
        {
            SpriteRenderer sr = targetObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                spriteMat = sr.material;
                if (spriteMat.HasProperty("_OutlineThickness"))
                {
                    spriteMat.SetFloat("_OutlineThickness", 0); 
                }
                else
                {
                    Debug.LogWarning("材质上没有 '_OutlineThickness' 属性！");
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            Debug.Log($"按下按键：{keyToPress}");

            if (clip != null)
            {
                audioSource.PlayOneShot(clip);
            }

            if (spriteMat != null)
            {
                spriteMat.SetFloat("_OutlineThickness", outlineThickness);
                CancelInvoke(nameof(DisableOutline));
                Invoke(nameof(DisableOutline), outlineDuration);
            }
        }
    }

    void DisableOutline()
    {
        if (spriteMat != null)
        {
            spriteMat.SetFloat("_OutlineThickness", 0);
        }
    }
}
