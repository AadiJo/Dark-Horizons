using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeakWithinRange : MonoBehaviour
{

    public GameObject dialouge;
    public GameObject player;

    void Start()
    {

        dialouge.SetActive(false);

    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < 2)
        {

            dialouge.SetActive(true);

        }
        if (Mathf.Abs(transform.position.x - player.transform.position.x) > 20)
        {

            dialouge.SetActive(false);

        }
    }
}
