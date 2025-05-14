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

        if (!isLoading) // �ε� ���� �ƴ� ����
        {
            float newY = Mathf.Sin(Time.time * speed) * floatStrength;
            transform.localPosition = startPos + new Vector3(0, newY, 0);
        }
        // �ƹ� Ű ������ �ε� ����
        if (!isLoading && Input.anyKeyDown)
        {
            isLoading = true;
            loadingPannel.SetActive(true);

            loadingTextCoroutine = StartCoroutine(LoadingDotRoutine());
        }

        // �ε� ���� �� �ε� ����
        if (isLoading && loadingPannel.activeSelf)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / loadingDuration);
            loadingBar.value = progress;
            player.position = Vector3.Lerp(playerStartPoint.position, playerEndPoint.position, progress);

            // �ε� �Ϸ� �� �� �̵�
            if (progress >= 1f)
            {
                hasFinishedLoading = true;

                // �ε� �ִϸ��̼� ���� (�� ���� �þ�� �ڷ�ƾ ����)
                if (loadingTextCoroutine != null)
                {
                    StopCoroutine(loadingTextCoroutine);
                    loadingTextCoroutine = null;
                }

                // �Ϸ� ���� �ڷ�ƾ ����
                StartCoroutine(FinishLoadingSequence());
            }
        }
    }
    IEnumerator LoadingDotRoutine()
    {
        string baseText = "Loading";
        int dotCount = 0;

        while (true) // ��� �ݺ�
        {
            dotCount = (dotCount + 1) % 4; // 0 �� 1 �� 2 �� 3 �� 0 ...
            loadingText.text = baseText + new string('.', dotCount);
            yield return new WaitForSeconds(0.5f); // 0.5�ʸ��� �� ���� ����
        }
    }
    IEnumerator FinishLoadingSequence()
    {
        // 1. �ؽ�Ʈ �ٲٰ� ���� ����
        loadingText.text = "�ε� �Ϸ�!";
        loadingText.color = Color.green;

        // 2. 1�� ��ٸ���
        yield return new WaitForSeconds(1f);

        // 3. �� ��ȯ
        SceneManager.LoadScene("TitleScene");
    }
}
