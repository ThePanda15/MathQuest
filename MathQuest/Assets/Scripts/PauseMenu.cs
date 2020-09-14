using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI, quest;
    public Sprite[] icons;
    public Image pauseIcon;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        quest.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseIcon.sprite = icons[0];
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        quest.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseIcon.sprite = icons[1];
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quiting...");
    }

    public void ChallengersRoom()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseIcon.sprite = icons[0];
        SceneManager.LoadScene(1);
    }
}
