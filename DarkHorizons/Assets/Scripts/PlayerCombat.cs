using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;
    bool isAttacking = false;
    public Transform attackPoint;
    public float attackRange = 0.5f;

    public LayerMask enemyLayers;

    public PlayerMovement playerMovement;

    public int attackDamage = 10;

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
}
