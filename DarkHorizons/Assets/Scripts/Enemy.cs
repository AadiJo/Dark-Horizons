using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 10;
    int currentHealth;
    public GameObject glow;
    public Animator animator;
    public float attackPower;
    private GameObject player;

    void Start()
    {

        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {

        if (animator.GetBool("Dead") == true)
        {

            FindObjectOfType<AudioManager>().Play("EnemyDie");

        }
        if (currentHealth > 0)
        {

            if (Mathf.Abs(player.transform.position.x - transform.position.x) <= 2)
            {

                StartCoroutine(DelayAttackAnim());

            }

        }


    }

    public void TakeDamage(int damage)
    {

        if (currentHealth <= 0)
        {

            Die();

        }

        currentHealth -= damage;
        StartCoroutine(hurtDelay());

        animator.SetTrigger("Hurt");


    }

    void Die()
    {

        StartCoroutine(DelayDeathAnim());

        GetComponent<EnemyAI>().enabled = false;
        Debug.Log("Supposed to disable EnemyAI");

        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        GetComponent<CircleCollider2D>().enabled = false;






        //StartCoroutine(leaveRemains());

        this.enabled = false;

    }

    IEnumerator DelayDeathAnim()
    {
        yield return new WaitForSeconds(0.2f);
        transform.position = new Vector2(transform.position.x, transform.position.y - 0.12f);
        animator.SetBool("Dead", true);
        FindObjectOfType<AudioManager>().Play("EnemyDie");
        if (glow != null)
        {
            glow.SetActive(false);
        }

        //GetComponent<Animator>().enabled = false;



    }

    IEnumerator hurtDelay()
    {
        yield return new WaitForSeconds(0.5f);

    }
    IEnumerator DelayAttackAnim()
    {
        animator.SetBool("Run", false);
        GetComponent<EnemyAI>().enabled = false;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        if (currentHealth > 0)
        {

            animator.SetBool("Run", true);
            GetComponent<EnemyAI>().enabled = true;

        }


    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Player")
        {


            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(attackPower);

        }

    }





}
