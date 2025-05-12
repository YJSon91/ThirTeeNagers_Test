using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void RestartGame()
    {
        StartCoroutine(RestartRoutine());
    }

    public IEnumerator RestartRoutine()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.InitGame();
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
