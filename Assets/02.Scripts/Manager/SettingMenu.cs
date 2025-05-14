using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public static SettingMenu Instance { get; private set; }            //���� �޴� �ν��Ͻ�ȭ
    [SerializeField] private GameObject pausePanel;                     //�Ͻ����� �޴� �г�
    [SerializeField] private GameObject settingPanel;                   //���� �޴� �г�
    [SerializeField] private AudioSource sfxaudioSource;                   //
    [SerializeField] private AudioSource bgmaudioSource;
    [SerializeField] private Slider sfxvolumeSlider;
    [SerializeField] private Slider bgmvolumeSlider;
    [SerializeField] private Toggle bgmMuteToggle;
    [SerializeField] private Toggle sfxMuteToggle;
    [SerializeField] private Toggle godModeToggle;

    private PlayerHandler playerHandler;

    //setting �̱��� ����
    private void Awake()
    {
        Instance = this;
        Debug.Log("settingMenu �ν��Ͻ� �ʱ�ȭ");

        playerHandler = FindObjectOfType<PlayerHandler>();
    }

    //���ø޴� Ȱ��ȭ
    public void OpenSettings()
    {
        if (SettingMenu.Instance == null)
        {
            Debug.LogWarning("SettingMenu.Instance�� null");
            return;
        }
        settingPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    //���ø޴� ��Ȱ��ȭ
    public void CloseSettings()
    {
        settingPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    // Start is called before the first frame update
    private void Start()
    {

        float savedSfxVolume = PlayerPrefs.GetFloat("sfxVolume", 1f);
        float savedBgmVolume = PlayerPrefs.GetFloat("bgmVolume", 1f);
        bool isBgmMute = PlayerPrefs.GetInt("bgmmute", 0) == 1;
        bool isSfxMute = PlayerPrefs.GetInt("sfxmute", 0) == 1;



        sfxvolumeSlider.value = savedSfxVolume;
        bgmvolumeSlider.value = savedBgmVolume;
        bgmMuteToggle.isOn = !isBgmMute;
        sfxMuteToggle.isOn = !isSfxMute;

        sfxaudioSource.volume = savedSfxVolume;
        bgmvolumeSlider.value = savedBgmVolume;
        sfxaudioSource.mute = isSfxMute;
        bgmaudioSource.mute = isBgmMute;

        sfxvolumeSlider.onValueChanged.AddListener(SetSfxVolume);
        bgmvolumeSlider.onValueChanged.AddListener(SetBgmVolume);
        bgmMuteToggle.onValueChanged.AddListener(OnbgmMuteToggleChanged);
        sfxMuteToggle.onValueChanged.AddListener(OnsfxMuteToggleChanged);


        if(StageManager.instance.currentStageData.stageNumber < 5)
        {
            godModeToggle.interactable = false;
            godModeToggle.isOn = false;
            playerHandler.godMod = false;

        }
        else
        {
            {
                godModeToggle.interactable= true;
                godModeToggle.isOn = playerHandler.godMod;
            }
        }
        godModeToggle.onValueChanged.AddListener((isOn) =>
        {
            playerHandler.godMod = isOn;
            Debug.Log($"[Settings] ����� {(isOn ? "Ȱ��ȭ" : "��Ȱ��ȭ")}");
        });
    }

    public void SetSfxVolume(float value)
    {
        sfxaudioSource.volume = value;
        PlayerPrefs.SetFloat("sfxVolume", value);
        PlayerPrefs.Save();
    }
    public void SetBgmVolume(float value)
    {
        bgmaudioSource.volume = value;
        PlayerPrefs.SetFloat("bgmVolume", value);
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
        sfxaudioSource.mute = ismute;
        PlayerPrefs.SetInt("sfxmute", ismute ? 1 : 0);
        PlayerPrefs.Save();
    }

}
