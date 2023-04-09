using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 10;
    int currentHealth;
    public GameObject glow;
    private Animator animator;
    public float attackPower;
    private GameObject player;
    private GameManager gameManager;
    public float[] knockback = new float[] { 400f, 200f };

    void Start()
    {

        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();

    }

    void Update()
    {

        if (animator.GetBool("Dead") == true)
        {

            FindObjectOfType<AudioManager>().Play("EnemyDie");

        }
        if (currentHealth > 0)
        {

            if (Mathf.Abs(player.transform.position.x - transform.position.x) <= 2 && Mathf.Abs(player.transform.position.y - transform.position.y) <= 4)
            {

                StartCoroutine(DelayAttackAnim());

            }

        }


    }

    public void TakeDamage(int damage)
    {

        if (currentHealth <= 1)
        {

            Die();

        }
        FindObjectOfType<AudioManager>().Play("EnemyHit");
        currentHealth -= damage;
        StartCoroutine(hurtDelay());

        animator.SetTrigger("Hurt");


    }

    void Die()
    {

        StartCoroutine(DelayDeathAnim());

        GetComponent<EnemyAI>().enabled = false;
        //Debug.Log("Supposed to disable EnemyAI");

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

            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(attackPower, knockback);
            if (other.gameObject.GetComponent<PlayerHealth>().currentHealth <= 0)
            {

                Regex rgx = new Regex("[^a-zA-Z -]");
                string name = gameObject.name;
                name = rgx.Replace(name, "");
                name = name.ToUpper();
                gameManager.killerName = name;
                //Debug.Log("Death by " + name);

            }

        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {


            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(attackPower, knockback);
            if (other.gameObject.GetComponent<PlayerHealth>().currentHealth <= 0)
            {

                Regex rgx = new Regex("[^a-zA-Z -]");
                string name = gameObject.name;
                name = rgx.Replace(name, "");
                name = name.ToUpper();
                gameManager.killerName = name;
                //Debug.Log("Death by " + name);

            }

        }


    }





}
