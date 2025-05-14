using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStage", menuName = "Stage/StageData")]
public class StageData : ScriptableObject
{
    public Vector3 spawnPosition;
    public int stageNumber;
    public float survivalTime;
    public int playerSpeed;
    public GameObject backgroundPrefab;
    public AudioClip bgm;

}
