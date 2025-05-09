using UnityEngine;
using System.Collections;

/*
 * PlayerItemInteraction.cs
 * -------------------------
 * 이 스크립트는 플레이어가 아이템과 충돌했을 때
 * 체력 증가, 속도 증가/감소 등의 효과를 적용하는 전용 스크립트입니다.
 * 
 * [사용 방법]
 * 1. 이 스크립트를 Player 오브젝트에 붙입니다.
 * 2. Item.cs 스크립트에서 충돌한 대상에게 이 스크립트를 GetComponent로 가져옵니다.
 *    예:
 *       PlayerItemInteraction itemHandler = collision.GetComponent<PlayerItemInteraction>();
 *       if (itemHandler != null)
 *       {
 *           itemHandler.IncreaseHealth(1); // 체력 +1
 *           itemHandler.ChangeMovementSpeed(true); // 가속
 *       }
 * 
 * [주의 사항]
 * - 이 스크립트는 PlayerController와 분리되어 독립적으로 붙일 수 있습니다.
 * - Start()에서 초기값 자동 설정 (currentSpeed, currentHealth)
 */

public class PlayerItemInteraction : MonoBehaviour
{
    [Header("아이템 효과 설정")]
    [SerializeField] private float defaultSpeed = 5f;
    [SerializeField] private float speedChangeDuration = 5f;
    [SerializeField] private int maxHealth = 3;

    private float currentSpeed;
    private int currentHealth;
    private Coroutine speedCoroutine;

    private void Start()
    {
        currentSpeed = defaultSpeed;
        currentHealth = maxHealth;
    }
}


