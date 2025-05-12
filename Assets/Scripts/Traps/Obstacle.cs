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

    public float widthPadding = 4.0f; // 장애물 간격

    // 장애물 위치 설정
    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        widthPadding = Random.Range(3.0f, 4.0f);
        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);

        switch (obstacleType)
        {
            case ObstacleType.Fire:
                placePosition.y = -0.9f; // 중간 라인 (예시)
                break;
            case ObstacleType.Saw:
                placePosition.y = -1.5f; // 아래쪽
                break;
            case ObstacleType.Spike:
                placePosition.y = -1.15f;
                break;
            case ObstacleType.SpikeHead:
                placePosition.y = Random.Range(0.0f, 3.0f); // 위쪽 랜덤
                break;
           
        }

        transform.position = placePosition;
        return placePosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHandler>(out var player))
        {
            //player.TakeDamage(1,vector2);
        }
    }
}
