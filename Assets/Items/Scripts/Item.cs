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

    // 속도 효과 적용 중인 경우 취소를 위한 코루틴 핸들
    private Coroutine speedCoroutine;

    // 최대 체력
    [SerializeField] private int maxHealth = 3;

    // 현재 체력
    private int currentHealth;

    // 게임 시작 시 초기 세팅
    private void Start()
    {
        currentSpeed = defaultSpeed;
        currentHealth = maxHealth;
    }

    // 체력을 amount만큼 증가시킨다 (최대치 초과 방지)
    public void IncreaseHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        // Debug.Log($"체력 증가 → 현재 체력: {currentHealth}");
    }

    // 이동 속도를 일시적으로 변경한다 (true = 가속, false = 감속)
    public void ChangeMovementSpeed(bool isSpeedUp)
    {
        float targetSpeed = isSpeedUp ? defaultSpeed * 1.5f : defaultSpeed * 0.5f;

        // 기존 효과가 있으면 중단하고 덮어쓰기
        if (speedCoroutine != null)
        {
            StopCoroutine(speedCoroutine);
        }

        speedCoroutine = StartCoroutine(ApplyTemporarySpeed(targetSpeed));
    }

    // 일정 시간 후 속도를 원래대로 복원한다
    private IEnumerator ApplyTemporarySpeed(float modifiedSpeed)
    {
        currentSpeed = modifiedSpeed;
        // Debug.Log($"속도 변경 → 현재 속도: {currentSpeed}");

        yield return new WaitForSeconds(speedChangeDuration);

        currentSpeed = defaultSpeed;
        // Debug.Log($"속도 복원 → 현재 속도: {currentSpeed}");
    }

    // 현재 이동 속도를 외부에서 조회할 수 있게 한다
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}
