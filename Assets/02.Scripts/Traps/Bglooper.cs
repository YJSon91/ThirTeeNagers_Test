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
    //�������� �ε�� ��ֹ� ���ġ�ϴ� �Լ�
    public void ResetObstacles()
    {
        List<Obstacle> obstacleList = GameObject.FindObjectsOfType<Obstacle>().ToList();        //�������� �ȿ� �ִ� ��ֹ��� ���� ã�� ����Ʈ�� ����

        //��ֹ��� ������ Ȯ��
        if(obstacleList.Count== 0)
        {
            Debug.LogWarning("[Bglooper] ��ֹ��� �ϳ��� �����ϴ�.");
            return;
        }

        Transform player = GameObject.FindWithTag("Player")?.transform; //�±� ������� �÷��̾� ã��
        //������ �α�
        if(player == null)
        {
            Debug.Log("[Bglooper]�÷��̾ ã�� �� �����ϴ�.");
            return;
        }

        obstacleCount = obstacleList.Count;         //���ġ �� ��ֹ� ��
        obstacleLastPosition = player.position + new Vector3(10f, 0, 0);        //�÷��̾� ���� ����(������)���� ��ֹ� ��ġ ����


        //��ֹ� ��ġ ���ġ
        for(int i = 0; i< obstacleCount; i++)       //obstacleCount(��ֹ� ��)��ŭ �ݺ�
        {   
            //��ֹ� ����Ʈ���� �����ϰ� �ϳ� ����
            int randIndex = Random.Range(0, obstacleList.Count);        
            Obstacle selected = obstacleList[randIndex];

            //������ ��ֹ��� ���� ��ֹ� ��ġ�� �������� ���� �Ÿ� ������ ��ġ�� ��ġ��(SetRandomPlace���� return �� placePosition�� ���� ��ֹ��� ���� ��ġ�� ���)
            obstacleLastPosition = selected.SetRandomPlace(obstacleLastPosition,obstacleCount);

            obstacleList.RemoveAt(randIndex);       //����Ʈ���� �ش� ��ֹ��� �����Ͽ� �ߺ� ����
        }
        Debug.Log($"[Bglooper] ��ֹ� {obstacleCount}�� ���ġ �Ϸ�.");
    }
}