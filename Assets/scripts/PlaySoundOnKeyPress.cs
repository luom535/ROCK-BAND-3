using UnityEngine;

public class PlaySoundOnKeyPress : MonoBehaviour
{
    public AudioClip clip; // 要播放的音频片段
    public KeyCode keyToPress = KeyCode.Space; // 触发播放的按键，默认是空格键

    private AudioSource audioSource;

    void Start()
    {
        // 尝试获取 AudioSource 组件
        audioSource = GetComponent<AudioSource>();

        // 如果没有，就自动添加一个
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.clip = clip;
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            audioSource.Play();
        }
    }
}
