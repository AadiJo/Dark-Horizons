using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameisPaused = false;
    public bool paused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame

    private void Start()
    {

        GameisPaused = false;

    }

    void Update()
    {

        if (Time.timeScale == 1)
        {

            GameisPaused = false;

        }
        else
        {

            GameisPaused = true;

        }

        paused = GameisPaused;


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
