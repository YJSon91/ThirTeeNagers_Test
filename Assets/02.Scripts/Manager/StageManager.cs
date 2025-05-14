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
    [SerializeField] private AudioSource audioSource;       //bgm ����� ����� �ҽ�

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

        //����� ���
        audioSource.clip = data.bgm;
        audioSource.Play();

        //���������� �ε� �ɶ� �������ִ� ��ֹ��� ���ġ
        FindObjectOfType<Bglooper>()?.ResetObstacles();

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

    
}
