using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnClick : MonoBehaviour
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(" UI 버튼 클릭됨!");
        // 여기에 원하는 동작 추가
    }
}
