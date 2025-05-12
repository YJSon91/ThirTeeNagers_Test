using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenu : MonoBehaviour
{
    public static SettingMenu Instance { get; private set; }            //세팅 메뉴 인스턴스화
    [SerializeField] private GameObject pausePanel;                     //일시정지 메뉴 패널
    [SerializeField] private GameObject settingPanel;                   //세팅 메뉴 패널


    //setting 싱글톤 패턴
    private void Awake()
    {
        Instance = this;
        Debug.Log("settingMenu 인스턴스 초기화");
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
}
