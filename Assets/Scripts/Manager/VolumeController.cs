using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource bgmaudioSource;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle bgmMuteToggle;
    [SerializeField] private Toggle sfxMuteToggle;

    // Start is called before the first frame update
    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        bool isBgmMute = PlayerPrefs.GetInt("bgmmute", 0) == 1;
        bool isSfxMute = PlayerPrefs.GetInt("sfxmute", 0) == 1;



        volumeSlider.value = savedVolume;
        bgmMuteToggle.isOn = !isBgmMute;
        sfxMuteToggle.isOn = !isSfxMute;

        audioSource.volume = savedVolume;
        audioSource.mute = isSfxMute;
        bgmaudioSource.mute = isBgmMute;

        volumeSlider.onValueChanged.AddListener(SetVolume);
        bgmMuteToggle.onValueChanged.AddListener(OnbgmMuteToggleChanged);
        sfxMuteToggle.onValueChanged.AddListener(OnsfxMuteToggleChanged);
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
        PlayerPrefs.Save();
    }
    private void OnbgmMuteToggleChanged(bool isOn)
    {
        bool isMute = !isOn;
        bgmaudioSource.mute = isMute;
        PlayerPrefs.SetInt("bgmmute", isMute ? 1 : 0);
        PlayerPrefs.Save();
    }
    private void OnsfxMuteToggleChanged(bool isOn)
    {
        bool ismute = !isOn;
        audioSource.mute = ismute;
        PlayerPrefs.SetInt("sfxmute", ismute ? 1 : 0);
        PlayerPrefs.Save();
    }
}
