using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        // �������� ������ �� 5�ʰ� ������ �ڵ����� �ı��� (�÷��̾ �� �Ծ��� ��� ���)
        Destroy(gameObject, 5f);
    }
}
