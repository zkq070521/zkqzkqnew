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
        
        if (transform.position.y >= maxY)
        {
            moveDirection = -1;
            
            transform.position = new Vector3(
                transform.position.x,
                maxY,
                transform.position.z
            );
            targetPosition.y = maxY; // 同步目标位置（平滑模式用
        }
        
        else if (transform.position.y <= minY)
        {
            moveDirection = 1;
            
            transform.position = new Vector3(
                transform.position.x,
                minY,
                transform.position.z
            );
            targetPosition.y = minY; 
        }
    }

  
   
}