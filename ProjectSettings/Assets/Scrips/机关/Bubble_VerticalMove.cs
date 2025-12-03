using UnityEngine;


public class Bubble_VerticalMove : MonoBehaviour
{
    [Header("移动配置")]
    public float moveSpeed = 1.5f; 
    public float minY ; 
    public float maxY ; 
    private int moveDirection = 1; 

    
    public bool useSmoothMove = true; //平滑移动
    public float smoothFactor = 2f; // 平滑系数（越大越顺滑

    private Vector3 targetPosition; 

    private void Start()
    {
        
        targetPosition = transform.position;
        
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, minY, maxY),
            transform.position.z
        );
    }

    private void Update()
    {
        if (useSmoothMove)
        {
            
            SmoothVerticalMove();
        }
        else
        {
           
            NormalVerticalMove();
        }

       
        CheckBoundaries();
    }

    
    private void NormalVerticalMove()
    {
        Vector3 newPos = transform.position;
        // 只在Y轴移动，X、Z轴保持不变
        newPos.y += moveDirection * moveSpeed * Time.deltaTime;
        transform.position = newPos;
    }

   
    private void SmoothVerticalMove()
    {
        // 更新目标位置（按当前方向向边界移动）
        targetPosition.y += moveDirection * moveSpeed * Time.deltaTime;
        // 平滑插值到目标位置
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothFactor * Time.deltaTime
        );
    }

    
    private void CheckBoundaries()
    {
        // 到达上边界（maxY）→ 切换为向下移动
        if (transform.position.y >= maxY)
        {
            moveDirection = -1;
            // 强制停在边界上（避免超出范围）
            transform.position = new Vector3(
                transform.position.x,
                maxY,
                transform.position.z
            );
            targetPosition.y = maxY; // 同步目标位置（平滑模式用）
        }
        // 到达下边界（minY）→ 切换为向上移动
        else if (transform.position.y <= minY)
        {
            moveDirection = 1;
            // 强制停在边界上
            transform.position = new Vector3(
                transform.position.x,
                minY,
                transform.position.z
            );
            targetPosition.y = minY; // 同步目标位置（平滑模式用）
        }
    }

    /// <summary>
    /// Gizmos可视化上下边界（Scene窗口中显示，方便调整）
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan; // 边界线颜色为青色
        float width = 2f; // 边界线宽度（根据气泡大小调整）

        // 绘制下边界线（水平横线）
        Gizmos.DrawLine(
            new Vector3(transform.position.x - width, minY, transform.position.z),
            new Vector3(transform.position.x + width, minY, transform.position.z)
        );
        // 绘制上边界线（水平横线）
        Gizmos.DrawLine(
            new Vector3(transform.position.x - width, maxY, transform.position.z),
            new Vector3(transform.position.x + width, maxY, transform.position.z)
        );
        // 绘制边界范围框（矩形）
        Gizmos.DrawWireCube(
            new Vector3(transform.position.x, (minY + maxY) / 2f, transform.position.z),
            new Vector3(width * 2f, maxY - minY, 0.1f)
        );
    }
}