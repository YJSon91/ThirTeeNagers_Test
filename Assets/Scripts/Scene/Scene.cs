using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Scene : GameManager
public class Scene : MonoBehaviour
{

    void Start()
    {
        int j = 11;
        //int j =  currentStage; // 게임 메니져에서 가져오는 현제 가장 높은 스테이지 값

        GameObject Infinity = GameObject.Find("Infinity"); Infinity.SetActive(false); //인피니티 오브젝트 가져오고 숨기기
        int i = 1;
        while (true)//~참인동안
        {

            GameObject btn = GameObject.Find("StageBtn (" + i + ")"); //이름으로 게임 오브젝트 검색후 btn에 넣음
            if (btn == null)//없으면 반복그만
            {
                if(j+1 > i)//최종 스테이지를 넘었을 경우
                {
                    Infinity.SetActive(true);//인피니티 모드해금
                }
                break;
            }
            btn.SetActive(false);//해당 오브젝트 비활성화

            if (j >= i)//만약 행당스테이지 까지 열려 있으면
                btn.SetActive(true); //보이게 한다
           i++;//i값 올리기
        }
    }
}