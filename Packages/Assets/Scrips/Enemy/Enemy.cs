using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [Header("巡逻设置")]
    public float patrolRange = 5f; // 巡逻范围（左右移动距离）
    public float moveSpeed = 2f;   // 移动速度

    private float startingX;      // 起始X坐标
    private float moveDirection = 3; // 移动方向（1右，-1左）
    private bool isMovingRight = true; // 当前移动方向

    void Start()
    {
        // 记录起始位置
        startingX = transform.position.x;
    }

    void Update()
    {
        // 计算移动
        float newX = transform.position.x + moveDirection * moveSpeed * Time.deltaTime;

        // 检查是否到达边界
        if (newX > startingX + patrolRange)
        {
            newX = startingX + patrolRange; // 确保不超过范围
            ChangeDirection();
        }
        else if (newX < startingX - patrolRange)
        {
            newX = startingX - patrolRange; // 确保不超过范围
            ChangeDirection();
        }

        // 应用新位置
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    // 改变移动方向
    void ChangeDirection()
    {
        moveDirection *= -1;
        isMovingRight = !isMovingRight;


        //transform.localScale = new Vector3(moveDirection, 3, 3);
    }

   
}