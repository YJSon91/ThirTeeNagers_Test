using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bglooper : MonoBehaviour
{
    

    public int obstacleCount = 0; // ��ֹ��� ����
    public Vector3 obstacleLastPosition = Vector3.zero; // ���������� ��ġ�� ��ֹ��� ��ġ
    public int numBgCount = 3;

    void Start()
    {
        // ���� �����ϴ� ��� Obstacle ��ü�� �迭�� ��������
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        // ù ��° ��ֹ��� ��ġ�� obstacleLastPosition�� ����
        obstacleLastPosition = obstacles[0].transform.position;
        // ��ֹ��� ������ ����Ͽ� ����
        obstacleCount = obstacles.Length;

        // ��ֹ� ������ŭ �ݺ��Ͽ� �� ��ֹ��� ��ġ�� �����ϰ� ����
        for (int i = 0; i < obstacleCount; i++)
        {
            // SetRandomPlace �Լ��� �� ��ֹ��� ��ġ�� ���� ��ֹ� ��ġ�� ������� ������
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ��ü�� �̸��� ����� �α׿� ���
        Debug.Log("Triggered: " + collision.name);

        if (collision.CompareTag("Ground"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;
            return;
        }
        // �浹�� ��ü�� Obstacle���� Ȯ��
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            // ��ֹ��� �浹 �� ���� ��ġ�� ���ġ
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
            
        }
    }
}