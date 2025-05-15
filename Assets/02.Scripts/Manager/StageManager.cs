using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public StageData currentStageData;              //�������� ����â���� �޾ƿ��� �������� ������
    public List<StageData> stageList;               //���� �������� Ȯ�� ����Ʈ

    public static StageManager instance;            //�ν��Ͻ� ȭ

    [SerializeField] private PlayerHandler player;          //�÷��̾� ����
    [SerializeField] private GameManager GameManager;       //���ӸŴ��� ����

    //�̱��� ȭ
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadStage(StageData data)
    {
        if(Camera.main != null)
        {
            Camera.main.backgroundColor = data.backgroundColor;
        }
        //���� �������� ����
        GameObject oldstage = GameObject.Find("StageRoot");         //stageroot ���ӿ�����Ʈ�� ã�� oldstage�� �Ҵ�
        //oldstage�� ������ �ı�
        if (oldstage != null)
        {
            Destroy(oldstage);
        }
        //�� �������� ����
        GameObject newStage = Instantiate(data.backgroundPrefab);       //�������� ��׶��� �������� ����
        currentStageData = data;                                        //���� �������� �����͸� ������

        //������ ����
        Debug.Log($"[StageManager] Loading Stage: {data.name}, Speed: {data.playerSpeed}, Time: {data.survivalTime}");      //����׿� �α�
        player.transform.position = data.spawnPosition;                 //�������� ������������ ���� �÷��̾��� ���� ��ġ�� ����
        player.PlayerSpeed = data.playerSpeed;                          //�÷��̾� ���ǵ� ����
        GameManager.SetSurvivalTime(data.survivalTime);                 //���� �ð� ����
        GameManager.SetCurrentStage(data.stageNumber);

        if (data.stageNumber == 1)
        {
            GameManager.Instance.Pause();
            GameManager.Instance.TutorialBoard.SetActive(true);
        }
        else
        {
            GameManager.Instance.TutorialBoard.SetActive(false);
        }


        //���������� �ε� �ɶ� �������ִ� ��ֹ��� ���ġ
        StartCoroutine(DelayedResetObstacles());

        //����
        GameManager.StageStart();
    }

    public void LoadNextStage()
    {
        GameManager.Instance.CloseStageClearPanel();            //���ӸŴ����� �ִ� �޼���(�������� Ŭ���� �г� �ݱ�) ȣ��

        Time.timeScale = 1.0f;                                  //�ð� �帣�� ��
        int index = stageList.IndexOf(currentStageData);        //���� �������� �������� �ε����� ã��
        if(index + 1 < stageList.Count)                         //���� ���������� ������ ���������� �ƴ϶��
        {
            //���� ���������� �ε�
            StageData next = stageList[index + 1];              
            LoadStage(next);

        }
        else
        {
            {
                Debug.Log("������ ���������Դϴ�.");
            }
        }

    }

    //����Ʈ �������� ������ �� �����͸� ������
    public void OnSelectStage(StageData selectData)
    {
        LoadStage(selectData);
    }


    private IEnumerator DelayedResetObstacles()
    {
        yield return null;
        FindObjectOfType<Bglooper>().ResetObstacles();
    }
    
}
