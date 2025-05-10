using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// itemSpawner.cs 에서 복사한 스크립트
/// obstacleSpawner에 맞게 수정해보기
/// </summary>
public class ObstacleSpawner : MonoBehaviour
{
    // Y축 기준으로 아이템을 배치할 수 있는 위치들 (인스펙터에서 spawnPoints 추가해서 지정할 수 있음)
    [Header("스폰 위치 (3개 라인, Y축 기준)")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private LayerMask obstacleLayer;         // 장애물 감지용 레이어 ("Obstacle")
    [SerializeField] private LayerMask itemLayer;             // 기존 아이템 감지용 레이어 ("Item")

    /// <summary>
    /// Fire,Saw,Spike,SpikeHead에 맞게 프리팹 연결
    /// </summary>
    [Header("아이템 프리팹")]
    [SerializeField] private GameObject firePrefab;     
    [SerializeField] private GameObject sawPrefab;      
    [SerializeField] private GameObject spikePrefab;   
    [SerializeField] private GameObject spikeHeadPrefab;
    private ObastacleType obstacleType;

    /// <summary>
    /// 아이템에서 장애물 감지하기 때문에 아이템레이어는 따로 지정하지 않음
    /// 아이템보다 빠르게 생성하기 위해서 spawnOffsetX 10->8로 수정
    /// </summary>
    [Header("플레이어 및 충돌 설정")]
    [SerializeField] private Transform playerTransform;       // 기준이 되는 플레이어 위치
    [SerializeField] private float spawnOffsetX = 8f;        // 플레이어보다 X축으로 앞에 생성될 거리
    //[SerializeField] private LayerMask obstacleLayer;         // 장애물 감지용 레이어 ("Obstacle")
    //[SerializeField] private LayerMask itemLayer;             // 기존 아이템 감지용 레이어 ("Item")

    private void Start()
    {
        // 점수 아이템: 0.2초마다 매우 자주 생성
        StartCoroutine(SpawnRoutine(firePrefab, 2.1f));

        // 속도 아이템: 10초마다 생성되며, 업/다운 중 무작위 선택
        StartCoroutine(SpawnRoutine(sawPrefab,10.1f));

        // 체력 아이템: 30초마다 생성
        StartCoroutine(SpawnRoutine(spikePrefab, 0.9f));

        // 체력 아이템: 30초마다 생성
        StartCoroutine(SpawnRoutine(spikeHeadPrefab, 5f));
    }

    private void LateUpdate()
    {
        // 스포너 오브젝트를 항상 플레이어 앞쪽으로 이동
        if (playerTransform != null)
        {
            Vector3 newPos = transform.position;
            newPos.x = playerTransform.position.x + spawnOffsetX;
            transform.position = newPos;
        }
    }

    /// <summary>
    /// 공통 스폰 루틴
    /// - 지정된 간격마다 장애물을 생성
    /// 
    /// </summary>
    private IEnumerator SpawnRoutine(GameObject fixedItem, float interval, bool isSpeedItem = false)
    {
        yield return new WaitForSeconds(interval); // 초기 몰림 방지를 위한 대기

        while (true)
        {
            GameObject spawnObstacle = fixedItem;
            // 생성된 프리팹에서 장애물 타입을 가져옴
            // 이 부분은 실제로는 프리팹에서 스크립트를 통해 가져오는 것이 좋음
            obstacleType = spawnObstacle.GetComponent<Obstacle>().obstacleType;


            switch (obstacleType)
            {
                case ObastacleType.Fire:
                    SpawnFireRandomPosition(spawnObstacle);
                    yield return new WaitForSeconds(interval);
                    break;

                case ObastacleType.Saw:
                    SpawnSawRandomPosition(spawnObstacle);
                    yield return new WaitForSeconds(interval);
                    break;

                case ObastacleType.Spike:
                    SpawnSpikeRandomPosition(spawnObstacle);
                    yield return new WaitForSeconds(interval);
                    break;

                case ObastacleType.SpikeHead:
                    SpawnSpikeHeadRandomPosition(spawnObstacle);
                    yield return new WaitForSeconds(interval);
                    break;
            }
        }
    }

    /// <summary>
    /// 아이템을 장애물타입 별로 높이 조절해 생성할 수 있도록 수정(아이템스크립트 참고하면 될듯)
    /// - 해당 위치에 장애물이 없을 경우만 생성
    /// - 해당 위치에 기존 아이템이 있다면 삭제 후 새로 생성
    /// </summary>
    private void SpawnFireRandomPosition(GameObject obstaclePrefab)
    {
        if (spawnPoints.Length == 0 || obstaclePrefab == null) return;

        const int maxAttempts = 10;

        for (int i = 0; i < maxAttempts; i++)
        {
           
            float spawnY = spawnPoints[1].position.y;
            float spawnX = transform.position.x;

            Vector2 spawnPos = new Vector2(spawnX, spawnY);

            // 해당 위치에 장애물이 있으면 스킵
            bool isBlocked = Physics2D.OverlapCircle(spawnPos, 0.5f, obstacleLayer);
            if (isBlocked) continue;

            // 해당 위치에 기존 아이템이 있으면 삭제
            Collider2D overlap = Physics2D.OverlapCircle(spawnPos, 0.5f, itemLayer);
            if (overlap != null)
            {
                Destroy(overlap.gameObject); // 먼저 있던 아이템 제거
            }
            // 아이템 생성
            Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
            return;
        }

        Debug.LogWarning("아이템 스폰 실패: 모든 라인에 장애물이 있음");
    }
    private void SpawnSawRandomPosition(GameObject obstaclePrefab)
    {
        if (spawnPoints.Length == 0 || obstaclePrefab == null) return;

        const int maxAttempts = 10;

        for (int i = 0; i < maxAttempts; i++)
        {
            
            float spawnY = spawnPoints[0].position.y; //Saw은 0라인에만 생성
            float spawnX = transform.position.x;

            Vector2 spawnPos = new Vector2(spawnX, spawnY);

            // 해당 위치에 장애물이 있으면 스킵
            bool isBlocked = Physics2D.OverlapCircle(spawnPos, 0.5f, obstacleLayer);
            if (isBlocked) continue;

            // 해당 위치에 기존 아이템이 있으면 삭제
            Collider2D overlap = Physics2D.OverlapCircle(spawnPos, 0.5f, itemLayer);
            if (overlap != null)
            {
                Destroy(overlap.gameObject); // 먼저 있던 아이템 제거
            }
            // 아이템 생성
            Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
            return;
        }

        Debug.LogWarning("아이템 스폰 실패: 모든 라인에 장애물이 있음");
    }
    private void SpawnSpikeRandomPosition(GameObject obstaclePrefab)
    {
        if (spawnPoints.Length == 0 || obstaclePrefab == null) return;

        const int maxAttempts = 10;

        for (int i = 0; i < maxAttempts; i++)
        {

            float spawnY = -1.2f;
            float spawnX = transform.position.x;

            Vector2 spawnPos = new Vector2(spawnX, spawnY);

            // 해당 위치에 장애물이 있으면 스킵
            bool isBlocked = Physics2D.OverlapCircle(spawnPos, 0.5f, obstacleLayer);
            if (isBlocked) continue;

            // 해당 위치에 기존 아이템이 있으면 삭제
            Collider2D overlap = Physics2D.OverlapCircle(spawnPos, 0.5f, itemLayer);
            if (overlap != null)
            {
                Destroy(overlap.gameObject); // 먼저 있던 아이템 제거
            }
            // 아이템 생성
            Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
            return;
        }

        Debug.LogWarning("아이템 스폰 실패: 모든 라인에 장애물이 있음");
    }
    private void SpawnSpikeHeadRandomPosition(GameObject obstaclePrefab)
    {
        if (spawnPoints.Length == 0 || obstaclePrefab == null) return;

        const int maxAttempts = 10;

        for (int i = 0; i < maxAttempts; i++)
        {
            int index = Random.Range(2, spawnPoints.Length); // Y 라인 중 하나 선택
            float spawnY = spawnPoints[index].position.y; //SpikeHead은 2라인 위에만 생성
            float spawnX = transform.position.x;

            Vector2 spawnPos = new Vector2(spawnX, spawnY);
            // 해당 위치에 장애물이 있으면 스킵
            bool isBlocked = Physics2D.OverlapCircle(spawnPos, 0.5f, obstacleLayer);
            if (isBlocked) continue;

            // 해당 위치에 기존 아이템이 있으면 삭제
            Collider2D overlap = Physics2D.OverlapCircle(spawnPos, 0.5f, itemLayer);
            if (overlap != null)
            {
                Destroy(overlap.gameObject); // 먼저 있던 아이템 제거
            }

            // 아이템 생성
            Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
            return;
        }

        Debug.LogWarning("아이템 스폰 실패: 모든 라인에 장애물이 있음");
    }
}
