using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Scene : GameManager
public class Scene : MonoBehaviour
{
  
    
    void Start()
    {

        // int j = GameManager.Instance.currentStage;
        int j = 10;

    //int j = gameManager.currentStage;
    //int j =  currentStage; // ���� �޴������� �������� ���� ���� ���� �������� ��

    GameObject InfinityStageBtn = GameObject.Find("InfinityStageBtn"); InfinityStageBtn.SetActive(false); //���Ǵ�Ƽ ������Ʈ �������� �����
        int i = 1;
        while (true)//~���ε���
        {

            GameObject btn = GameObject.Find("StageSelectBtn " + i); //�̸����� ���� ������Ʈ �˻��� btn�� ����
            Debug.Log(("StageSelectBtn " + i));
            if (btn == null)//������ �ݺ��׸�
            {
                if(j+1 > i)//���� ���������� �Ѿ��� ���
                {
                    InfinityStageBtn.SetActive(true);//���Ǵ�Ƽ ����ر�
                }
                break;
            }
            btn.SetActive(false);//�ش� ������Ʈ ��Ȱ��ȭ

            if (j >= i)//���� ��罺������ ���� ���� ������
                btn.SetActive(true); //���̰� �Ѵ�
           i++;//i�� �ø���
        }
    }
}