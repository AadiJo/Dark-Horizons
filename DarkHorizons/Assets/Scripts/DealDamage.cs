using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Text.RegularExpressions;

public class DealDamage : MonoBehaviour
{

    [Header("Parameters")]
    [Space]
    public float damage = 4f;
    public float[] knockback = new float[] { 200f, 200f };
    private bool canDamage = true;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            if (canDamage)
            {

                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage, knockback);
                if (other.gameObject.GetComponent<PlayerHealth>().currentHealth <= 0)
                {

                    Regex rgx = new Regex("[^a-zA-Z -]");
                    string name = gameObject.name;
                    name = rgx.Replace(name, "");
                    name = name.ToUpper();
                    gameManager.killerName = name;
                    //Debug.Log("Death by " + name);

                }
                StartCoroutine(DisableDmg());

            }


        }

    }

    IEnumerator DisableDmg()
    {

        canDamage = false;
        yield return new WaitForSeconds(0.5f);
        canDamage = true;

    }
}
