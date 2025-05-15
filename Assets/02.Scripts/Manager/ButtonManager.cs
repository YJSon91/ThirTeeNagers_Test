using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEditorInternal.VersionControl.ListControl;

public class ButtonManager : MonoBehaviour
{
    //버튼 오브젝트(인스펙터에서 할당)
    [Header("버튼")]
    [SerializeField] private Button GameOver_restartButton;
    [SerializeField] private Button Pause_startButton;
    [SerializeField] private Button GmaeOver_ExitButton;
    [SerializeField] private Button Pause_ExitButton;
    [SerializeField] private Button PauseButton;
    [SerializeField] private Button SettingButton;
    [SerializeField] private Button CloseButton;
    [SerializeField] private Button TitleButton;

    //패널 오브젝트(인스펙터에서 할당)
    [Header("패널")]
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject SettingPanel;
    [SerializeField] private GameObject GameOverPanel;

   


    //시작할때 초기화(비활성화가 default)
    private void Start()
    {
        PausePanel.SetActive(false);
        SettingPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    //딜레이주고 리스타트 
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


//게임 종료(에디터일 경우 플레이모드 해제, 빌드 후일 경우 게임 종료)
public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
        SFXManager.instance.PlayerOffButtonClick();
    }

    //일시정지 버튼
    public void PauseButtonActive()
    {
        PausePanel.SetActive(true);
        GameManager.Instance.Pause();
        SFXManager.instance.PlayOnButtonClick();
    }
    //재개버튼
    public void ResumeButton()
    {
        PausePanel.SetActive(false);
        GameManager.Instance.Resume();
        SFXManager.instance.PlayerOffButtonClick();
    }
    //시작 화면으로 이동
    public void GoTitleScreen()
    {
        SceneManager.LoadScene("TitleScene");
        SFXManager.instance.PlayerOffButtonClick();
    }
    public void TutorialBoardClick()
    {
        
        GameObject btn = GameObject.Find("TutorialBoard"); //이름으로 게임 오브젝트 검색후 btn에 넣음
        btn.SetActive(false);

        GameManager.Instance.Resume();



    }

}
