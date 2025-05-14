using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectButtonManager : MonoBehaviour
{
    [SerializeField] private StageData stageData;
    public void SetSelectedStage()
    {
        StageDataHolder.Instance.selectedStage = stageData;
        SceneManager.LoadScene("MainScenes");
    }
}
