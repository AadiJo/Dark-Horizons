using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{

    private bool triggered = false;
    private GameObject player;
    private bool m_Right = true;
    public GameObject glow;
    private float velocity = 1;
    private Vector2 direction = Vector2.right;
    private Animator animator;
    private bool canAttack = true;
    private float currentHealth;
    public Transform attackPoint;
    public LayerMask playerLayer;

    [Header("Attributes")]
    [Space]

    public float maxHealth = 50f;

    public float speed = 1f;
    public float attackDamage = 10f;
    public float attackRange = 0.5f;

    public float[] knockback = new float[] { 300f, 200f };





    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        if (player == null)
        {
            Debug.LogError("Player not found");
        }
    }

    void Update()
    {

        if (Mathf.Abs(player.transform.position.x - transform.position.x) < 6f)
        {

            triggered = true;

        }

        if (triggered)
        {

            animator.SetFloat("Speed", 2.5f);

            if (canAttack)
            {

                if ((player.transform.position.x - transform.position.x) < 0)
                {

                    if (m_Right)
                    {

                        Flip();

                    }


                }
                else
                {

                    if (!m_Right)
                    {

                        Flip();

                    }

                }

            }



            if (m_Right)
            {

                velocity = 1;
                direction = Vector2.right;

            }
            else
            {

                velocity = -1;
                direction = Vector2.left;

            }

            if (Mathf.Abs(player.transform.position.x - transform.position.x) < 2f)
            {
                if (canAttack)
                {
                    Attack();
                    animator.SetTrigger("Attack");
                    StartCoroutine(AttackDelay());

                }


            }

            if (canAttack)
            {

                float distance = speed * Time.deltaTime;
                transform.Translate(direction * distance);

            }







        }

    }

    private void Attack()
    {

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayers)
        {

            player.GetComponent<PlayerHealth>().TakeDamage(attackDamage, knockback);
            Debug.Log(player.gameObject.name);
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


        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        GetComponent<BoxCollider2D>().enabled = false;






        //StartCoroutine(leaveRemains());

        this.enabled = false;

    }

    private void Flip()
    {
        m_Right = !m_Right;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {

            return;

        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }

    IEnumerator AttackDelay()
    {

        canAttack = false;
        yield return new WaitForSeconds(0.3f);
        canAttack = true;

    }

    IEnumerator hurtDelay()
    {
        yield return new WaitForSeconds(0.5f);

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

        yield return new WaitForSeconds(2.5f);
        GetComponent<SpriteRenderer>().enabled = false;

        //GetComponent<Animator>().enabled = false;



    }
}
