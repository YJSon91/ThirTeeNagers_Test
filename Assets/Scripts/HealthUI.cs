using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image[] heartImages;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Animator animator;

    private int maxHealth;


    public void SetMaxHealth(int hp)
    {
        maxHealth = hp;
        UpdateHealtDisplay(hp);
    }

    public void UpdateHealtDisplay(int currentHP)
    {
        for(int i = 0; i < heartImages.Length; i++)
        {
            Animator heartAnimator = heartImages[i].GetComponent<Animator>();
            if (i<currentHP)
            {
                if (heartImages[i].sprite != fullHeart)
                {
                    heartImages[i].sprite = fullHeart;
                    heartAnimator.ResetTrigger("IsHeal");
                    heartAnimator.SetTrigger("IsHeal");
                }
            }
            else
            {
                if(heartImages[i].sprite != emptyHeart)
                {
                    heartImages[i].sprite = emptyHeart;
                    if(heartAnimator != null)
                    {
                        heartAnimator.ResetTrigger("IsHit");
                        heartAnimator.SetTrigger("IsHit");
                    }
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
