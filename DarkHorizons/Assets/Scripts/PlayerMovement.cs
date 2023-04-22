using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    private PlayerHealth health;

    public float runSpeed = 40f;
    private float originalRunSpeed;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool attacking = false;
    Vector3 initialCords;

    public bool canMove = true;

    void Start()
    {

        health = GetComponent<PlayerHealth>();
        originalRunSpeed = runSpeed;
        initialCords = transform.position;

    }
    void Update()
    {
        if (health.dead)
        {

            canMove = false;

        }

        if (canMove)
        {

            runSpeed = originalRunSpeed;

        }
        else
        {

            runSpeed = 0;

        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;




        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (canMove)
        {

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("isJumping", true);
                if (controller.m_Grounded)
                {

                    FindObjectOfType<AudioManager>().Play("Jump");

                }

            }

        }


        if (controller.m_Falling)
        {

            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);

        }

        if (canMove)
        {

            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }


        }


    }

    public void OnLanding()
    {

        animator.SetBool("isFalling", false);

    }

    public void onCrouching(bool isCrouching)
    {

        animator.SetBool("isCrouching", isCrouching);

    }

    void FixedUpdate()
    {

        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }


}