using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strbtn : MonoBehaviour
{
    public GameObject StageScreen; // Unity 에디터에서 연결
    public GameObject TitleScreen;

    private void OnMouseDown()
    {
        StageScreen.SetActive(true);
        TitleScreen.SetActive(false);
    }
}

