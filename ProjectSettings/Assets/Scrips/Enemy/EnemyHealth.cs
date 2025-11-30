using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{


    public float maxHealth;
    public float currentHealth;
    public Animator animator;
    public bool isDead = false;
    public bool isWhite = false;




    void Start()
    {
        currentHealth = maxHealth;
    }

    /*public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }

    }*/

    void Die()
    {
        isDead = true;
        animator.SetBool("isDie",isDead);

        //禁用玩家
        //GetComponent<PlayerController>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy took " + damage + " damage. Current health: " + currentHealth);
        if (currentHealth <= 0)
        {
            // 敌人死亡处理
            Debug.Log("Enemy died!");
            //Destroy(gameObject);
            isDead = true;
            animator.SetBool("isDie", isDead);

            //禁用玩家
            //GetComponent<PlayerController>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            //animator.SetBool("isDie", isDead);
            //gameObject.SetActive(false);
            //动画状态，0是默认图层
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            /*if (stateInfo.IsName("Monkeydie") && stateInfo.normalizedTime >= 1)
            {*/
                isWhite = true;
                animator.SetBool("isKong", isWhite);
                DisableEnemy();
            //}
        }
    }

    void DisableEnemy()
    {
        // 禁用敌人的碰撞体，防止继续与其他物体交互
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // 禁用敌人的刚体，防止物理模拟继续影响敌人
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.isKinematic = true;
            rigidbody.detectCollisions = false;
        }

        // 禁用敌人的脚本，停止敌人的行为逻辑
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }

        // 可以选择将敌人对象设置为不活动状态
        gameObject.SetActive(false);
    }
}




   
    
