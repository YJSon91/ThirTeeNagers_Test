using UnityEngine;

// 아이템 타입 정의
public enum ItemType
{
    Score,
    Heal,
    SpeedUp,
    SpeedDown
}

// 플레이어가 아이템에 닿았을 때 효과를 적용하는 스크립트
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
                GameManager.Instance.AddScore(1); // 점수 +1
                break;

            case ItemType.Heal:
                playerState.CurrentHealth += 1; // 체력 +1
                break;

            case ItemType.SpeedUp:
                itemInteraction.ChangeMovementSpeed(true); // 속도 증가
                break;

            case ItemType.SpeedDown:
                itemInteraction.ChangeMovementSpeed(false); // 속도 감소
                break;
        }

        Destroy(gameObject); // 아이템 제거
    }
}
