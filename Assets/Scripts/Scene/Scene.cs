using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Scene : GameManager
public class Scene : MonoBehaviour
{

    void Start()
    {
        int j = 11;
        //int j =  currentStage; // ���� �޴������� �������� ���� ���� ���� �������� ��

        GameObject Infinity = GameObject.Find("Infinity"); Infinity.SetActive(false); //���Ǵ�Ƽ ������Ʈ �������� �����
        int i = 1;
        while (true)//~���ε���
        {

            GameObject btn = GameObject.Find("StageBtn (" + i + ")"); //�̸����� ���� ������Ʈ �˻��� btn�� ����
            if (btn == null)//������ �ݺ��׸�
            {
                if(j+1 > i)//���� ���������� �Ѿ��� ���
                {
                    Infinity.SetActive(true);//���Ǵ�Ƽ ����ر�
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