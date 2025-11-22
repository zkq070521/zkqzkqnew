
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    
    public Transform player;
   
    public float detectionRange = 10f;
    
    public float attackRange = 2f;
    
    public float moveSpeed = 10f;
    // 攻击间隔时间
    public float attackCooldown = 1f;

    public int attackDamage = 10;

    private bool isAttacking = false;
    private float lastAttackTime;

    private Rigidbody2D rb;
    private int faceDir;
    private Animator anim;



    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        Vector2 d = new Vector2 (player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        if(d.x > 0)
        {
            faceDir = -1;
        }
        else if(d.x < 0)
        {
            faceDir = 1;
        }
            transform.localScale = new Vector3(faceDir, 1, 1);
        anim.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
       
            // 计算敌人与玩家之间的距离
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);


        

        // 如果玩家在检测范围内
        if (distanceToPlayer <= detectionRange)
        {
            anim.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
            // 如果玩家在攻击范围内
            if (distanceToPlayer <= attackRange)
            {
                // 发动攻击
                isAttacking = true;
                Attack();
                

            }
            else
            {
                // 向玩家移动
                isAttacking = false;
                anim.SetBool("isAttack", isAttacking);
                MoveTowardsPlayer();
                anim.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));

            }
            
        }
    }

    private void MoveTowardsPlayer()
    {
       
        // 计算移动方向;向Player移动
        Vector2 direction = (player.position - transform.position).normalized;
        // 计算移动速度
        Vector2 movement = direction * moveSpeed;
        // 移动敌人
        rb.velocity = new Vector2(movement.x,rb.velocity.y);
        anim.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
    }

    private void Attack()
    {
        if (isAttacking && Time.time - lastAttackTime >= attackCooldown)
        {
            
            lastAttackTime = Time.time;

            anim.SetBool("isAttack", isAttacking);

            // 处理伤害
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }

            // 攻击结束
            isAttacking = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 检测范围
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        // 攻击范围
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}


