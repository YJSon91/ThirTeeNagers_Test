using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour

{
    public float highPosY = 1f; // 장애물이 배치될 수 있는 Y축 상한선
    public float lowPosY = -1f; // 장애물이 배치될 수 있는 Y축 하한선

    public float holeSizeMin = 1f; // 구멍의 최소 크기
    public float holeSizeMax = 3f; // 구멍의 최대 크기

    public Transform topObject; // 장애물 상단 오브젝트 (이 객체를 위로 배치)
    public Transform bottomObject; // 장애물 하단 오브젝트 (이 객체를 아래로 배치)

    public float widthPadding = 4f; // 각 장애물 간의 X축 간격 (너비 패딩)

    // 장애물을 랜덤 위치에 배치하는 함수
    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        // 구멍 크기 랜덤 설정 (min ~ max 범위 내에서)
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        // 구멍 크기를 반으로 나누어 상단과 하단 객체의 Y 위치 설정
        float halfHoleSize = holeSize / 2f;
        topObject.localPosition = new Vector3(0, halfHoleSize); // 상단 객체의 위치
        bottomObject.localPosition = new Vector3(0, -halfHoleSize); // 하단 객체의 위치

        // 마지막 위치에서 X축으로 간격을 더한 새로운 위치 계산
        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        // 새로운 위치의 Y축을 랜덤 값으로 설정 (lowPosY와 highPosY 사이)
        placePosition.y = Random.Range(lowPosY, highPosY);

        // 장애물의 위치를 새로운 위치로 설정
        transform.position = placePosition;

        // 새로 설정된 위치를 반환
        return placePosition;
    }
    // 플레이어와 충돌 시 TakeDamage 메서드 호출
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        PlayerHandler playerHandler = collision.GetComponent<PlayerHandler>();
        if(playerHandler)
        {
            // 플레이어와 충돌 시 TakeDamage 메서드 호출
            playerHandler.TakeDamage(1); // 플레이어에게 1의 피해를 줌
            Debug.Log("플레이어가 장애물에 충돌했습니다.");
        }
        
    }
}