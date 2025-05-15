using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEditorInternal.VersionControl.ListControl;

public class ButtonManager : MonoBehaviour
{
    //��ư ������Ʈ(�ν����Ϳ��� �Ҵ�)
    [Header("��ư")]
    [SerializeField] private Button GameOver_restartButton;
    [SerializeField] private Button Pause_startButton;
    [SerializeField] private Button GmaeOver_ExitButton;
    [SerializeField] private Button Pause_ExitButton;
    [SerializeField] private Button PauseButton;
    [SerializeField] private Button SettingButton;
    [SerializeField] private Button CloseButton;
    [SerializeField] private Button TitleButton;

    //�г� ������Ʈ(�ν����Ϳ��� �Ҵ�)
    [Header("�г�")]
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject SettingPanel;
    [SerializeField] private GameObject GameOverPanel;

   


    //�����Ҷ� �ʱ�ȭ(��Ȱ��ȭ�� default)
    private void Start()
    {
        PausePanel.SetActive(false);
        SettingPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    //�������ְ� ����ŸƮ 
    public void RestartGame()
    {
        Time.timeScale = 1f;
        int lastStage = PlayerPrefs.GetInt("LastStage", 1);
        PlayerPrefs.SetInt("TempStageToRestart",lastStage);
        GameManager.IsResatarting = true;
        SceneManager.sceneLoaded += OnSceneReloaded;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnSceneReloaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneReloaded;
        int stageToRestart = PlayerPrefs.GetInt("TempStageToRestart");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.InitGame(stageToRestart);
            GameManager.Instance.StageStart();
        }
        else
        {
            Debug.LogWarning("GameManager.Instance is null after scene load!");
        }
    }


//���� ����(�������� ��� �÷��̸�� ����, ���� ���� ��� ���� ����)
public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
        SFXManager.instance.PlayerOffButtonClick();
    }

    //�Ͻ����� ��ư
    public void PauseButtonActive()
    {
        PausePanel.SetActive(true);
        GameManager.Instance.Pause();
        SFXManager.instance.PlayOnButtonClick();
    }
    //�簳��ư
    public void ResumeButton()
    {
        PausePanel.SetActive(false);
        GameManager.Instance.Resume();
        SFXManager.instance.PlayerOffButtonClick();
    }
    //���� ȭ������ �̵�
    public void GoTitleScreen()
    {
        SceneManager.LoadScene("TitleScene");
        SFXManager.instance.PlayerOffButtonClick();
    }
    public void TutorialBoardClick()
    {
        
        GameObject btn = GameObject.Find("TutorialBoard"); //�̸����� ���� ������Ʈ �˻��� btn�� ����
        btn.SetActive(false);

        GameManager.Instance.Resume();



    }

}
