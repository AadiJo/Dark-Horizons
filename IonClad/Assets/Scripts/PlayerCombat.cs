using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;
    bool isAttacking = false;

    public PlayerMovement playerMovement;

    void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {

            Attack();

        }

        if (isAttacking)
        {

            playerMovement.canMove = false;

        }else
        {

            playerMovement.canMove = true;

        }

    }

    private void Attack()
    {

        if (!animator.GetBool("isJumping") && !animator.GetBool("isFalling"))
        {
            animator.SetInteger("attackRandomizer", Random.Range(0, 2));
            animator.SetTrigger("attack");
            StartCoroutine(stopMovement());

        }
        


    }

    IEnumerator stopMovement()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.7f);
        isAttacking = false;

    }
}
