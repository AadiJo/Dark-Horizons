using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class BigBoss : MonoBehaviour
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
    private bool grounded = true;
    public LayerMask playerLayer;
    private GameManager gameManager;
    public GameObject healthbar;
    public TriggerEvent secondEvent;
    public enum directions { right, left };
    public LayerMask groundLayers;
    public Transform groundCheck;

    [Header("Attributes")]
    [Space]

    public float maxHealth = 50f;
    public bool canJump = false;
    public float triggerRange = 6f;
    public float jumpForce = 350f;

    public float speed = 1f;
    public float attackDamage = 10f;
    public float attackRange = 0.5f;
    public directions defaultOrientation = directions.right;

    public float[] knockback = new float[] { 300f, 200f };





    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        healthbar.gameObject.GetComponent<Slider>().value = maxHealth;
        gameManager = FindObjectOfType<GameManager>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        if (player == null)
        {
            Debug.LogError("Player not found");
        }
    }

    void Update()
    {

        animator.SetBool("Grounded", grounded);

        if (Mathf.Abs(player.transform.position.x - transform.position.x) < triggerRange)
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
                    if (defaultOrientation == directions.right)
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
                else
                {

                    if (defaultOrientation == directions.right)
                    {

                        if (!m_Right)
                        {

                            Flip();

                        }
                    }
                    else
                    {

                        if (m_Right)
                        {

                            Flip();

                        }

                    }

                }

            }



            if (m_Right)
            {

                if (defaultOrientation == directions.right)
                {

                    direction = Vector2.right;

                }
                else
                {

                    direction = Vector2.left;

                }


            }
            else
            {

                if (defaultOrientation == directions.right)
                {

                    direction = Vector2.left;

                }
                else
                {

                    direction = Vector2.right;

                }

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

    private void FixedUpdate()
    {

        if (canJump)
        {
            grounded = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, groundLayers);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    grounded = true;
                }
            }



        }







    }

    private void Attack()
    {

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayers)
        {

            player.GetComponent<PlayerHealth>().TakeDamage(attackDamage, knockback);
            if (player.gameObject.GetComponent<PlayerHealth>().currentHealth <= 0)
            {

                Regex rgx = new Regex("[^a-zA-Z -]");
                string name = gameObject.name;
                name = rgx.Replace(name, "");
                name = name.ToUpper();
                name = name.Replace("_", " ");
                if (name.Contains("CLONE"))
                {

                    name = name.Substring(0, name.Length - 5);

                }
                gameManager.killerName = name;
                //Debug.Log("Death by " + name);

            }
        }

    }

    public void TakeDamage(int damage)
    {

        currentHealth -= damage;
        StartCoroutine(DelayHealthAnim());


        if (currentHealth <= 1)
        {

            Die();

        }
        FindObjectOfType<AudioManager>().Play("EnemyHit");

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

    private void Jump()
    {

        grounded = false;
        animator.SetTrigger("Jump");
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

    }

    IEnumerator AttackDelay()
    {

        canAttack = false;
        yield return new WaitForSeconds(1f);
        canAttack = true;
        Jump();

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
        healthbar.gameObject.SetActive(false);
        if (GetComponent<TriggerEvent>() != null)
        {

            GetComponent<TriggerEvent>().triggered = true;

        }
        if (secondEvent != null)
        {

            secondEvent.triggered = true;

        }




        //GetComponent<Animator>().enabled = false;



    }

    IEnumerator DelayHealthAnim()
    {
        yield return new WaitForSeconds(0.4f);
        healthbar.gameObject.GetComponent<Slider>().value = currentHealth;




    }
}
