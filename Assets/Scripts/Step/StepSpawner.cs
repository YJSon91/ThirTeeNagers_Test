using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    [Header("���� ��ġ (3�� ����, Y�� ����)")]
    [SerializeField] private Transform[] spawnPoints;
    // Y�� �������� �������� ��ġ�� �� �ִ� ��ġ�� (Low, Mid, High ����)

    [Header("������ ������")]
    [SerializeField] private GameObject bitStep;     // ���� ����
    [SerializeField] private GameObject smallStep;      // ���� ����

  

    [Header("�÷��̾� �� �浹 ����")]
    [SerializeField] private Transform playerTransform;       // ������ �Ǵ� �÷��̾� ��ġ
    [SerializeField] private float spawnOffsetX = 10f;        // �÷��̾�� X������ �տ� ������ �Ÿ�
    [SerializeField] private LayerMask obstacleLayer;         // ��ֹ� ������ ���̾� ("Obstacle")
    [SerializeField] private LayerMask itemLayer;             // ���� ������ ������ ���̾� ("Item")

    private void Start()
    {
        // // ���� ���� 2�ʸ��� ����
        StartCoroutine(SpawnRoutine(bitStep, 4f));



        //  ���� ���� 2�ʸ��� ����
        StartCoroutine(SpawnRoutine(smallStep, 2f));
    }

    private void LateUpdate()
    {
        // ������ ������Ʈ�� �׻� �÷��̾� �������� �̵�
        if (playerTransform != null)
        {
            Vector3 newPos = transform.position;
            newPos.x = playerTransform.position.x + spawnOffsetX;
            transform.position = newPos;
        }
    }

    /// <summary>
    /// ���� ���� ��ƾ
    /// - ������ ���ݸ��� �������� ����
    /// - �ӵ� �������� ��� ��/�ٿ� �� �ϳ��� �������� ����
    /// </summary>
    private IEnumerator SpawnRoutine(GameObject fixedItem, float interval, bool isSpeedItem = false)
    {
        yield return new WaitForSeconds(interval); // �ʱ� ���� ������ ���� ���

        while (true)
        {
            GameObject toSpawn = fixedItem;

            SpawnItemAtRandomPosition(toSpawn);
            yield return new WaitForSeconds(interval);
        }
    }

    /// <summary>
    /// �������� Y���� �� ������ ��ġ�� ����
    /// - �ش� ��ġ�� ��ֹ��� ���� ��츸 ����
    /// - �ش� ��ġ�� ���� �������� �ִٸ� ���� �� ���� ����
    /// </summary>
    private void SpawnItemAtRandomPosition(GameObject itemPrefab)
    {
        if (spawnPoints.Length == 0 || itemPrefab == null) return;

        const int maxAttempts = 10;

        for (int i = 0; i < maxAttempts; i++)
        {
            int index = Random.Range(0, spawnPoints.Length); // Y ���� �� �ϳ� ����
            float spawnY = spawnPoints[index].position.y;
            float spawnX = transform.position.x;

            Vector2 spawnPos = new Vector2(spawnX, spawnY);

            // �ش� ��ġ�� ��ֹ��� ������ ��ŵ
            bool isBlocked = Physics2D.OverlapCircle(spawnPos, 0.5f, obstacleLayer);
            if (isBlocked) continue;

            // �ش� ��ġ�� ���� �������� ������ ����
            Collider2D overlap = Physics2D.OverlapCircle(spawnPos, 0.5f, itemLayer);
            if (overlap != null)
            {
                Destroy(overlap.gameObject); // ���� �ִ� ������ ����
            }

            // ������ ����
            Instantiate(itemPrefab, spawnPos, Quaternion.identity);
            return;
        }

        Debug.LogWarning("������ ���� ����: ��� ���ο� ��ֹ��� ����");
    }
}

