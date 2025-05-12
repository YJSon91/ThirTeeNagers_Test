using UnityEngine;
using System.Collections;

/// <summary>
/// [ItemSpawner.cs]
/// �÷��̾� ���� ���� �Ÿ� �տ��� �������� �ֱ������� �����ϴ� ��ũ��Ʈ
/// - ����, ü��, �ӵ� �������� ���� �ٸ� �ֱ�� ����
/// - ���� ��ġ�� Y�� ���� �� ������ ����, X���� �÷��̾� �������� ���� �Ÿ� ��
/// - �ش� ��ġ�� ���� �������� ���� ��� ���� �������� ���� �� �� ������ ����
/// </summary>
public class ItemSpawner : MonoBehaviour
{
    [Header("���� ��ġ (3�� ����, Y�� ����)")]
    [SerializeField] private Transform[] spawnPoints;
    // Y�� �������� �������� ��ġ�� �� �ִ� ��ġ�� (Low, Mid, High ����)

    [Header("������ ������")]
    [SerializeField] private GameObject scoreItemPrefab;     // ���� ������ ������
    [SerializeField] private GameObject healItemPrefab;      // ü�� ȸ�� ������ ������
    [SerializeField] private GameObject speedUpItemPrefab;   // �ӵ� ���� ������ ������
    [SerializeField] private GameObject speedDownItemPrefab; // �ӵ� ���� ������ ������

    [Header("�÷��̾� �� �浹 ����")]
    [SerializeField] private Transform playerTransform;       // ������ �Ǵ� �÷��̾� ��ġ
    [SerializeField] private float spawnOffsetX = 10f;        // �÷��̾�� X������ �տ� ������ �Ÿ�
    [SerializeField] private LayerMask obstacleLayer;         // ��ֹ� ������ ���̾� ("Obstacle")
    [SerializeField] private LayerMask itemLayer;             // ���� ������ ������ ���̾� ("Item")

    private void Start()
    {
        // ���� ������: 0.2�ʸ��� �ſ� ���� ����
        StartCoroutine(SpawnRoutine(scoreItemPrefab, 0.2f));

        // �ӵ� ������: 10�ʸ��� �����Ǹ�, ��/�ٿ� �� ������ ����
        StartCoroutine(SpawnRoutine(null, 10f, true));

        // ü�� ������: 30�ʸ��� ����
        StartCoroutine(SpawnRoutine(healItemPrefab, 30f));
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

            // �ӵ� �������̸� ��/�ٿ� �� ���� ����
            if (isSpeedItem)
            {
                toSpawn = Random.value > 0.5f ? speedUpItemPrefab : speedDownItemPrefab;
            }

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
