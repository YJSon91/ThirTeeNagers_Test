using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenu : MonoBehaviour
{
    public static SettingMenu Instance { get; private set; }
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingPanel;

    private void Awake()
    {
        Instance = this;
        Debug.Log("settingMenu �ν��Ͻ� �ʱ�ȭ");
    }

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

    public void CloseSettings()
    {
        settingPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
}
