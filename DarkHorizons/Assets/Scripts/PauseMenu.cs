using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameisPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (GameisPaused)
            {

                Resume();

            }
            else
            {

                Pause();

            }

        }

    }

    public void Resume()
    {

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;

    }

    public void Pause()
    {

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;

    }

}
