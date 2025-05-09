using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // 기본 이동 속도
    [SerializeField] private float defaultSpeed = 5f;

    // 현재 적용 중인 이동 속도
    private float currentSpeed;

    // 속도 변경 효과 지속 시간 (초)
    [SerializeField] private float speedChangeDuration = 5f;

    // 속도 효과가 적용 중일 때 실행되는 코루틴을 저장하는 변수
    // (나중에 중단할 수 있도록 참조를 유지함)
    private Coroutine speedCoroutine;

    // 최대 체력
    [SerializeField] private int maxHealth = 3;

    // 현재 체력
    private int currentHealth;

    // 게임 시작 시 초기 속도와 체력을 설정
    private void Start()
    {
        currentSpeed = defaultSpeed;
        currentHealth = maxHealth;
    }

    // 외부에서 체력을 amount만큼 회복시킬 때 사용
    public void IncreaseHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        // Debug.Log($"체력 증가 → 현재 체력: {currentHealth}");
    }

    // 아이템 효과로 인해 속도를 변경하고 일정 시간 후 복구하는 함수
    // isSpeedUp이 true면 가속, false면 감속
    public void ChangeMovementSpeed(bool isSpeedUp)
    {
        // 속도를 1.5배 빠르게 또는 0.5배 느리게 설정
        float targetSpeed = isSpeedUp ? defaultSpeed * 1.5f : defaultSpeed * 0.5f;

        // 기존 속도 변경 효과가 실행 중이면 중단 (중첩 방지)
        if (speedCoroutine != null)
        {
            StopCoroutine(speedCoroutine);
        }

        // 새로운 속도 변경 효과 코루틴 실행
        speedCoroutine = StartCoroutine(ApplyTemporarySpeed(targetSpeed));
    }

    // 속도 변경 효과를 일정 시간 동안 유지하고 복구하는 코루틴
    // 코루틴(Coroutine)이란? Unity에서 일정 시간 동안 기다렸다가 어떤 동작을 실행할 수 있게 해주는 기능
    // 예: 속도 5초 동안 변경 → 다시 원래 속도로 복원
    private IEnumerator ApplyTemporarySpeed(float modifiedSpeed)
    {
        // 현재 속도를 변경된 값으로 설정
        currentSpeed = modifiedSpeed;

        // 지정된 시간만큼 대기 (이 동안 속도 유지)
        yield return new WaitForSeconds(speedChangeDuration);

        // 시간이 지나면 다시 기본 속도로 되돌림
        currentSpeed = defaultSpeed;
    }

    // 현재 속도를 외부에서 조회할 수 있게 제공하는 함수
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}
