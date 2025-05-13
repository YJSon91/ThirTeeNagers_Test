using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image[] heartImages;       //총 3개의 하트이미지를 인스펙터에서 할당함
    [SerializeField] private Sprite fullHeart;      //꽉찬 하트
    [SerializeField] private Sprite emptyHeart;         		//빈 하트
    [SerializeField] private Animator animator; 	//애니메이터

    private int maxHealth;              //hp 변수

    //체력 초기화 메서드
    public void SetMaxHealth(int hp)
    {
        maxHealth = hp;
        UpdateHealtDisplay(hp);
    }

    //체력 업데이트(실시간 조정) 메서드
    public void UpdateHealtDisplay(int currentHP)
    {
        //하트 이미지 수 만큼 반복
        for (int i = 0; i < heartImages.Length; i++)
        {
            // 애니메이터 할당
            Animator heartAnimator = heartImages[i].GetComponent<Animator>();
            //i보다 currentHP가 높으면 실행
            if (i<currentHP)
            {
                //하트이미지[I]번째가 꽉찬하트가 아니면
                if (heartImages[i].sprite != fullHeart)
                {
                    heartImages[i].sprite = fullHeart;  //그 하트를 꽉찬하트로 바꿔줌
                    heartAnimator.ResetTrigger("IsHeal");   //HEAL 애니메이션 초기화
                    heartAnimator.SetTrigger("IsHeal");     //HEAL 애니메이션 출력
                }
            }
            //I보다 currentHP가 낮으면 실행
            else
            {
                if(heartImages[i].sprite != emptyHeart)     //I번째 이미지가 빈하트가 아니면
                {
                    if(heartAnimator != null)           //애니메이션이 NULL이 아니면
                    {   
                        heartImages[i].sprite = emptyHeart;     //꽉찬하트를 빈 하트로 만듦
                        //피격 애니메이션 실행
                        heartAnimator.ResetTrigger("IsHit");
                        heartAnimator.SetTrigger("IsHit");
                    }
                }
            }
        }
    }
}
