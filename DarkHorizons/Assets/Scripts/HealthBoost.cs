using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : MonoBehaviour
{

    public GameObject glow;

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player")
        {

            other.gameObject.GetComponent<PlayerHealth>().GainHealth(7f);
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            glow.SetActive(false);

        }

    }
}
