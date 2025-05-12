using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image[] heartImages;       //�� 3���� ��Ʈ�̹����� �ν����Ϳ��� �Ҵ���
    [SerializeField] private Sprite fullHeart;      //���� ��Ʈ
    [SerializeField] private Sprite emptyHeart;         		//�� ��Ʈ
    [SerializeField] private Animator animator; 	//�ִϸ�����

    private int maxHealth;              //hp ����

    //ü�� �ʱ�ȭ �޼���
    public void SetMaxHealth(int hp)
    {
        maxHealth = hp;
        UpdateHealtDisplay(hp);
    }

    //ü�� ������Ʈ(�ǽð� ����) �޼���
    public void UpdateHealtDisplay(int currentHP)
    {
        //��Ʈ �̹��� �� ��ŭ �ݺ�
        for (int i = 0; i < heartImages.Length; i++)
        {
            // �ִϸ����� �Ҵ�
            Animator heartAnimator = heartImages[i].GetComponent<Animator>();
            //i���� currentHP�� ������ ����
            if (i<currentHP)
            {
                //��Ʈ�̹���[I]��°�� ������Ʈ�� �ƴϸ�
                if (heartImages[i].sprite != fullHeart)
                {
                    heartImages[i].sprite = fullHeart;  //�� ��Ʈ�� ������Ʈ�� �ٲ���
                    heartAnimator.ResetTrigger("IsHeal");   //HEAL �ִϸ��̼� �ʱ�ȭ
                    heartAnimator.SetTrigger("IsHeal");     //HEAL �ִϸ��̼� ���
                }
            }
            //I���� currentHP�� ������ ����
            else
            {
                if(heartImages[i].sprite != emptyHeart)     //I��° �̹����� ����Ʈ�� �ƴϸ�
                {
                    if(heartAnimator != null)           //�ִϸ��̼��� NULL�� �ƴϸ�
                    {   
                        heartImages[i].sprite = emptyHeart;     //������Ʈ�� �� ��Ʈ�� ����
                        //�ǰ� �ִϸ��̼� ����
                        heartAnimator.ResetTrigger("IsHit");
                        heartAnimator.SetTrigger("IsHit");
                    }
                }
            }
        }
    }
}
