using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ڵ�� create�޴��� stage/stagedata��� �޴��� �����ǰ� ��
[CreateAssetMenu(fileName = "NewStage", menuName = "Stage/StageData")]
public class StageData : ScriptableObject	//���� scriptableobject�� ������ Ȱ��ȭ
{
    public Vector3 spawnPosition;			//ĳ���� ���� ����Ʈ
    public int stageNumber;					//�������� ��ȣ
    public float survivalTime;				//�����ؾ� �ϴ� �ð�
    public float playerSpeed;					//���� �̵��ӵ�
    public GameObject backgroundPrefab;		//��� ������
    public AudioClip bgm;					//�������

}
