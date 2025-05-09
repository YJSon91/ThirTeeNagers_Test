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
    [SerializeField] private Transform[] spawnPoints; // �������� ������ �� �ִ� Y�� ���� ��ġ 3�� (Low, Mid, High)

    [Header("������ ������")]
    [SerializeField] private GameObject scoreItemPrefab;     // ���� ������ ������
    [SerializeField] private GameObject healItemPrefab;      // ü�� ȸ�� ������ ������
    [SerializeField] private GameObject speedUpItemPrefab;   // �ӵ� ���� ������ ������
    [SerializeField] private GameObject speedDownItemPrefab; // �ӵ� ���� ������ ������

    [Header("��ֹ� ���̾� ����ũ")]
    [SerializeField] private LayerMask obstacleLayer; // ��ֹ��� ��ġ�� �ʰ� �ϱ� ���� ���̾� ����ũ ("Obstacle"�� �����ؾ� ��)

    private void Start()
    {
        // ���� �������� 1�ʸ��� ������
        StartCoroutine(SpawnRoutine(scoreItemPrefab, 1f));

        // �ӵ� �������� 10�ʸ��� �����Ǹ�, ��/�ٿ� �� �����ϰ� ���õ�
        StartCoroutine(SpawnRoutine(null, 10f, true));

        // ü�� �������� 15�ʸ��� ������
        StartCoroutine(SpawnRoutine(healItemPrefab, 15f));
    }

    // Ư�� �������� ���� �ð� �������� �����ϴ� ��ƾ
    // fixedItem: ������ ������ ������ (null�̸� isSpeedItem�� true���� ��)
    // interval: �� �ʸ��� ��������
    // isSpeedItem: true�̸� �ӵ� ������ �� ���� ���� (Up/Down)
    private IEnumerator SpawnRoutine(GameObject fixedItem, float interval, bool isSpeedItem = false)
    {
        while (true)
        {
            GameObject toSpawn = fixedItem;

            // �ӵ� �������� ��� ��/�ٿ� �� �ϳ��� �������� ����
            if (isSpeedItem)
            {
                toSpawn = Random.value > 0.5f ? speedUpItemPrefab : speedDownItemPrefab;
            }

            // ���õ� �������� ������ ��ġ�� ����
            SpawnItemAtRandomPosition(toSpawn);

            // ���� �������� ���
            yield return new WaitForSeconds(interval);
        }
    }

    // ���� �������� ������ ��ġ�� �����ϴ� �Լ�
    // ��, �ش� ��ġ�� ��ֹ��� ���� ��� �ִ� 10������ �ٸ� ��ġ�� �õ���
    private void SpawnItemAtRandomPosition(GameObject itemPrefab)
    {
        // ���� ������ ����ְų� �������� ������ �ƹ� �͵� ���� ����
        if (spawnPoints.Length == 0 || itemPrefab == null) return;

        const int maxAttempts = 10; // ��ֹ��� ��ġ�� �ʵ��� �ִ� 10�� �õ�

        for (int i = 0; i < maxAttempts; i++)
        {
            int index = Random.Range(0, spawnPoints.Length); // 3�� ���� �� �ϳ� ���� ����
            Vector2 spawnPos = spawnPoints[index].position;  // �ش� ��ġ ��ǥ ��������

            // �ش� ��ġ�� ��ֹ��� �ִ��� Ȯ�� (OverlapCircle�� ����)
            bool isBlocked = Physics2D.OverlapCircle(spawnPos, 0.5f, obstacleLayer);

            // ��ֹ��� ������ ������ ���� �� �Լ� ����
            if (!isBlocked)
            {
                Instantiate(itemPrefab, spawnPos, Quaternion.identity);
                return;
            }
        }

        // ��� ���ο� ��ֹ��� �־� ���� ������ ��� ��� ���
        Debug.LogWarning("������ ���� ����: ��� ���ο� ��ֹ��� ����");
    }
}
