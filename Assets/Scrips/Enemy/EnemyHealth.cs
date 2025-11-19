using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
 

    public float maxHealth;
    public float currentHealth;
    public Animator animator;
    public bool isDead = false;



    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }

    }

    void Die()
    {
        isDead = true;
        //animator.SetTrigger("Death");

        //½ûÓÃÍæ¼Ò
        //GetComponent<PlayerController>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}


