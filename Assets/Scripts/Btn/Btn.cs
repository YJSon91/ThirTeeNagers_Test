using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Btn : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("스타트 버튼 클릭됨!");

        // 예시 동작: 콘솔 출력, 씬 전환 등
        // SceneManager.LoadScene("GameScene"); // 게임 씬으로 이동할 경우
    }
}