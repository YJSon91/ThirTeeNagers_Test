using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUnlockManager : MonoBehaviour
{
    [SerializeField] private Button[] stageButtons;             //스테이지 선택 버튼들 할당




    // Start is called before the first frame update
    private void Start()
    {
        int unlockedStage = PlayerPrefs.GetInt("UnlockedStage", 1);         //언락한 스테이지 값을 playerprefs로 가져옴(없으면 1을 디폴트 값으로 가져옴)

        for(int i = 0;  i < stageButtons.Length; i++)
        {
            stageButtons[i].interactable = i < unlockedStage;       //언락된 스테이지만 활성화(나머지는 투명화 처리)
        }
    }

    public static void UnlockNextStage(int justClearedStageIndex)
    {
        int unlockedStage = PlayerPrefs.GetInt("UnlockedStage", 1);     //언락한 스테이지 값을 playerprefs로 가져옴(없으면 1을 디폴트 값으로 가져옴)
        if (justClearedStageIndex + 1 >= unlockedStage)             //방금 클리어한 스테이지가 현재 해제된 마지막 스테이지 이상일때만 해제
        {
            PlayerPrefs.SetInt("UnlockedStage", justClearedStageIndex + 2);     //스테이지 해제 저장하기
        }
    }

    //리셋 버튼
    public void ResetStageUnlock()
    {
        PlayerPrefs.DeleteKey("UnlockedStage");     //unlockedStage 값만 playerPrefs에서 삭제
        PlayerPrefs.Save();
        Debug.Log("스테이지 잠금 정보가 초기화 되었습니다.");
    }
}
