using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrigger : MonoBehaviour
{

    public GameObject objectToSpawn;
    private bool canSpawn = true;
    public float[] position = new float[] { 0, 0 };
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name == "Player")
        {
            if (canSpawn)
            {

                GameObject newSpikeball = Instantiate(objectToSpawn, new Vector2(position[0], position[1]), Quaternion.identity);
                newSpikeball.GetComponent<Rigidbody2D>().AddForce(new Vector2(300f, -100f));
                StartCoroutine(DelaySpawn());
                Destroy(newSpikeball, 7f);

            }



        }



    }

    IEnumerator DelaySpawn()
    {

        canSpawn = false;
        yield return new WaitForSeconds(3f);
        canSpawn = true;

    }

}
