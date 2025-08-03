using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CoinCount;

public class MenuController : MonoBehaviour
{
    public GameObject DeathPanel;
    public GameObject PausePanel;
    public GameObject InGamePanel;
    public GameObject EndPanel;

    public TextMeshProUGUI TotalScore;
    private CoinCollector coin;

    public AudioSource audioSource;
    public AudioClip[] soundClips; // 0 = �l�m, 1 = Biti�

    public bool isPause;
    public bool isDeath;
    public bool isEnd;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            isDeath = true;
        }
        if (other.gameObject.CompareTag("end"))
        {
            isEnd = true;
        }
    }

    public void Start()
    {
        audioSource.ignoreListenerPause = false;
    }

    public void Update()
    {
        // �lme
        if (isDeath && !isPause)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            PlaySound(0); // �l�m sesi

            DeathPanel.SetActive(true);
            PausePanel.SetActive(false);
            InGamePanel.SetActive(false);
            EndPanel.SetActive(false);
            Time.timeScale = 0f;
        }

        // Durdurma (Escape tu�u)
        if (!isDeath && Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;

            if (isPause)
            {
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                PausePanel.SetActive(true);
                InGamePanel.SetActive(false);
            }
            else
            {
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                PausePanel.SetActive(false);
                InGamePanel.SetActive(true);
            }
        }

        // Oyunun Biti�i
        if (isEnd && !isPause && !isDeath)
        {
            PlaySound(1); // Biti� sesi

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            DeathPanel.SetActive(false);
            PausePanel.SetActive(false);
            InGamePanel.SetActive(false);
            EndPanel.SetActive(true);
            Time.timeScale = 0f;

            TotalScore.text = "Total Score " + CoinCollector.score;
        }
    }

    private void PlaySound(int index)
    {
        if (audioSource != null && soundClips != null && index >= 0 && index < soundClips.Length)
        {
            audioSource.clip = soundClips[index];
            audioSource.Play();
        }
    }
}
