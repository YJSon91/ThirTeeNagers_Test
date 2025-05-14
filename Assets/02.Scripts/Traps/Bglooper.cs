using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
using System.Linq;

public class Bglooper : MonoBehaviour
{


    public int obstacleCount = 0; // ��ֹ��� ����
    public Vector3 obstacleLastPosition = Vector3.zero; // ���������� ��ġ�� ��ֹ��� ��ġ
    public int numBgCount = 3;
    public int numGroundCount = 3;


    void Start()
    {
        List<Obstacle> obstacleList = GameObject.FindObjectsOfType<Obstacle>().ToList();
        obstacleCount = obstacleList.Count;

        // ���� ��ġ�� ù ��° ������Ʈ ����
        obstacleLastPosition = obstacleList[0].transform.position;

        for (int i = 0; i < obstacleCount; i++)
        {
            // ���� ������Ʈ �� �����ϰ� �ϳ� ����
            int randIndex = Random.Range(0, obstacleList.Count);
            Obstacle selected = obstacleList[randIndex];

            // ��ġ
            obstacleLastPosition = selected.SetRandomPlace(obstacleLastPosition, obstacleCount);

            // ������ ������Ʈ�� ����Ʈ���� ����
            obstacleList.RemoveAt(randIndex);
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ��ü�� �̸��� ����� �α׿� ���
        // Debug.Log("Triggered: " + collision.name);
        
        if (collision.CompareTag("Ground"))
        {
            Debug.Log("Ground Check.");
            float widthOfBgObject = ((BoxCollider2D)collision).size.x/2;
            Debug.Log("widthOfBgObject: " + widthOfBgObject);
            Vector3 pos = collision.transform.position;
            
            pos.x += widthOfBgObject * numGroundCount;
            collision.transform.position = pos;
            return;
        }
        else if (collision.CompareTag("BackGround"))
        {
            Debug.Log("Bg Check.");
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;
            return;
        }
        // �浹�� ��ü�� Obstacle���� Ȯ��
        Obstacle obstacle = collision.GetComponentInParent<Obstacle>();
        if (obstacle)
        {
            Debug.Log("Object Check");
            // ��ֹ��� �浹 �� ���� ��ġ�� ���ġ
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);

        }
    }
}