using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemyup : MonoBehaviour
{
    [Header("巡逻设置")]
    public float patrolRange = 5f; // 巡逻范围（左右移动距离）
    public float moveSpeed = 2f;   // 移动速度

    private float startingY;      // 起始X坐标
    private float moveDirection = 3; // 移动方向（1右，-1左）
    private bool isMovingRight = true; // 当前移动方向

    void Start()
    {
        // 记录起始位置
        startingY = transform.position.y;
    }

    void Update()
    {
        // 计算移动
        float newY = transform.position.y + moveDirection * moveSpeed * Time.deltaTime;

        // 检查是否到达边界
        if (newY > startingY + patrolRange)
        {
            newY = startingY + patrolRange; // 确保不超过范围
            ChangeDirection();
        }
        else if (newY < startingY - patrolRange)
        {
            newY = startingY - patrolRange; // 确保不超过范围
            ChangeDirection();
        }

        // 应用新位置
        transform.position = new Vector3(transform.position.x, newY,  transform.position.z);
    }

    // 改变移动方向
  void ChangeDirection()
    {
        moveDirection *= -1;
        isMovingRight = !isMovingRight;


        //transform.localScale = new Vector3(3, moveDirection, 3);
    }


}