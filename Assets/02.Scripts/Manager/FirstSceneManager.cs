using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstSceneManager : MonoBehaviour
{
    public float floatStrength = 10f;
    public float speed = 2f;
    public GameObject loadingPannel;
    public Slider loadingBar;
    public Transform player;
    public Transform playerStartPoint;
    public Transform playerEndPoint;
    public TextMeshProUGUI loadingText;
    public float loadingDuration = 3f;

    private float timer = 0f;
    private Coroutine loadingTextCoroutine;

    private Vector3 startPos;
    private bool isLoading = false;
    private bool hasFinishedLoading = false;

    private void Start()
    {
        startPos = transform.localPosition;
        loadingPannel.SetActive(false);
        loadingBar.minValue = 0f;
        loadingBar.maxValue = 1f;
        loadingBar.value = 0;

    }

    void Update()
    {

        if (!isLoading) // 로딩 중이 아닐 때만
        {
            float newY = Mathf.Sin(Time.time * speed) * floatStrength;
            transform.localPosition = startPos + new Vector3(0, newY, 0);
        }
        // 아무 키 누르면 로딩 시작
        if (!isLoading && Input.anyKeyDown)
        {
            isLoading = true;
            loadingPannel.SetActive(true);

            loadingTextCoroutine = StartCoroutine(LoadingDotRoutine());
        }

        // 로딩 중일 때 로딩 진행
        if (isLoading && loadingPannel.activeSelf)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / loadingDuration);
            loadingBar.value = progress;
            player.position = Vector3.Lerp(playerStartPoint.position, playerEndPoint.position, progress);

            // 로딩 완료 후 씬 이동
            if (progress >= 1f)
            {
                hasFinishedLoading = true;

                // 로딩 애니메이션 멈춤 (점 점점 늘어나는 코루틴 중지)
                if (loadingTextCoroutine != null)
                {
                    StopCoroutine(loadingTextCoroutine);
                    loadingTextCoroutine = null;
                }

                // 완료 연출 코루틴 시작
                StartCoroutine(FinishLoadingSequence());
            }
        }
    }
    IEnumerator LoadingDotRoutine()
    {
        string baseText = "Loading";
        int dotCount = 0;

        while (true) // 계속 반복
        {
            dotCount = (dotCount + 1) % 4; // 0 → 1 → 2 → 3 → 0 ...
            loadingText.text = baseText + new string('.', dotCount);
            yield return new WaitForSeconds(0.5f); // 0.5초마다 한 번씩 변경
        }
    }
    IEnumerator FinishLoadingSequence()
    {
        // 1. 텍스트 바꾸고 색상 변경
        loadingText.text = "로딩 완료!";
        loadingText.color = Color.green;

        // 2. 1초 기다리기
        yield return new WaitForSeconds(1f);

        // 3. 씬 전환
        SceneManager.LoadScene("TitleScene");
    }
}
