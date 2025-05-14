using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public StageData currentStageData;
    public List<StageData> stageList;

    public static StageManager instance;

    [SerializeField] private PlayerHandler player;
    [SerializeField] private GameManager GameManager;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadStage(StageData data)
    {
        GameObject oldstage = GameObject.Find("StageRoot");
        if (oldstage != null)
        {
            Destroy(oldstage);
        }

        GameObject newStage = Instantiate(data.backgroundPrefab);
        currentStageData = data;
        Debug.Log($"[StageManager] Loading Stage: {data.name}, Speed: {data.playerSpeed}, Time: {data.survivalTime}");
        player.transform.position = data.spawnPosition;
        player.PlayerSpeed = data.playerSpeed;
        GameManager.SetSurvivalTime(data.survivalTime);
        Instantiate(data.backgroundPrefab);
        audioSource.clip = data.bgm;
        audioSource.Play();

        GameManager.StageStart();
    }

    public void LoadNextStage()
    {
        GameManager.Instance.CloseStageClearPanel();

        Time.timeScale = 1.0f;
        int index = stageList.IndexOf(currentStageData);
        if(index + 1 < stageList.Count)
        {
            StageData next = stageList[index + 1];
            LoadStage(next);

        }
        else
        {
            {
                Debug.Log("마지막 스테이지입니다.");
            }
        }

    }

    public void OnSelectStage(StageData selectData)
    {
        LoadStage(selectData);
    }
}
