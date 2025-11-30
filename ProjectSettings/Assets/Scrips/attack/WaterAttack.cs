using UnityEngine;


public class WaterAttack : MonoBehaviour
{
    [Header("攻击设置")]
    public int damage = 20; 
    private Animator anim;  
    private bool hasAttacked = false; 

    void Start()
    {
        
        anim = GetComponent<Animator>();
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !hasAttacked)
        {
            AttackPlayer(other.gameObject);
        }
    }

    
    void AttackPlayer(GameObject player)
    {
        // 播放Attack动画
        anim.SetBool("IsAttacking", true);

        // 给玩家掉血
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null) 
        {
            playerHealth.TakeDamage(damage); // 掉血：每次掉20血（可改）
        }

        //防止重复攻击
        hasAttacked = true;

        // （可选）攻击后1秒重置，允许再次攻击（比如陷阱是持续伤害）
        Invoke("ResetAttack", 2f); // 1f代表1秒，可改成0.5f（0.5秒）
    }

    // 重置攻击状态（让陷阱能再次触发）
    void ResetAttack()
    {
        hasAttacked = false;
        anim.SetBool("IsAttacking", false); // 动画切回Idle
    }
}