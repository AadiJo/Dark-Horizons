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

        if (GetComponent<SpriteRenderer>().color.a == 1f)
        {

            if (Mathf.Abs(transform.position.x - player.transform.position.x) < 5)
            {

                dialouge.SetActive(true);

            }

        }


        if (Mathf.Abs(transform.position.x - player.transform.position.x) > 20)
        {

            dialouge.SetActive(false);

        }
    }
}
