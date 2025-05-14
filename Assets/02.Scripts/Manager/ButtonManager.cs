using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

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
        StartCoroutine(RestartRoutine());
    }
    //리스타트 메서드
    public IEnumerator RestartRoutine()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);           //씬 다시 불러오기
        yield return new WaitForSeconds(0.1f);                                      //0.1초 기다림
        GameManager.Instance.InitGame();                                            //게임매니저 초기화
    }

    //게임 종료(에디터일 경우 플레이모드 해제, 빌드 후일 경우 게임 종료)
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    //일시정지 버튼
    public void PauseButtonActive()
    {
        PausePanel.SetActive(true);
        GameManager.Instance.Pause();
    }
    //재개버튼
    public void ResumeButton()
    {
        PausePanel.SetActive(false);
        GameManager.Instance.Resume();
    }
    //시작 화면으로 이동
    public void GoTitleScreen()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void TutorialBoardClick()
    {
        GameObject btn = GameObject.Find("TutorialBoard"); //이름으로 게임 오브젝트 검색후 btn에 넣음
        btn.SetActive(false);
    }

}
