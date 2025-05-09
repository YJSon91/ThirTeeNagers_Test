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
    [SerializeField] private Transform[] spawnPoints; // 아이템이 생성될 수 있는 Y축 기준 위치 3개 (Low, Mid, High)

    [Header("아이템 프리팹")]
    [SerializeField] private GameObject scoreItemPrefab;     // 점수 아이템 프리팹
    [SerializeField] private GameObject healItemPrefab;      // 체력 회복 아이템 프리팹
    [SerializeField] private GameObject speedUpItemPrefab;   // 속도 증가 아이템 프리팹
    [SerializeField] private GameObject speedDownItemPrefab; // 속도 감소 아이템 프리팹

    [Header("장애물 레이어 마스크")]
    [SerializeField] private LayerMask obstacleLayer; // 장애물과 겹치지 않게 하기 위한 레이어 마스크 ("Obstacle"로 지정해야 함)

    private void Start()
    {
        // 점수 아이템은 1초마다 생성됨
        StartCoroutine(SpawnRoutine(scoreItemPrefab, 1f));

        // 속도 아이템은 10초마다 생성되며, 업/다운 중 랜덤하게 선택됨
        StartCoroutine(SpawnRoutine(null, 10f, true));

        // 체력 아이템은 15초마다 생성됨
        StartCoroutine(SpawnRoutine(healItemPrefab, 15f));
    }

    // 특정 아이템을 일정 시간 간격으로 생성하는 루틴
    // fixedItem: 생성할 아이템 프리팹 (null이면 isSpeedItem이 true여야 함)
    // interval: 몇 초마다 생성할지
    // isSpeedItem: true이면 속도 아이템 중 랜덤 선택 (Up/Down)
    private IEnumerator SpawnRoutine(GameObject fixedItem, float interval, bool isSpeedItem = false)
    {
        while (true)
        {
            GameObject toSpawn = fixedItem;

            // 속도 아이템인 경우 업/다운 중 하나를 랜덤으로 선택
            if (isSpeedItem)
            {
                toSpawn = Random.value > 0.5f ? speedUpItemPrefab : speedDownItemPrefab;
            }

            // 선택된 아이템을 랜덤한 위치에 스폰
            SpawnItemAtRandomPosition(toSpawn);

            // 다음 생성까지 대기
            yield return new WaitForSeconds(interval);
        }
    }

    // 실제 아이템을 랜덤한 위치에 생성하는 함수
    // 단, 해당 위치에 장애물이 있을 경우 최대 10번까지 다른 위치를 시도함
    private void SpawnItemAtRandomPosition(GameObject itemPrefab)
    {
        // 스폰 지점이 비어있거나 프리팹이 없으면 아무 것도 하지 않음
        if (spawnPoints.Length == 0 || itemPrefab == null) return;

        const int maxAttempts = 10; // 장애물과 겹치지 않도록 최대 10번 시도

        for (int i = 0; i < maxAttempts; i++)
        {
            int index = Random.Range(0, spawnPoints.Length); // 3개 라인 중 하나 랜덤 선택
            Vector2 spawnPos = spawnPoints[index].position;  // 해당 위치 좌표 가져오기

            // 해당 위치에 장애물이 있는지 확인 (OverlapCircle로 감지)
            bool isBlocked = Physics2D.OverlapCircle(spawnPos, 0.5f, obstacleLayer);

            // 장애물이 없으면 아이템 생성 후 함수 종료
            if (!isBlocked)
            {
                Instantiate(itemPrefab, spawnPos, Quaternion.identity);
                return;
            }
        }

        // 모든 라인에 장애물이 있어 스폰 실패한 경우 경고 출력
        Debug.LogWarning("아이템 스폰 실패: 모든 라인에 장애물이 있음");
    }
}
