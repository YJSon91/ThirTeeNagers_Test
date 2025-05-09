using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backbtn : MonoBehaviour
{
    public GameObject StageScreen; 
    public GameObject TitleScreen;

    private void OnMouseDown()
    {
        StageScreen.SetActive(false);
        TitleScreen.SetActive(true);
    }
}
