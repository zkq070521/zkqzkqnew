using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public InvincibilityManager invincibilityManager; // 引用无敌脚本
    public int maxHealth;
    public int currentHealth;
    public Animator animator;
    public bool isDead = false;
    //public Rigidbody2D rb;
    public Vector3 startPosition;
    public Transform playerTransform;

    public SpriteRenderer playerSpriteRenderer; 
    private Color originalColor; // 保存原来的颜色
    private bool isFlashingRed; // 防止重复闪红



    public UnityEvent<PlayerHealth> OnHealthChange;

    
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //rb = GameObject.FindGameObjectWithTag("Player").Rigidbody2D;
        startPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        currentHealth = maxHealth;
        OnHealthChange?.Invoke(this);

        originalColor = playerSpriteRenderer.color;
        isFlashingRed = false;
    }

    private void Update()
    {
        OnHealthChange?.Invoke(this);
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            OnHealthChange?.Invoke(this);
        }
    }

    public void TakeDamage(int damage)
    {
        if (invincibilityManager != null && invincibilityManager.IsInvincible())
        {
            Debug.Log("处于无敌状态，不受伤害！");
            return;
        }


            currentHealth -= damage;
        Debug.Log("广播");
        OnHealthChange?.Invoke(this);

        if (!isFlashingRed)
        {
            SetRedColor(); // 变红色
            Invoke("ResetOriginalColor", 0.2f); 
        }


        if (currentHealth <= 0)
        {
            playerTransform.position = startPosition;
            currentHealth = maxHealth;
            OnHealthChange?.Invoke(this);
            //rb.position = startPosition;
        }
        
        //OnHealthChange?.Invoke(this);
    }

    private void SetRedColor()
    {
        isFlashingRed = true; 
        
            playerSpriteRenderer.color = Color.red;
        
    }

    private void ResetOriginalColor()
    {
        isFlashingRed = false;
         playerSpriteRenderer.color = originalColor;
       
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        OnHealthChange?.Invoke(this);
    }
}
