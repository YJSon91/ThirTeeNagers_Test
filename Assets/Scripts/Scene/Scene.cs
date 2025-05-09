using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    public GameObject StageScreen;
    public GameObject TitleScreen;
    void Start()
    {
        TitleScreen.SetActive(true);
        StageScreen.SetActive(false);
    }


}
