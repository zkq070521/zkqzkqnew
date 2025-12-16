using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : MonoBehaviour
{
    [Header("移动配置")]
    public float moveSpeed;
    public float left; // 左边界（x坐标最小值）
    public float right; // 右边界（x坐标最大值）
    private Vector2 startPosition;
    private int moveDirection; // 移动方向（1=向右，-1=向左）
    private bool isHit = false; 

    private void Start()
    {
       startPosition = transform.position;
        moveDirection = 1;
    }

    private void Update()
    {

        if (isHit) return;

        MoveUFO();
        CheckBoundaries();
        FlipUFOByDirection();
    }


    public void OnHitByRay()
    {
        isHit = true; // 停止移动

         Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 4f;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void MoveUFO()
    {
        Vector3 newPosition = transform.position;
        newPosition.x += moveDirection * moveSpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    private void CheckBoundaries()
    {
        if (transform.position.x >= right + startPosition.x)
        {
            moveDirection = -1;
        }
        else if (transform.position.x <= startPosition.x - left)
        {
            moveDirection = 1;
        }
    }

    private void FlipUFOByDirection()
    {
        Vector3 currentScale = transform.localScale;
        if (moveDirection == 1)
        {
            currentScale.x = 1f;
        }
        else if (moveDirection == -1)
        {
            currentScale.x = -1f;
        }
        transform.localScale = currentScale;
    }
}
