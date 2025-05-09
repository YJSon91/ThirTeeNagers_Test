using UnityEngine;
using System.Collections;

// 플레이어가 아이템 효과(속도 배율)를 받는 처리를 담당하는 스크립트
// GameManager의 기본 속도에 배율을 곱해 일시적인 속도 변화를 적용한다
public class PlayerItemInteraction : MonoBehaviour
{
    [Header("속도 효과 지속 시간 (초)")]
    [SerializeField] private float speedChangeDuration = 5f;

    // 현재 속도 배율 (1.0f = 기본, 1.5f = 가속, 0.5f = 감속)
    private float currentSpeedMultiplier = 1f;

    // 코루틴 참조 저장 (중첩 방지용)
    private Coroutine speedCoroutine;

    // 속도 아이템 효과 적용 (true = 가속 / false = 감속)
    public void ChangeMovementSpeed(bool isSpeedUp)
    {
        float multiplier = isSpeedUp ? 1.5f : 0.5f;

        if (speedCoroutine != null)
        {
            StopCoroutine(speedCoroutine);
        }

        speedCoroutine = StartCoroutine(ApplySpeedMultiplierTemporarily(multiplier));
    }

    // 일정 시간 동안 배율 적용 후 복구
    private IEnumerator ApplySpeedMultiplierTemporarily(float multiplier)
    {
        currentSpeedMultiplier = multiplier;

        yield return new WaitForSeconds(speedChangeDuration);

        currentSpeedMultiplier = 1f;
    }

    // 현재 이동 속도를 반환
    // GameManager의 기본 속도에 배율을 곱한 결과 반환
    // GameManager.Instance.GetCurrentGameSpeed()는 현재 기본 속도를 반환하는 함수여야 함
    public float GetCurrentSpeed()
    {
        float baseSpeed = GameManager.Instance.GetCurrentGameSpeed(); // ← GameManager에 함수 반드시 추가할 것
        return baseSpeed * currentSpeedMultiplier;
    }
}
