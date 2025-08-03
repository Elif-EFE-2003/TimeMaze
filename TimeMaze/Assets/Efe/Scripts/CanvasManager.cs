using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public GameObject infoPanel;

    public void Awake()
    {
        ShowInfo(4f);
    }
    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
        Time.timeScale = 1f;
    }
    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ShowInfo(float duration)
    {
        infoPanel.SetActive(true);
        StartCoroutine(HideAfterSeconds(duration));
    }

    private System.Collections.IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        infoPanel.SetActive(false);
    }
}
