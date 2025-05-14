using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataHolder : MonoBehaviour
{
    public static StageDataHolder Instance { get; private set; }

    public StageData selectedStage;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
