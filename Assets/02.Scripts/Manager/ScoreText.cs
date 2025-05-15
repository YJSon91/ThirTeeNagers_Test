using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTxt;
    void Start()
    {
        ShowBestScore();
    }
    

    public void ShowBestScore()
    {
        scoreTxt.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
    }
}
