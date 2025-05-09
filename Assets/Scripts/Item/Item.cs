using UnityEngine;

// ������ Ÿ�� ����
public enum ItemType
{
    Score,
    Heal,
    SpeedUp,
    SpeedDown
}

// �÷��̾ �����ۿ� ����� �� ȿ���� �����ϴ� ��ũ��Ʈ
public class Item : MonoBehaviour
{
    [SerializeField] private ItemType itemType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerState playerState = collision.GetComponent<PlayerState>();
        PlayerItemInteraction itemInteraction = collision.GetComponent<PlayerItemInteraction>();

        if (playerState == null || itemInteraction == null) return;

        switch (itemType)
        {
            case ItemType.Score:
                GameManager.Instance.AddScore(1); // ���� +1
                break;

            case ItemType.Heal:
                playerState.CurrentHealth += 1; // ü�� +1
                break;

            case ItemType.SpeedUp:
                itemInteraction.ChangeMovementSpeed(true); // �ӵ� ����
                break;

            case ItemType.SpeedDown:
                itemInteraction.ChangeMovementSpeed(false); // �ӵ� ����
                break;
        }

        Destroy(gameObject); // ������ ����
    }
}
