using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class ButtonManager : MonoBehaviour
{
    [Header("버튼")]
    [SerializeField] private Button GameOver_restartButton;
    [SerializeField] private Button Pause_startButton;
    [SerializeField] private Button GmaeOver_ExitButton;
    [SerializeField] private Button Pause_ExitButton;
    [SerializeField] private Button PauseButton;
    [SerializeField] private Button SettingButton;
    [SerializeField] private Button CloseButton;


    [Header("패널")]
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject SettingPanel;
    [SerializeField] private GameObject GameOverPanel;



    private void Start()
    {
        PausePanel.SetActive(false);
        SettingPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }


    public void RestartGame()
    {
        StartCoroutine(RestartRoutine());
    }

    public IEnumerator RestartRoutine()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.InitGame();
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    public void OpenSettingsButton()
    {
        SettingMenu.Instance.OpenSettings();
    }

    public void PauseButtonActive()
    {
        PausePanel.SetActive(true);
        GameManager.Instance.Pause();
    }

    public void ResumeButton()
    {
        PausePanel.SetActive(false);
        GameManager.Instance.Resume();
    }

}
