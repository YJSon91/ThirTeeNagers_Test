using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Btn : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("��ŸƮ ��ư Ŭ����!");

        // ���� ����: �ܼ� ���, �� ��ȯ ��
        // SceneManager.LoadScene("GameScene"); // ���� ������ �̵��� ���
    }
}