using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{
    public GameObject player;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {

        Vector3 m_Velocity = Vector3.zero;

        if (player.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
            Vector3 targetVelocity = new Vector2(-1f * 4f, rb.velocity.y);
            // And then smoothing it out and applying it to the character
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref (m_Velocity), .05f);

        }
        else
        {
            spriteRenderer.flipX = false;
            Vector3 targetVelocity = new Vector2(1f * 4f, rb.velocity.y);
            // And then smoothing it out and applying it to the character
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref (m_Velocity), .05f);

        }



    }

}
