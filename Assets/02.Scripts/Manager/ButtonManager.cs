using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

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
        StartCoroutine(RestartRoutine());
    }
    //����ŸƮ �޼���
    public IEnumerator RestartRoutine()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);           //�� �ٽ� �ҷ�����
        yield return new WaitForSeconds(0.1f);                                      //0.1�� ��ٸ�
        GameManager.Instance.InitGame();                                            //���ӸŴ��� �ʱ�ȭ
    }

    //���� ����(�������� ��� �÷��̸�� ����, ���� ���� ��� ���� ����)
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    //�Ͻ����� ��ư
    public void PauseButtonActive()
    {
        PausePanel.SetActive(true);
        GameManager.Instance.Pause();
    }
    //�簳��ư
    public void ResumeButton()
    {
        PausePanel.SetActive(false);
        GameManager.Instance.Resume();
    }
    //���� ȭ������ �̵�
    public void GoTitleScreen()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void TutorialBoardClick()
    {
        GameObject btn = GameObject.Find("TutorialBoard"); //�̸����� ���� ������Ʈ �˻��� btn�� ����
        btn.SetActive(false);
    }

}
