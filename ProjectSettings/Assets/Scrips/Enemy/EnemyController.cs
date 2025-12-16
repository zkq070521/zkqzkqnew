using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    public Transform laserOrigin;

   
    public float shootInterval = 1f;
    private float lastShotTime;

    
    public LineRenderer laserLine;

 
    private GameObject player;
    private PlayerHealth playerHealth;

    public LayerMask layermask;

   
    public float maxShootDistance = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
        if (player != null) 
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
        laserLine.enabled = false; 
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
       
        if (player == null) return;

      
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (Time.time - lastShotTime >= shootInterval)
        {
          
            if (distanceToPlayer <= maxShootDistance && CanSeePlayer())
            {
               
                laserLine.enabled = true;
                laserLine.SetPosition(0, laserOrigin.position); // 起点：敌人激光点
                laserLine.SetPosition(1, player.transform.position); // 终点：主角位置

                playerHealth?.TakeDamage(5); 

            
                lastShotTime = Time.time;
            }
            else
            {
              
                laserLine.enabled = false;

                //重置血量
                //playerHealth.ResetHealth();
            }
        }
    }

    bool CanSeePlayer()
    {
        // 计算从敌人激光点到主角的方向和距离
        Vector2 directionToPlayer = (player.transform.position - laserOrigin.position).normalized;//必须归一化，方向向量的模长是1
        float distanceToPlayer = Vector2.Distance(laserOrigin.position, player.transform.position);

        // 发射射线 (起点：激光点，方向：指向主角，距离：到主角的距离)
        RaycastHit2D hit = Physics2D.Raycast(laserOrigin.position, directionToPlayer, distanceToPlayer, layermask);

        // 检查射线击中了什么
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("击中主角");
                return true;
            }
            else
            {
                // 击中墙壁/敌人等障碍物，返回false
                Debug.Log("被阻挡");
                return false;
            }
        }
        else
        {
            // 射线没有击中任何东西
            Debug.LogWarning("射线未击中任何对象！");
            return false;
        }
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // 颜色：红色
        Gizmos.DrawWireSphere(transform.position, maxShootDistance); // 绘制线框球体，显示射击范围
    }
}