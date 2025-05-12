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
            Match match = Regex.Match(buttonName, @"\d+");// 정규식으로 숫자 추출
            int stageNumber = int.Parse(match.Value); // 인트 값으면 변환
            Debug.Log(stageNumber);//출력
        SceneManager.LoadScene("MainScenes");//메인씬으로
    }




 }
    


