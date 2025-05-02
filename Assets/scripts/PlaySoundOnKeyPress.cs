using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnKeyPress : MonoBehaviour
{
    [Header("Input Setting")]
    public KeyCode keyToPress = KeyCode.Space;

    [Header("Level 1 Sounds")]
    public AudioClip[] clipsLevel1;

    [Header("Level 2 Sounds")]
    public AudioClip[] clipsLevel2;

    [Header("Level 3 Sounds")]
    public AudioClip[] clipsLevel3;

    [Header("Effect / Animator")]
    public GameObject visualEffect;         
    public Animator targetAnimator;        
    public string triggerName = "Play";    

    private AudioSource audioSource;
    private InstrumentInput instrumentInput;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;

        instrumentInput = GetComponent<InstrumentInput>();
        if (instrumentInput == null)
        {
            Debug.LogWarning($"No InstrumentInput component found on {gameObject.name}!");
        }

        // 初始化时关闭特效
        if (visualEffect != null)
        {
            var ps = visualEffect.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
            else
            {
                visualEffect.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            Debug.Log($"Pressed key: {keyToPress}");

            PlayRandomClipsByLevel();

            if (instrumentInput != null)
            {
                instrumentInput.OnClick();
            }

            TriggerEffectOrAnimation();
        }
    }

    void PlayRandomClipsByLevel()
    {
        AudioClip[] sourceClips = null;

        switch (instrumentInput.currentLevel)
        {
            case 0: sourceClips = clipsLevel1; break;
            case 1: sourceClips = clipsLevel2; break;
            case 2:
            default: sourceClips = clipsLevel3; break;
        }

        if (sourceClips == null || sourceClips.Length == 0)
        {
            Debug.LogWarning("No audio clips available for current level.");
            return;
        }

        int clipCount = Mathf.Min(sourceClips.Length, Random.Range(1, 4));
        List<int> usedIndices = new List<int>();

        for (int i = 0; i < clipCount; i++)
        {
            int index;
            do
            {
                index = Random.Range(0, sourceClips.Length);
            } while (usedIndices.Contains(index));

            usedIndices.Add(index);
            audioSource.PlayOneShot(sourceClips[index]);
        }
    }

    void TriggerEffectOrAnimation()
    {
    
        if (visualEffect != null)
        {
            var ps = visualEffect.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                ps.Play();
            }
            else
            {
                visualEffect.SetActive(false); 
                visualEffect.SetActive(true);
            }
        }

       
        if (targetAnimator != null && !string.IsNullOrEmpty(triggerName))
        {
            targetAnimator.SetTrigger(triggerName);
        }
    }
}
