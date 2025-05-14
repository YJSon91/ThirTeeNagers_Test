using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public static SettingMenu Instance { get; private set; }            //세팅 메뉴 인스턴스화
    [SerializeField] private GameObject pausePanel;                     //일시정지 메뉴 패널
    [SerializeField] private GameObject settingPanel;                   //세팅 메뉴 패널
    [SerializeField] private AudioSource sfxaudioSource;                   //
    [SerializeField] private AudioSource bgmaudioSource;
    [SerializeField] private Slider sfxvolumeSlider;
    [SerializeField] private Slider bgmvolumeSlider;
    [SerializeField] private Toggle bgmMuteToggle;
    [SerializeField] private Toggle sfxMuteToggle;
    [SerializeField] private Toggle godModeToggle;

    private PlayerHandler playerHandler;

    //setting 싱글톤 패턴
    private void Awake()
    {
        Instance = this;
        Debug.Log("settingMenu 인스턴스 초기화");

        playerHandler = FindObjectOfType<PlayerHandler>();
    }

    //세팅메뉴 활성화
    public void OpenSettings()
    {
        if (SettingMenu.Instance == null)
        {
            Debug.LogWarning("SettingMenu.Instance가 null");
            return;
        }
        settingPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    //세팅메뉴 비활성화
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
            Debug.Log($"[Settings] 갓모드 {(isOn ? "활성화" : "비활성화")}");
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
