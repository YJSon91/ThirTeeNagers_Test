using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenes : GameManager
{
    // Start is called before the first frame update
    void Start()
    {
        int i = 1;
        int j = currentStage;
        while (true)
        {
            GameObject btn = GameObject.Find("StageBtn (" + i + ")");
            if (btn == null)
                break;

            btn.SetActive(false);

            if (j >= i)
                btn.SetActive(true);


            i++;
        }
    }
}
