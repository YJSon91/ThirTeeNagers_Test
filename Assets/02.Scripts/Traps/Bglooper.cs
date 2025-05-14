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
}