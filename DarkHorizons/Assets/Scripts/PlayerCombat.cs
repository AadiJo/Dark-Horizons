using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;
    bool isAttacking = false;
    public Transform attackPoint;
    public float attackRange = 0.5f;

    private Rigidbody2D rb;

    public LayerMask enemyLayers;

    public PlayerMovement playerMovement;

    public int attackDamage = 10;
    private PlayerHealth health;

    void Start()
    {

        health = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {

            Attack();

        }

        if (isAttacking)
        {

            playerMovement.canMove = false;

        }
        else
        {

            playerMovement.canMove = true;

        }

    }

    private void Attack()
    {


        if (!animator.GetBool("isJumping") && !animator.GetBool("isFalling"))
        {
            //Play animation
            animator.SetInteger("attackRandomizer", Random.Range(0, 2));
            animator.SetTrigger("attack");
            StartCoroutine(stopMovement());

            // Detect enemies in range

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            // Damage
            foreach (Collider2D enemy in hitEnemies)
            {

                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);

            }
        }



    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {

            return;

        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }

    IEnumerator stopMovement()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.layer == 9)
        {

            health.TakeDamage(1);
            if (rb.velocity.x < 0)
            {

                rb.AddForce(new Vector2(2000, 200));

            }
            else
            {

                rb.AddForce(new Vector2(-2000, 200));

            }



        }

    }
}
