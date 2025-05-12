using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Btn : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("StageScene");
    }
}