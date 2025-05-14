using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenu : MonoBehaviour
{
    public static SettingMenu Instance { get; private set; }            //���� �޴� �ν��Ͻ�ȭ
    [SerializeField] private GameObject pausePanel;                     //�Ͻ����� �޴� �г�
    [SerializeField] private GameObject settingPanel;                   //���� �޴� �г�


    //setting �̱��� ����
    private void Awake()
    {
        Instance = this;
        Debug.Log("settingMenu �ν��Ͻ� �ʱ�ȭ");
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
}
