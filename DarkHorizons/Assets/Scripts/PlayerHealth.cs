using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public Slider healthBar;

    public bool dead = false;

    private float lerpSpeed = 0.55f;
    private float time;
    private Animator animator;

    public float currentHealth;
    private float maxHealth = 20;

    public void SetHealthBar(float maxHealth)
    {

        healthBar.maxValue = maxHealth;
        currentHealth = maxHealth;
        healthBar.value = currentHealth;

    }


    private void Start()
    {
        SetHealthBar(maxHealth);
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {

        animator.SetTrigger("hurt");
        currentHealth -= damage;
        time = 0;

    }
    public void GainHealth(float health)
    {

        currentHealth += health;
        time = 0;

    }

    private void Update()
    {

        if (currentHealth <= 0)
        {

            dead = true;
            currentHealth = 0;


        }

        AnimateHealthBar();


    }

    private void AnimateHealthBar()
    {

        float targetHealth = currentHealth;
        float startHealth = healthBar.value;
        time += Time.deltaTime * lerpSpeed;

        healthBar.value = Mathf.Lerp(startHealth, targetHealth, time);

    }


}
