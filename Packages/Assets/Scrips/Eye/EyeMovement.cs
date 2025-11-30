using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EyeMovement : MonoBehaviour
{
    // 移动速度
    public float moveSpeed = 10f;
    // 爆炸效果预制体
    //public GameObject explosionPrefab;
    // Enemy目标列表
    private GameObject enemy;
    // 当前目标敌人
    //private GameObject currentTarget;

    void Start()
    {
        // 初始查找所有Enemy
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    void OnEnable()
    {
        // 订阅场景切换事件
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // 取消订阅
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 场景加载完成后调用
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 检查是否切换到Domain场景
        if (scene.name == "Domain")
        {
            Debug.Log("是这里");
            enemy = GameObject.FindGameObjectWithTag("Enemy");
            Vector2 direction = (enemy.transform.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    void Update()
    {
        // 如果当前没有目标，尝试寻找新目标
        /*if (currentTarget == null || !currentTarget.activeSelf)
        {
            FindNewTarget();
        }*/

        // 如果有目标，向目标移动
        /*if (currentTarget != null)
        {
            MoveTowardsTarget();
        }*/
    }

    // 查找所有Enemy
    /*void FindEnemies()
    {
        enemies.Clear();
        GameObject[] foundEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemies.AddRange(foundEnemies);
        //FindNewTarget();
    }*/

    // 寻找新的目标敌人
    /*void FindNewTarget()
    {
        if (enemies.Count == 0) return;

        // 简单的目标选择：距离最近的敌人
        float minDistance = float.MaxValue;
        GameObject closestEnemy = null;

        foreach (var enemy in enemies)
        {
            if (enemy == null || !enemy.activeSelf) continue;

            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy;
            }
        }

        currentTarget = closestEnemy;
    }
*/
    // 向目标移动
    void MoveTowardsTarget()
    {
        Vector2 direction = (enemy.transform.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    // 碰撞检测
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 播放爆炸效果
           /* if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }*/

            // 停用当前物体（而不是销毁，以便可以重新启用）
            gameObject.SetActive(false);
        }
    }
}