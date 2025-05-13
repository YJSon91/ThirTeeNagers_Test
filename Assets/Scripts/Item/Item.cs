using UnityEngine;

// ������ Ÿ�� ����
public enum ItemType
{
    Score,      // ���� ������
    Heal,       // ü�� ȸ�� ������
    SpeedUp,    // �ӵ� ���� ������
    SpeedDown   // �ӵ� ���� ������
}

// �÷��̾ �����ۿ� ����� �� ȿ���� �����ϴ� ��ũ��Ʈ
public class Item : MonoBehaviour
{
    [SerializeField] private ItemType itemType; // �ν����Ϳ��� ������ Ÿ�� ����
    [SerializeField] private Transform playerTransform;
    [SerializeField] private HealthUI healthUI;

    private void Start()
    {
        healthUI = FindObjectOfType<HealthUI>();
        // �������� ������ �� 5�ʰ� ������ �ڵ����� �ı��� (�÷��̾ �� �Ծ��� ��� ���)
        //Destroy(gameObject, 5f);
        GameObject playerobj = GameObject.FindWithTag("Player");                    //player �±� ã��
        if (playerobj != null)
        {
            playerTransform = playerobj.transform;          //�÷��̾ null�� �ƴϸ� �÷��̾� ��ġ�� ����
        }
    }
    private void Update()
    {
        if (playerTransform == null) return;

        float distance = transform.position.x - playerTransform.position.x;
        if(distance < -10f )
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ����� Player �±װ� �ƴϸ� ����
        if (!collision.CompareTag("Player")) return;

        // �÷��̾��� ü�� ���� �������� (PlayerState ��ũ��Ʈ)
        PlayerState playerState = collision.GetComponent<PlayerState>();

        // �÷��̾��� �ӵ� ���� ���� �������� (PlayerItemInteraction ��ũ��Ʈ)
        PlayerItemInteraction itemInteraction = collision.GetComponent<PlayerItemInteraction>();

        // �� �� �ϳ��� ������ ȿ�� �������� �ʰ� ����
        if (playerState == null || itemInteraction == null) return;

        // ������ ������ ���� �ٸ� ȿ�� ����
        switch (itemType)
        {
            case ItemType.Score:
                GameManager.Instance.AddScore(1); // ���� +1 ���� (GameManager�� AddScore �Լ� ȣ��)
                Debug.Log("�������� �����");
                SoundManager.instance.PlayScore();
                break;

            case ItemType.Heal:
                playerState.CurrentHealth += 1; // ü�� +1 ���� (�ִ�ġ ������ PlayerState���� ó��)
                healthUI.UpdateHealtDisplay(playerState.CurrentHealth);
                SoundManager.instance.PlayItemPickUp();
                break;

            case ItemType.SpeedUp:
                itemInteraction.ChangeMovementSpeed(true); // �ӵ� ���� ȿ�� (1.5��)
                itemInteraction.PlayTrailEffect(5);
                SoundManager.instance.PlayItemPickUp();
                break;

            case ItemType.SpeedDown:
                itemInteraction.ChangeMovementSpeed(false); // �ӵ� ���� ȿ�� (0.5��)
                SoundManager.instance.PlayItemPickUp();
                break;
        }

        // ȿ�� ���� �� �������� ��� ����
        Destroy(gameObject);
    }
}
