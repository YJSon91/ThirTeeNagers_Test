using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneBtns : MonoBehaviour
{
    // Start is called before the first frame update
  public void SceneChange()
    {
        SceneManager.LoadScene("StageScene");// ���������� �̵�
        SFXManager.instance.PlayOnButtonClick();
    }

    public void GameExit()//���� ��ư
    {
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //����Ƽ ���� ����
        #else
        Application.Quit();
        #endif

        SFXManager.instance.PlayerOffButtonClick();
    }

    private void Start()
    {
        BgmManager.instance.PlayTitleBgm();
    }

}
