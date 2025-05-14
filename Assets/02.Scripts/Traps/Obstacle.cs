using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum ObstacleType
{
    Fire,
    Spike,
    Saw,
    SpikeHead,
}
public class Obstacle : MonoBehaviour
{
    [Header("장애물 타입 설정")]
    public ObstacleType obstacleType;
         
    public float widthPadding = 4.2f; // 장애물 간격

    // 장애물 위치 설정
    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        widthPadding = Random.Range(2.5f, 4.5f);
        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);

        switch (obstacleType)
        {
            case ObstacleType.Fire:
                placePosition.y = -1.7f; // 중간 라인 (예시)

                break;
            case ObstacleType.Saw:
                placePosition.y = -2.0f; // 아래쪽
                break;
            case ObstacleType.Spike:
                placePosition.y = -1.75f;
                break;
            case ObstacleType.SpikeHead:
               int spikeHeadHeight = Random.Range(0,2); // y값 종류
                switch (spikeHeadHeight)
                {
                    case 0:
                        placePosition.y = 0.5f; // 중간 라인 (예시)
                        break;
                    case 1:
                        placePosition.y = -0.75f; // 아래쪽
                        break;
                   
                }   

                break;
           
        }

        transform.position = placePosition;
        return placePosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHandler>(out var player))
        {
            player.TakeDamage(1,collision.transform.position);
        }
    }
}
