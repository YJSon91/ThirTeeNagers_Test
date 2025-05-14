using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectButtonManager : MonoBehaviour
{
    [SerializeField] private StageData stageData;           //버튼마다 스테이지 데이터를 받아온다
    //버튼 onclick 전용 메서드
    public void SetSelectedStage()
    {
        StageDataHolder.Instance.selectedStage = stageData;
        SceneManager.LoadScene("MainScenes");
    }
}
