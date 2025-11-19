using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    
    public int maxHealth;
    public int currentHealth;
    public Animator animator;
    public bool isDead = false;
    //public Rigidbody2D rb;
    public Vector3 startPosition;
    public Transform playerTransform;

    public UnityEvent<PlayerHealth> OnHealthChange;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //rb = GameObject.FindGameObjectWithTag("Player").Rigidbody2D;
        startPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        currentHealth = maxHealth;
        OnHealthChange?.Invoke(this);

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnHealthChange?.Invoke(this);
        if (currentHealth <= 0)
        {
            playerTransform.position = startPosition;
            currentHealth = maxHealth;
            //rb.position = startPosition;
        }
        
    }

    

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        OnHealthChange?.Invoke(this);
    }
}
