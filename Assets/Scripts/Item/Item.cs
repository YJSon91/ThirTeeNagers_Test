using UnityEngine;

// ������ Ÿ�� ����
public enum ItemType
{
    Score,      // ���� ������
    Heal,       // ü�� ȸ�� ������
    SpeedUp,    // �ӵ� ���� ������
    SpeedDown   // �ӵ� ���� ������
}

// �÷��̾ �����ۿ� ����� �� ȿ���� �����ϴ� ��ũ��Ʈ
public class Item : MonoBehaviour
{
    [SerializeField] private ItemType itemType; // �ν����Ϳ��� ������ Ÿ�� ����

    private void Start()
    {
        // �������� ������ �� 5�ʰ� ������ �ڵ����� �ı��� (�÷��̾ �� �Ծ��� ��� ���)
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ����� Player �±װ� �ƴϸ� ����
        if (!collision.CompareTag("Player")) return;

        // �÷��̾��� ü�� ���� �������� (PlayerState ��ũ��Ʈ)
        PlayerState playerState = collision.GetComponent<PlayerState>();

        // �÷��̾��� �ӵ� ���� ���� �������� (PlayerItemInteraction ��ũ��Ʈ)
        PlayerItemInteraction itemInteraction = collision.GetComponent<PlayerItemInteraction>();

        // �� �� �ϳ��� ������ ȿ�� �������� �ʰ� ����
        if (playerState == null || itemInteraction == null) return;

        // ������ ������ ���� �ٸ� ȿ�� ����
        switch (itemType)
        {
            case ItemType.Score:
                GameManager.Instance.AddScore(1); // ���� +1 ���� (GameManager�� AddScore �Լ� ȣ��)
                break;

            case ItemType.Heal:
                playerState.CurrentHealth += 1; // ü�� +1 ���� (�ִ�ġ ������ PlayerState���� ó��)
                break;

            case ItemType.SpeedUp:
                itemInteraction.ChangeMovementSpeed(true); // �ӵ� ���� ȿ�� (1.5��)
                break;

            case ItemType.SpeedDown:
                itemInteraction.ChangeMovementSpeed(false); // �ӵ� ���� ȿ�� (0.5��)
                break;
        }

        // ȿ�� ���� �� �������� ��� ����
        Destroy(gameObject);
    }
}
