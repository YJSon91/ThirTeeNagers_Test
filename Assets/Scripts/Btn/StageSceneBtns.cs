using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StageSceneBtns : MonoBehaviour
{
    public void SceneChange()//ȭ�� ��ȯ
    {
        SceneManager.LoadScene("TitleScene");// ȭ�� ��ȯ
    }

        public void StageSelect()
        {
            GameObject clickedButton = EventSystem.current.currentSelectedGameObject;//��� ��ư ������Ʈ ��������
            string buttonName = clickedButton.name; // �̸� ����


            string stageNumber = buttonName.Substring(14); // "StageSelectBtn 1"�� 14����(����)���� ���ڿ��б�
            Debug.Log(stageNumber);//���
            SceneManager.LoadScene("MainScenes");//��������
    }




 }
    


