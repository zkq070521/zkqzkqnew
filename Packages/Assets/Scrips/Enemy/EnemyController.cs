using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // 新增：最大射击距离（可在Inspector面板调整，默认5米）
    public float maxShootDistance = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // "Player"标签
        if (player != null) // 新增：判空，避免Player不存在时报错
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
        laserLine.enabled = false; // 初始隐藏激光
    }

    void Update()
    {
        // 检查是否到了可以射击的时间（原有逻辑保留，无需改）
    }

    private void FixedUpdate()
    {
        // 新增：先判断Player是否存在（避免空引用）
        if (player == null) return;

        // 新增：计算敌人到Player的实际距离
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (Time.time - lastShotTime >= shootInterval)
        {
            // 修改：射击条件 = 距离在最大范围內 + 能看到Player
            if (distanceToPlayer <= maxShootDistance && CanSeePlayer())
            {
                // 发射激光 (显示效果)
                laserLine.enabled = true;
                laserLine.SetPosition(0, laserOrigin.position); // 起点：敌人激光点
                laserLine.SetPosition(1, player.transform.position); // 终点：主角位置

                playerHealth?.TakeDamage(5); // 新增：?. 判空，避免PlayerHealth不存在时报错

                // 更新上次射击时间
                lastShotTime = Time.time;
            }
            else
            {
                // 看不到主角 或 距离太远，隐藏激光
                laserLine.enabled = false;

                //重置血量（注释保留，按需开启）
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

    // 新增：Scene视图可视化最大射击距离（方便调试，可选）
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // 颜色：红色
        Gizmos.DrawWireSphere(transform.position, maxShootDistance); // 绘制线框球体，显示射击范围
    }
}