using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapdoor_Bottom : MonoBehaviour
{
    public GameObject spikeballPrefab;
    private float roofHeight = -28.41f;
    private float destroyDelay = 5f;

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.name == "Player")
        {

            GameObject newSpikeball = Instantiate(spikeballPrefab, new Vector2(other.transform.position.x, roofHeight), transform.rotation);
            Destroy(newSpikeball, 5f);

        }


    }
}
