using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum ObastacleType
{
    Fire,       // 불 장애물
    Saw,        // 톱 장애물
    Spike,      // 스파이크 장애물
    SpikeHead,  // 스파이크 헤드 장애물        
}

public class Obstacle : MonoBehaviour
{
    public ObastacleType obstacleType; // 인스펙터에서 장애물 타입 선택

    private void Start()
    {
        // 아이템이 생성된 후 5초가 지나면 자동으로 파괴됨 (플레이어가 화면에서 사라지면 파괴)
        Destroy(gameObject, 8f);
    }

    // 플레이어가 장애물에 충돌 시 데미지 처리할 로직 추가
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체의 이름을 디버그 로그에 출력
        Debug.Log("Triggered");

        PlayerHandler player = collision.GetComponent<PlayerHandler>();
        if ((player))
        {
            player.TakeDamage(1);
            Debug.Log("Damaged");

        }
    
    }
   
}
