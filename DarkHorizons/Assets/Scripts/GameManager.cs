using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;


public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    private GameObject player;
    public GameObject killLog;
    private PauseMenu pauseMenu;

    [HideInInspector] public string killerName;

    [Header("Game Variables")]
    [Space]

    public bool dead = false;


    void Start()
    {

        player = GameObject.Find("Player");
        pauseMenu = FindObjectOfType<PauseMenu>();
        //killLog = GameObject.Find("KillLog");


    }

    public void respawn()
    {

        pauseMenu.Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    private void Update()
    {

        if (dead)
        {

            OnDeath();

        }

    }

    void OnDeath()
    {

        GameObject playerLight = GameObject.Find("Player Light 2D");
        // Disable components
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<DeathFX>().ApplyFX();
        if (playerLight == null)
        {

            return;

        }
        playerLight.SetActive(false);

        // Disable animations
        player.GetComponent<Animator>().SetBool("isJumping", false);
        player.GetComponent<Animator>().SetBool("isFalling", false);
        player.GetComponent<Animator>().SetBool("isCrouching", false);

        // Turn off music
        FindObjectOfType<AudioManager>().Stop("Main Track");

        // Display Kill Log
        killLog.SetActive(true);
        killLog.GetComponent<TextMeshProUGUI>().text = "DEATH BY " + killerName;

        //sGameObject.FindGameObjectWithTag("DeadText").SetActive(true);


    }
}
