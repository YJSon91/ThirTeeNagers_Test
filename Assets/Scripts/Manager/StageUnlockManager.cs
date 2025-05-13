using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUnlockManager : MonoBehaviour
{
    [SerializeField] private Button[] stageButtons;             //�������� ���� ��ư�� �Ҵ�




    // Start is called before the first frame update
    private void Start()
    {
        int unlockedStage = PlayerPrefs.GetInt("UnlockedStage", 1);         //����� �������� ���� playerprefs�� ������(������ 1�� ����Ʈ ������ ������)

        for(int i = 0;  i < stageButtons.Length; i++)
        {
            stageButtons[i].interactable = i < unlockedStage;       //����� ���������� Ȱ��ȭ(�������� ����ȭ ó��)
        }
    }

    public static void UnlockNextStage(int justClearedStageIndex)
    {
        int unlockedStage = PlayerPrefs.GetInt("UnlockedStage", 1);     //����� �������� ���� playerprefs�� ������(������ 1�� ����Ʈ ������ ������)
        if (justClearedStageIndex + 1 >= unlockedStage)             //��� Ŭ������ ���������� ���� ������ ������ �������� �̻��϶��� ����
        {
            PlayerPrefs.SetInt("UnlockedStage", justClearedStageIndex + 2);     //�������� ���� �����ϱ�
        }
    }

    //���� ��ư
    public void ResetStageUnlock()
    {
        PlayerPrefs.DeleteKey("UnlockedStage");     //unlockedStage ���� playerPrefs���� ����
        PlayerPrefs.Save();
        Debug.Log("�������� ��� ������ �ʱ�ȭ �Ǿ����ϴ�.");
    }
}
