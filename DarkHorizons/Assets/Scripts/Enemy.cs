using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 10;
    int currentHealth;
    public GameObject glow;
    public Animator animator;

    void Start()
    {

        currentHealth = maxHealth;

    }

    public void TakeDamage(int damage)
    {

        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {

            Die();

        }

    }

    void Die()
    {

        StartCoroutine(delayDeathAnim());



        GetComponent<Rigidbody2D>().gravityScale = 0;

        GetComponent<Collider2D>().enabled = false;

        //StartCoroutine(leaveRemains());

        this.enabled = false;

    }

    IEnumerator delayDeathAnim()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Dead", true);
        if (glow != null)
        {
            glow.SetActive(false);
        }



    }





}
