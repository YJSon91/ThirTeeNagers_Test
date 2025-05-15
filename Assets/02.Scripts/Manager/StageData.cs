using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//코드로 create메뉴에 stage/stagedata라는 메뉴가 생성되게 함
[CreateAssetMenu(fileName = "NewStage", menuName = "Stage/StageData")]
public class StageData : ScriptableObject	//옆에 scriptableobject를 적으면 활성화
{
    public Vector3 spawnPosition;			//캐릭터 생성 포인트
    public int stageNumber;					//스테이지 번호
    public float survivalTime;				//생존해야 하는 시간
    public float playerSpeed;					//유저 이동속도
    public GameObject backgroundPrefab;		//배경 프리팹
    public AudioClip bgm;					//배경음악

}
