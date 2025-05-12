using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image[] heartImaages;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private int maxHealth;


    public void SetMaxHealth(int hp)
    {
        maxHealth = hp;
        UpdateHealtDisplay(hp);
    }

    public void UpdateHealtDisplay(int currentHP)
    {
        for(int i = 0; i < heartImaages.Length; i++)
        {
            if(i<currentHP)
            {
                heartImaages[i].sprite = fullHeart;
            }
            else
            {
                heartImaages[i].sprite = emptyHeart;
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
