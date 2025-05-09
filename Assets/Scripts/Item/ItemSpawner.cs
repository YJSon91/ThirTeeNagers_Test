using UnityEngine;
using System.Collections;

// 아이템을 일정 시간마다 생성하는 스크립트
// [사용법]
// - spawnPoints 배열에는 SpawnLineLow / Mid / High 오브젝트를 순서대로 등록
// - 각 아이템 프리팹은 인스펙터에서 연결
// - obstacleLayer는 "Obstacle" 레이어를 지정할 것
// [장애물 담당자 주의]
// - 모든 장애물 오브젝트는 반드시 "Obstacle" 레이어로 설정해야 정상 작동

public class ItemSpawner : MonoBehaviour
{
    [Header("스폰 위치 (3개 라인)")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("아이템 프리팹")]
    [SerializeField] private GameObject scoreItemPrefab;
    [SerializeField] private GameObject healItemPrefab;
    [SerializeField] private GameObject speedUpItemPrefab;
    [SerializeField] private GameObject speedDownItemPrefab;

    [Header("장애물 레이어 마스크")]
    [SerializeField] private LayerMask obstacleLayer;

    private void Start()
    {
        StartCoroutine(SpawnRoutine(scoreItemPrefab, 1f));       // 점수 아이템: 1초마다
        StartCoroutine(SpawnRoutine(null, 10f, true));           // 속도 아이템: 10초마다 랜덤
        StartCoroutine(SpawnRoutine(healItemPrefab, 15f));       // 체력 아이템: 15초마다
    }

    // 공통 스폰 루틴
    private IEnumerator SpawnRoutine(GameObject fixedItem, float interval, bool isSpeedItem = false)
    {
        while (true)
        {
            GameObject toSpawn = fixedItem;

            // 속도 아이템일 경우 업/다운 중 하나를 랜덤 선택
            if (isSpeedItem)
            {
                toSpawn = Random.value > 0.5f ? speedUpItemPrefab : speedDownItemPrefab;
            }

            SpawnItemAtRandomPosition(toSpawn);
            yield return new WaitForSeconds(interval);
        }
    }

    // 장애물이 없는 위치에만 아이템 생성 (최대 10번 시도)
    private void SpawnItemAtRandomPosition(GameObject itemPrefab)
    {
        if (spawnPoints.Length == 0 || itemPrefab == null) return;

        const int maxAttempts = 10;
        for (int i = 0; i < maxAttempts; i++)
        {
            int index = Random.Range(0, spawnPoints.Length);
            Vector2 spawnPos = spawnPoints[index].position;

            // 해당 위치에 장애물이 있는지 검사
            bool isBlocked = Physics2D.OverlapCircle(spawnPos, 0.5f, obstacleLayer);

            if (!isBlocked)
            {
                Instantiate(itemPrefab, spawnPos, Quaternion.identity);
                return;
            }
        }

        Debug.LogWarning("아이템 스폰 실패: 모든 라인에 장애물이 있음");
    }
}
