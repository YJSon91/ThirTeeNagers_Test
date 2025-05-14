using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
using System.Linq;

public class Bglooper : MonoBehaviour
{


    public int obstacleCount = 0; // 장애물의 개수
    public Vector3 obstacleLastPosition = Vector3.zero; // 마지막으로 배치된 장애물의 위치
    public int numBgCount = 3;
    public int numGroundCount = 3;


    void Start()
    {
        List<Obstacle> obstacleList = GameObject.FindObjectsOfType<Obstacle>().ToList();
        obstacleCount = obstacleList.Count;

        // 시작 위치는 첫 번째 오브젝트 기준
        obstacleLastPosition = obstacleList[0].transform.position;

        for (int i = 0; i < obstacleCount; i++)
        {
            // 남은 오브젝트 중 랜덤하게 하나 선택
            int randIndex = Random.Range(0, obstacleList.Count);
            Obstacle selected = obstacleList[randIndex];

            // 배치
            obstacleLastPosition = selected.SetRandomPlace(obstacleLastPosition, obstacleCount);

            // 선택한 오브젝트는 리스트에서 제거
            obstacleList.RemoveAt(randIndex);
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체의 이름을 디버그 로그에 출력
        // Debug.Log("Triggered: " + collision.name);
        
        if (collision.CompareTag("Ground"))
        {
            Debug.Log("Ground Check.");
            float widthOfBgObject = ((BoxCollider2D)collision).size.x/2;
            Debug.Log("widthOfBgObject: " + widthOfBgObject);
            Vector3 pos = collision.transform.position;
            
            pos.x += widthOfBgObject * numGroundCount;
            collision.transform.position = pos;
            return;
        }
        else if (collision.CompareTag("BackGround"))
        {
            Debug.Log("Bg Check.");
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;
            return;
        }
        // 충돌한 객체가 Obstacle인지 확인
        Obstacle obstacle = collision.GetComponentInParent<Obstacle>();
        if (obstacle)
        {
            Debug.Log("Object Check");
            // 장애물이 충돌 시 랜덤 위치로 재배치
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);

        }
    }
    //스테이지 로드시 장애물 재배치하는 함수
    public void ResetObstacles()
    {
        List<Obstacle> obstacleList = GameObject.FindObjectsOfType<Obstacle>().ToList();        //스테이지 안에 있는 장애물을 전부 찾아 리스트로 만듦

        //장애물이 없는지 확인
        if(obstacleList.Count== 0)
        {
            Debug.LogWarning("[Bglooper] 장애물이 하나도 없습니다.");
            return;
        }

        Transform player = GameObject.FindWithTag("Player")?.transform; //태그 기반으로 플레이어 찾음
        //없으면 로그
        if(player == null)
        {
            Debug.Log("[Bglooper]플레이어를 찾을 수 없습니다.");
            return;
        }

        obstacleCount = obstacleList.Count;         //재배치 할 장애물 수
        obstacleLastPosition = player.position + new Vector3(10f, 0, 0);        //플레이어 보다 앞쪽(오른쪽)부터 장애물 배치 시작


        //장애물 위치 재배치
        for(int i = 0; i< obstacleCount; i++)       //obstacleCount(장애물 수)만큼 반복
        {   
            //장애물 리스트에서 랜덤하게 하나 꺼냄
            int randIndex = Random.Range(0, obstacleList.Count);        
            Obstacle selected = obstacleList[randIndex];

            //꺼내진 장애물을 이전 장애물 위치를 기준으로 일정 거리 떨어진 위치에 배치함(SetRandomPlace에서 return 된 placePosition은 다음 장애물의 기준 위치로 사용)
            obstacleLastPosition = selected.SetRandomPlace(obstacleLastPosition,obstacleCount);

            obstacleList.RemoveAt(randIndex);       //리스트에서 해당 장애물을 제거하여 중복 방지
        }
        Debug.Log($"[Bglooper] 장애물 {obstacleCount}개 재배치 완료.");
    }
}