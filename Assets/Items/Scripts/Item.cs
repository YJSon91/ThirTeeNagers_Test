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

    // �ӵ� ȿ���� ���� ���� �� ����Ǵ� �ڷ�ƾ�� �����ϴ� ����
    // (���߿� �ߴ��� �� �ֵ��� ������ ������)
    private Coroutine speedCoroutine;

    // �ִ� ü��
    [SerializeField] private int maxHealth = 3;

    // ���� ü��
    private int currentHealth;

    // ���� ���� �� �ʱ� �ӵ��� ü���� ����
    private void Start()
    {
        currentSpeed = defaultSpeed;
        currentHealth = maxHealth;
    }

    // �ܺο��� ü���� amount��ŭ ȸ����ų �� ���
    public void IncreaseHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        // Debug.Log($"ü�� ���� �� ���� ü��: {currentHealth}");
    }

    // ������ ȿ���� ���� �ӵ��� �����ϰ� ���� �ð� �� �����ϴ� �Լ�
    // isSpeedUp�� true�� ����, false�� ����
    public void ChangeMovementSpeed(bool isSpeedUp)
    {
        // �ӵ��� 1.5�� ������ �Ǵ� 0.5�� ������ ����
        float targetSpeed = isSpeedUp ? defaultSpeed * 1.5f : defaultSpeed * 0.5f;

        // ���� �ӵ� ���� ȿ���� ���� ���̸� �ߴ� (��ø ����)
        if (speedCoroutine != null)
        {
            StopCoroutine(speedCoroutine);
        }

        // ���ο� �ӵ� ���� ȿ�� �ڷ�ƾ ����
        speedCoroutine = StartCoroutine(ApplyTemporarySpeed(targetSpeed));
    }

    // �ӵ� ���� ȿ���� ���� �ð� ���� �����ϰ� �����ϴ� �ڷ�ƾ
    // �ڷ�ƾ(Coroutine)�̶�? Unity���� ���� �ð� ���� ��ٷȴٰ� � ������ ������ �� �ְ� ���ִ� ���
    // ��: �ӵ� 5�� ���� ���� �� �ٽ� ���� �ӵ��� ����
    private IEnumerator ApplyTemporarySpeed(float modifiedSpeed)
    {
        // ���� �ӵ��� ����� ������ ����
        currentSpeed = modifiedSpeed;

        // ������ �ð���ŭ ��� (�� ���� �ӵ� ����)
        yield return new WaitForSeconds(speedChangeDuration);

        // �ð��� ������ �ٽ� �⺻ �ӵ��� �ǵ���
        currentSpeed = defaultSpeed;
    }

    // ���� �ӵ��� �ܺο��� ��ȸ�� �� �ְ� �����ϴ� �Լ�
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}
