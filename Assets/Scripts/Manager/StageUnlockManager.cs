using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUnlockManager : MonoBehaviour
{
    [SerializeField] private Button[] stageButtons;




    // Start is called before the first frame update
    private void Start()
    {
        int unlockedStage = PlayerPrefs.GetInt("UnlockedStage", 1);

        for(int i = 0;  i < stageButtons.Length; i++)
        {
            stageButtons[i].interactable = i < unlockedStage;
        }
    }

    public static void UnlockNextStage(int justClearedStageIndex)
    {
        int unlockedStage = PlayerPrefs.GetInt("UnlockedStage", 1);
        if(justClearedStageIndex + 1 >= unlockedStage)
        {
            PlayerPrefs.SetInt("UnlockedStage", justClearedStageIndex + 2);
        }
    }

    public void ResetStageUnlock()
    {
        PlayerPrefs.DeleteKey("UnlockedStage");
        PlayerPrefs.Save();
        Debug.Log("스테이지 잠금 정보가 초기화 되었습니다.");
    }
}
