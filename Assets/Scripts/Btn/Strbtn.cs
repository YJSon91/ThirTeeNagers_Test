using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strbtn : MonoBehaviour
{
    public GameObject StageScreen; // Unity �����Ϳ��� ����
    public GameObject TitleScreen;

    private void OnMouseDown()
    {
        StageScreen.SetActive(true);
        TitleScreen.SetActive(false);
    }
}

