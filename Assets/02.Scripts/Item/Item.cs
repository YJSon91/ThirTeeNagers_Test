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
    [SerializeField] private Transform playerTransform;
    [SerializeField] private HealthUI healthUI;

    private void Start()
    {
        healthUI = FindObjectOfType<HealthUI>();
        // 아이템이 생성된 후 5초가 지나면 자동으로 파괴됨 (플레이어가 안 먹었을 경우 대비)
        //Destroy(gameObject, 5f);
        GameObject playerobj = GameObject.FindWithTag("Player");                    //player 태그 찾기
        if (playerobj != null)
        {
            playerTransform = playerobj.transform;          //플레이어가 null이 아니면 플레이어 위치를 저장
        }
    }
    private void Update()
    {
        //null 방어
        if (playerTransform == null) return;

        float distance = transform.position.x - playerTransform.position.x;     //플레이어와 아이템 간에 거리 계산
        //아이템과의 거리가 -10 이하이면 아이템 삭제
        if(distance < -10f )
        {
            Destroy(gameObject);
        }
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
                Debug.Log("오렌지에 닿았음");
                SoundManager.instance.PlayScore();  //오디오 클립 재생
                break;

            case ItemType.Heal:
                playerState.CurrentHealth += 1; // 체력 +1 증가 (최대치 제한은 PlayerState에서 처리)
                healthUI.UpdateHealtDisplay(playerState.CurrentHealth);     //체력ui가 회복되는 것 처리
                SoundManager.instance.PlayItemPickUp();                     //오디오 클립 재생
                break;

            case ItemType.SpeedUp:
                itemInteraction.ChangeMovementSpeed(true); // 속도 증가 효과 (1.5배)
                itemInteraction.PlayTrailEffect(5);         //이펙트 5초동안 지속
                SoundManager.instance.PlayItemPickUp();     //오디오 클립 재생
                break;

            case ItemType.SpeedDown:
                itemInteraction.ChangeMovementSpeed(false); // 속도 감소 효과 (0.5배)
                SoundManager.instance.PlayItemPickUp();     //오디오 클립 재생
                break;
        }

        // 효과 적용 후 아이템은 즉시 제거
        Destroy(gameObject);
    }
}
