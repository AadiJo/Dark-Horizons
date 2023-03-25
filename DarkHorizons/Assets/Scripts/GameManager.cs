using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public void respawn()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
