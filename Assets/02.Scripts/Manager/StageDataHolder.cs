using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataHolder : MonoBehaviour
{
    public static StageDataHolder Instance { get; private set; }            //인스턴스 프로퍼티

    public StageData selectedStage;     //스테이지 데이터를 받아오기 위한 변수

    //싱글톤 및 부서지지 않게 설계
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
