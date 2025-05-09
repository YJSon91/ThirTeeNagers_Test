using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 1f; // ��ֹ��� ��ġ�� �� �ִ� Y�� ���Ѽ�
    public float lowPosY = -1f; // ��ֹ��� ��ġ�� �� �ִ� Y�� ���Ѽ�

    public float holeSizeMin = 1f; // ������ �ּ� ũ��
    public float holeSizeMax = 3f; // ������ �ִ� ũ��

    public Transform topObject; // ��ֹ� ��� ������Ʈ (�� ��ü�� ���� ��ġ)
    public Transform bottomObject; // ��ֹ� �ϴ� ������Ʈ (�� ��ü�� �Ʒ��� ��ġ)

    public float widthPadding = 4f; // �� ��ֹ� ���� X�� ���� (�ʺ� �е�)

    // ��ֹ��� ���� ��ġ�� ��ġ�ϴ� �Լ�
    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        // ���� ũ�� ���� ���� (min ~ max ���� ������)
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        // ���� ũ�⸦ ������ ������ ��ܰ� �ϴ� ��ü�� Y ��ġ ����
        float halfHoleSize = holeSize / 2f;
        topObject.localPosition = new Vector3(0, halfHoleSize); // ��� ��ü�� ��ġ
        bottomObject.localPosition = new Vector3(0, -halfHoleSize); // �ϴ� ��ü�� ��ġ

        // ������ ��ġ���� X������ ������ ���� ���ο� ��ġ ���
        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        // ���ο� ��ġ�� Y���� ���� ������ ���� (lowPosY�� highPosY ����)
        placePosition.y = Random.Range(lowPosY, highPosY);

        // ��ֹ��� ��ġ�� ���ο� ��ġ�� ����
        transform.position = placePosition;

        // ���� ������ ��ġ�� ��ȯ
        return placePosition;


    }// �÷��̾ ��ֹ��� �浹 �� ������ ó���� ���� �߰�
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ��ü�� �̸��� ����� �α׿� ���
        Debug.Log("Triggered");

        PlayerHandler player = collision.GetComponent<PlayerHandler>();
        if ((player))
        {
            player.TakeDamage(1);
            Debug.Log("Damaged");

        }
    }
}