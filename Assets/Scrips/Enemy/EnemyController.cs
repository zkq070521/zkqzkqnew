using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyController : MonoBehaviour
{

    // 激光发射点 
    public Transform laserOrigin;

    // 射击间隔
    public float shootInterval = 1f;
    private float lastShotTime;

    // 激光可视效果
    public LineRenderer laserLine;

    // 主角引用
    private GameObject player;
    private PlayerHealth playerHealth;

    public LayerMask layermask;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // "Player"标签
        playerHealth = player.GetComponent<PlayerHealth>();
        laserLine.enabled = false; // 初始隐藏激光
    }

    void Update()
    {
        // 检查是否到了可以射击的时间

    }

    private void FixedUpdate()
    {
        if (Time.time - lastShotTime >= shootInterval)
        {
            // 看到主角
            if (CanSeePlayer())
            {
                // 发射激光 (显示效果)
                laserLine.enabled = true;
                laserLine.SetPosition(0, laserOrigin.position); // 起点：敌人激光点
                laserLine.SetPosition(1, player.transform.position); // 终点：主角位置


                playerHealth.TakeDamage(20);

                // 更新上次射击时间
                lastShotTime = Time.time;
            }
            else
            {
                // 看不到主角，隐藏激光
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

}


