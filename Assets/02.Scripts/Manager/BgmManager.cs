using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    public static BgmManager instance;

    private AudioSource audioSource;

    [Header("Ÿ��Ʋ BGM")]
    public AudioClip titleBgm;

    [Header("�������� ������")]
    public StageData[] stageDataList;

    public float volume 
    {
        get => audioSource.volume;
        set=>audioSource.volume = value;
            }
    public bool mute
    {
        get => audioSource.mute;
        set => audioSource.mute = value; 
    }


    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("BGMManager�� AudioSource�� �����ϴ�!");
        }
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
    }

    public void PlayTitleBgm()
    {
        PlayBgm(titleBgm);
    }

    public void PlayStageBgm(int stageNumber)
    {
        StageData data = stageDataList.FirstOrDefault(d => d.stageNumber == stageNumber);
        if (data == null || data.bgm == null)
        {
            Debug.LogWarning($"Stage {stageNumber}�� ���� BGM �����͸� ã�� �� �����ϴ�.");
            return;
        }
        PlayBgm(data.bgm);
        Debug.Log($"[BGMManager] �������� ��ȣ: {stageNumber}");
    }

    private void PlayBgm(AudioClip clip)
    {
        if(audioSource.clip != clip)
        {
            audioSource.clip = clip;
        }
        audioSource.Play();
    }

    public void StopBgm()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

}
