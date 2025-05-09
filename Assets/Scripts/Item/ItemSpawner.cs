using UnityEngine;
using System.Collections;

// �������� ���� �ð����� �����ϴ� ��ũ��Ʈ
// [����]
// - spawnPoints �迭���� SpawnLineLow / Mid / High ������Ʈ�� ������� ���
// - �� ������ �������� �ν����Ϳ��� ����
// - obstacleLayer�� "Obstacle" ���̾ ������ ��
// [��ֹ� ����� ����]
// - ��� ��ֹ� ������Ʈ�� �ݵ�� "Obstacle" ���̾�� �����ؾ� ���� �۵�

public class ItemSpawner : MonoBehaviour
{
    [Header("���� ��ġ (3�� ����)")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("������ ������")]
    [SerializeField] private GameObject scoreItemPrefab;
    [SerializeField] private GameObject healItemPrefab;
    [SerializeField] private GameObject speedUpItemPrefab;
    [SerializeField] private GameObject speedDownItemPrefab;

    [Header("��ֹ� ���̾� ����ũ")]
    [SerializeField] private LayerMask obstacleLayer;

    private void Start()
    {
        StartCoroutine(SpawnRoutine(scoreItemPrefab, 1f));       // ���� ������: 1�ʸ���
        StartCoroutine(SpawnRoutine(null, 10f, true));           // �ӵ� ������: 10�ʸ��� ����
        StartCoroutine(SpawnRoutine(healItemPrefab, 15f));       // ü�� ������: 15�ʸ���
    }

    // ���� ���� ��ƾ
    private IEnumerator SpawnRoutine(GameObject fixedItem, float interval, bool isSpeedItem = false)
    {
        while (true)
        {
            GameObject toSpawn = fixedItem;

            // �ӵ� �������� ��� ��/�ٿ� �� �ϳ��� ���� ����
            if (isSpeedItem)
            {
                toSpawn = Random.value > 0.5f ? speedUpItemPrefab : speedDownItemPrefab;
            }

            SpawnItemAtRandomPosition(toSpawn);
            yield return new WaitForSeconds(interval);
        }
    }

    // ��ֹ��� ���� ��ġ���� ������ ���� (�ִ� 10�� �õ�)
    private void SpawnItemAtRandomPosition(GameObject itemPrefab)
    {
        if (spawnPoints.Length == 0 || itemPrefab == null) return;

        const int maxAttempts = 10;
        for (int i = 0; i < maxAttempts; i++)
        {
            int index = Random.Range(0, spawnPoints.Length);
            Vector2 spawnPos = spawnPoints[index].position;

            // �ش� ��ġ�� ��ֹ��� �ִ��� �˻�
            bool isBlocked = Physics2D.OverlapCircle(spawnPos, 0.5f, obstacleLayer);

            if (!isBlocked)
            {
                Instantiate(itemPrefab, spawnPos, Quaternion.identity);
                return;
            }
        }

        Debug.LogWarning("������ ���� ����: ��� ���ο� ��ֹ��� ����");
    }
}
