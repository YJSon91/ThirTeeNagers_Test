using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneBtns : MonoBehaviour
{
    // Start is called before the first frame update
  public void SceneChange()
    {
        SceneManager.LoadScene("StageScene");// 스테이지로 이동
    }

    public void GameExit()//종료 버튼
    {
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //유니티 실행 종료
        #else
        Application.Quit();
        #endif
    }

    private void Start()
    {
        BgmManager.instance.PlayTitleBgm();
    }

}
