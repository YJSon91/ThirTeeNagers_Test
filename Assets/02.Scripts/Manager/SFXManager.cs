using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    private AudioSource audioSource;

    [Header("UI 버튼 클릭 사운드")]
    public AudioClip buttonOnClickClip;
    public AudioClip buttonOffClickClip;

    public float volume
    {
        get => audioSource.volume;
        set => audioSource.volume = value;
    }
    public bool mute
    {
        get => audioSource.mute;
        set => audioSource.mute = value;

    }

    private void Awake()
    {
        if(instance != null & instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayOnButtonClick()
    {
        if (buttonOnClickClip != null)
        {
            audioSource.PlayOneShot(buttonOnClickClip);
        }
    }

    public void PlayerOffButtonClick()
    {
        if(buttonOffClickClip != null)
            audioSource.PlayOneShot(buttonOffClickClip);
    }

}
