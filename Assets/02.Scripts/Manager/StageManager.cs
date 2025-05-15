using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public StageData currentStageData;              //스테이지 선택창에서 받아오는 스테이지 데이터
    public List<StageData> stageList;               //다음 스테이지 확인 리스트

    public static StageManager instance;            //인스턴스 화

    [SerializeField] private PlayerHandler player;          //플레이어 정보
    [SerializeField] private GameManager GameManager;       //게임매니저 정보

    //싱글톤 화
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
        //기존 스테이지 정리
        GameObject oldstage = GameObject.Find("StageRoot");         //stageroot 게임오브젝트를 찾고 oldstage에 할당
        //oldstage가 있으면 파괴
        if (oldstage != null)
        {
            Destroy(oldstage);
        }
        //새 스테이지 생성
        GameObject newStage = Instantiate(data.backgroundPrefab);       //데이터의 백그라운드 프리팹을 생성
        currentStageData = data;                                        //현제 스테이지 데이터를 가져옴

        //데이터 적용
        Debug.Log($"[StageManager] Loading Stage: {data.name}, Speed: {data.playerSpeed}, Time: {data.survivalTime}");      //디버그용 로그
        player.transform.position = data.spawnPosition;                 //데이터의 스폰포지션을 토대로 플레이어의 생성 위치를 지정
        player.PlayerSpeed = data.playerSpeed;                          //플레이어 스피드 지정
        GameManager.SetSurvivalTime(data.survivalTime);                 //생존 시간 지정
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


        //스테이지가 로드 될때 생성되있던 장애물들 재배치
        StartCoroutine(DelayedResetObstacles());

        //시작
        GameManager.StageStart();
    }

    public void LoadNextStage()
    {
        GameManager.Instance.CloseStageClearPanel();            //게임매니저에 있는 메서드(스테이지 클리어 패널 닫기) 호출

        Time.timeScale = 1.0f;                                  //시간 흐르게 함
        int index = stageList.IndexOf(currentStageData);        //현재 스테이지 데이터의 인덱스를 찾음
        if(index + 1 < stageList.Count)                         //현재 스테이지가 마지막 스테이지가 아니라면
        {
            //다음 스테이지로 로드
            StageData next = stageList[index + 1];              
            LoadStage(next);

        }
        else
        {
            {
                Debug.Log("마지막 스테이지입니다.");
            }
        }

    }

    //셀렉트 스테이지 씬에서 고른 데이터를 가져옴
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
