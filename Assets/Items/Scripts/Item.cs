using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // �⺻ �̵� �ӵ�
    [SerializeField] private float defaultSpeed = 5f;

    // ���� ���� ���� �̵� �ӵ�
    private float currentSpeed;

    // �ӵ� ���� ȿ�� ���� �ð� (��)
    [SerializeField] private float speedChangeDuration = 5f;

    // �ӵ� ȿ�� ���� ���� ��� ��Ҹ� ���� �ڷ�ƾ �ڵ�
    private Coroutine speedCoroutine;

    // �ִ� ü��
    [SerializeField] private int maxHealth = 3;

    // ���� ü��
    private int currentHealth;

    // ���� ���� �� �ʱ� ����
    private void Start()
    {
        currentSpeed = defaultSpeed;
        currentHealth = maxHealth;
    }

    // ü���� amount��ŭ ������Ų�� (�ִ�ġ �ʰ� ����)
    public void IncreaseHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        // Debug.Log($"ü�� ���� �� ���� ü��: {currentHealth}");
    }

    // �̵� �ӵ��� �Ͻ������� �����Ѵ� (true = ����, false = ����)
    public void ChangeMovementSpeed(bool isSpeedUp)
    {
        float targetSpeed = isSpeedUp ? defaultSpeed * 1.5f : defaultSpeed * 0.5f;

        // ���� ȿ���� ������ �ߴ��ϰ� �����
        if (speedCoroutine != null)
        {
            StopCoroutine(speedCoroutine);
        }

        speedCoroutine = StartCoroutine(ApplyTemporarySpeed(targetSpeed));
    }

    // ���� �ð� �� �ӵ��� ������� �����Ѵ�
    private IEnumerator ApplyTemporarySpeed(float modifiedSpeed)
    {
        currentSpeed = modifiedSpeed;
        // Debug.Log($"�ӵ� ���� �� ���� �ӵ�: {currentSpeed}");

        yield return new WaitForSeconds(speedChangeDuration);

        currentSpeed = defaultSpeed;
        // Debug.Log($"�ӵ� ���� �� ���� �ӵ�: {currentSpeed}");
    }

    // ���� �̵� �ӵ��� �ܺο��� ��ȸ�� �� �ְ� �Ѵ�
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}
