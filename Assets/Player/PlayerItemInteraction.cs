using UnityEngine;
using System.Collections;

/*
 * PlayerItemInteraction.cs
 * -------------------------
 * �� ��ũ��Ʈ�� �÷��̾ �����۰� �浹���� ��
 * ü�� ����, �ӵ� ����/���� ���� ȿ���� �����ϴ� ���� ��ũ��Ʈ�Դϴ�.
 * 
 * [��� ���]
 * 1. �� ��ũ��Ʈ�� Player ������Ʈ�� ���Դϴ�.
 * 2. Item.cs ��ũ��Ʈ���� �浹�� ��󿡰� �� ��ũ��Ʈ�� GetComponent�� �����ɴϴ�.
 *    ��:
 *       PlayerItemInteraction itemHandler = collision.GetComponent<PlayerItemInteraction>();
 *       if (itemHandler != null)
 *       {
 *           itemHandler.IncreaseHealth(1); // ü�� +1
 *           itemHandler.ChangeMovementSpeed(true); // ����
 *       }
 * 
 * [���� ����]
 * - �� ��ũ��Ʈ�� PlayerController�� �и��Ǿ� ���������� ���� �� �ֽ��ϴ�.
 * - Start()���� �ʱⰪ �ڵ� ���� (currentSpeed, currentHealth)
 */

public class PlayerItemInteraction : MonoBehaviour
{
    [Header("������ ȿ�� ����")]
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


