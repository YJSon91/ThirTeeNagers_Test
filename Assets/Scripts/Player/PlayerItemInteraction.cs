using UnityEngine;
using System.Collections;

// 플레이어가 아이템 효과(속도 배율)를 받는 처리를 담당하는 스크립트
// GameManager의 기본 속도에 배율을 곱해 일시적인 속도 변화를 적용한다
public class PlayerItemInteraction : MonoBehaviour
{
    [Header("속도 효과 지속 시간 (초)")]
    [SerializeField] private float speedChangeDuration = 5f; // 속도 아이템 효과가 유지되는 시간 (초)

    // 현재 속도 배율 (1.0f = 정상 속도, 1.5f = 1.5배 빠름, 0.5f = 절반 속도)
    private float currentSpeedMultiplier = 1f;

    // 현재 실행 중인 속도 코루틴을 저장 (중복 효과 방지용)
    private Coroutine speedCoroutine;

    // 속도 아이템 효과를 적용하는 함수
    // isSpeedUp이 true면 가속, false면 감속
    public void ChangeMovementSpeed(bool isSpeedUp)
    {
        // 가속이면 1.5배, 감속이면 0.5배 배율 적용
        float multiplier = isSpeedUp ? 1.5f : 0.5f;

        // 기존에 실행 중인 효과가 있으면 중단 (중첩 방지)
        if (speedCoroutine != null)
        {
            StopCoroutine(speedCoroutine);
        }

        // 새로운 속도 배율 코루틴 실행
        speedCoroutine = StartCoroutine(ApplySpeedMultiplierTemporarily(multiplier));
    }

    // 일정 시간 동안 배율을 적용하고 다시 1.0f로 복원하는 코루틴
    private IEnumerator ApplySpeedMultiplierTemporarily(float multiplier)
    {
        currentSpeedMultiplier = multiplier; // 새로운 배율 적용

        yield return new WaitForSeconds(speedChangeDuration); // 설정된 시간만큼 대기

        currentSpeedMultiplier = 1f; // 시간이 지나면 다시 정상 속도로 복원
    }

    // 현재 이동 속도를 반환하는 함수
    // GameManager에 있는 현재 기본 속도에 배율을 곱해서 최종 속도를 계산
    public float GetCurrentSpeed()
    {
        float baseSpeed = GameManager.Instance.GetCurrentGameSpeed(); // GameManager에서 현재 기본 속도 가져옴
        return baseSpeed * currentSpeedMultiplier; // 최종 속도 = 기본 속도 × 배율
    }
}
