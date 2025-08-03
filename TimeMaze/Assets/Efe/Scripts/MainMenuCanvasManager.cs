using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCanvasManager : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject TeamPanel;

    public void startLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void showTeamPanel()
    {
        MenuPanel.SetActive(!MenuPanel.activeSelf);
        TeamPanel.SetActive(!TeamPanel.activeSelf);
    }
    public void Close()
    {
        Application.Quit();
    }
}
