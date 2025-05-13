using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StageSceneBtns : MonoBehaviour
{
    public void SceneChange()//화면 전환
    {
        SceneManager.LoadScene("TitleScene");// 화면 전환
    }

        public void StageSelect()
        {
            GameObject clickedButton = EventSystem.current.currentSelectedGameObject;//행당 버튼 오브젝트 가져오기
            string buttonName = clickedButton.name; // 이름 추출


            string stageNumber = buttonName.Substring(14); // "StageSelectBtn 1"에 14번쨰(숫자)부터 문자열읽기
            Debug.Log(stageNumber);//출력
            SceneManager.LoadScene("MainScenes");//메인으로
    }




 }
    


