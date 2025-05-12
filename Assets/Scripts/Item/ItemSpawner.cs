using UnityEngine;
using System.Collections;

/// <summary>
/// [ItemSpawner.cs]
/// 플레이어 기준 일정 거리 앞에서 아이템을 주기적으로 생성하는 스크립트
/// - 점수, 체력, 속도 아이템을 각기 다른 주기로 생성
/// - 생성 위치는 Y축 라인 중 무작위 선택, X축은 플레이어 기준으로 고정 거리 앞
/// - 해당 위치에 기존 아이템이 있을 경우 기존 아이템을 삭제 후 새 아이템 생성
/// </summary>
public class ItemSpawner : MonoBehaviour
{
    [Header("스폰 위치 (3개 라인, Y축 기준)")]
    [SerializeField] private Transform[] spawnPoints;
    // Y축 기준으로 아이템을 배치할 수 있는 위치들 (Low, Mid, High 라인)

    [Header("아이템 프리팹")]
    [SerializeField] private GameObject scoreItemPrefab;     // 점수 아이템 프리팹
    [SerializeField] private GameObject healItemPrefab;      // 체력 회복 아이템 프리팹
    [SerializeField] private GameObject speedUpItemPrefab;   // 속도 증가 아이템 프리팹
    [SerializeField] private GameObject speedDownItemPrefab; // 속도 감소 아이템 프리팹

    [Header("플레이어 및 충돌 설정")]
    [SerializeField] private Transform playerTransform;       // 기준이 되는 플레이어 위치
    [SerializeField] private float spawnOffsetX = 10f;        // 플레이어보다 X축으로 앞에 생성될 거리
    [SerializeField] private LayerMask obstacleLayer;         // 장애물 감지용 레이어 ("Obstacle")
    [SerializeField] private LayerMask itemLayer;             // 기존 아이템 감지용 레이어 ("Item")

    private void Start()
    {
        // 점수 아이템: 0.2초마다 매우 자주 생성
        StartCoroutine(SpawnRoutine(scoreItemPrefab, 0.2f));

        // 속도 아이템: 10초마다 생성되며, 업/다운 중 무작위 선택
        StartCoroutine(SpawnRoutine(null, 10f, true));

        // 체력 아이템: 30초마다 생성
        StartCoroutine(SpawnRoutine(healItemPrefab, 30f));
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
    /// - 지정된 간격마다 아이템을 생성
    /// - 속도 아이템인 경우 업/다운 중 하나를 무작위로 선택
    /// </summary>
    private IEnumerator SpawnRoutine(GameObject fixedItem, float interval, bool isSpeedItem = false)
    {
        yield return new WaitForSeconds(interval); // 초기 몰림 방지를 위한 대기

        while (true)
        {
            GameObject toSpawn = fixedItem;

            // 속도 아이템이면 업/다운 중 랜덤 선택
            if (isSpeedItem)
            {
                toSpawn = Random.value > 0.5f ? speedUpItemPrefab : speedDownItemPrefab;
            }

            SpawnItemAtRandomPosition(toSpawn);
            yield return new WaitForSeconds(interval);
        }
    }

    /// <summary>
    /// 아이템을 Y라인 중 무작위 위치에 생성
    /// - 해당 위치에 장애물이 없을 경우만 생성
    /// - 해당 위치에 기존 아이템이 있다면 삭제 후 새로 생성
    /// </summary>
    private void SpawnItemAtRandomPosition(GameObject itemPrefab)
    {
        if (spawnPoints.Length == 0 || itemPrefab == null) return;

        const int maxAttempts = 10;

        for (int i = 0; i < maxAttempts; i++)
        {
            int index = Random.Range(0, spawnPoints.Length); // Y 라인 중 하나 선택
            float spawnY = spawnPoints[index].position.y;
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
            Instantiate(itemPrefab, spawnPos, Quaternion.identity);
            return;
        }

        Debug.LogWarning("아이템 스폰 실패: 모든 라인에 장애물이 있음");
    }
}
