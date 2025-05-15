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
    }

    public void GameExit()//���� ��ư
    {
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //����Ƽ ���� ����
        #else
        Application.Quit();
        #endif
    }

    private void Start()
    {
        BgmManager.instance.PlayTitleBgm();
    }

}
