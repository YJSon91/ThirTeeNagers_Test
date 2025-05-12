using UnityEngine;

// 아이템 타입 정의
public enum ItemType
{
    Score,      // 점수 아이템
    Heal,       // 체력 회복 아이템
    SpeedUp,    // 속도 증가 아이템
    SpeedDown   // 속도 감소 아이템
}

// 플레이어가 아이템에 닿았을 때 효과를 적용하는 스크립트
public class Item : MonoBehaviour
{
    [SerializeField] private ItemType itemType; // 인스펙터에서 아이템 타입 선택

    private void Start()
    {
        // 아이템이 생성된 후 5초가 지나면 자동으로 파괴됨 (플레이어가 안 먹었을 경우 대비)
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 대상이 Player 태그가 아니면 무시
        if (!collision.CompareTag("Player")) return;

        // 플레이어의 체력 정보 가져오기 (PlayerState 스크립트)
        PlayerState playerState = collision.GetComponent<PlayerState>();

        // 플레이어의 속도 조절 정보 가져오기 (PlayerItemInteraction 스크립트)
        PlayerItemInteraction itemInteraction = collision.GetComponent<PlayerItemInteraction>();

        // 둘 중 하나라도 없으면 효과 적용하지 않고 종료
        if (playerState == null || itemInteraction == null) return;

        // 아이템 종류에 따라 다른 효과 적용
        switch (itemType)
        {
            case ItemType.Score:
                GameManager.Instance.AddScore(1); // 점수 +1 증가 (GameManager의 AddScore 함수 호출)
                break;

            case ItemType.Heal:
                playerState.CurrentHealth += 1; // 체력 +1 증가 (최대치 제한은 PlayerState에서 처리)
                break;

            case ItemType.SpeedUp:
                itemInteraction.ChangeMovementSpeed(true); // 속도 증가 효과 (1.5배)
                break;

            case ItemType.SpeedDown:
                itemInteraction.ChangeMovementSpeed(false); // 속도 감소 효과 (0.5배)
                break;
        }

        // 효과 적용 후 아이템은 즉시 제거
        Destroy(gameObject);
    }
}
