using UnityEngine;


public class UFO_Move : MonoBehaviour
{
    [Header("移动配置")]
    public float moveSpeed ; 
    public float leftBound ; // 左边界（x坐标最小值）
    public float rightBound ; // 右边界（x坐标最大值）

    private int moveDirection ; // 移动方向（1=向右，-1=向左）


    private void Start()
    {
        moveDirection = 1;
    }
    private void Update()
    {
        
        
        MoveUFO();

        
        CheckBoundaries();

        
        FlipUFOByDirection();
    }

    
    private void MoveUFO()
    {
        
        Vector3 newPosition = transform.position;
        newPosition.x += moveDirection * moveSpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    
    private void CheckBoundaries()
    {
        
        if (transform.position.x >= rightBound)
        {
            moveDirection = -1;
          
            //transform.position = new Vector3(rightBound, transform.position.y, transform.position.z);
        }
       
        else if (transform.position.x <= leftBound)
        {
            moveDirection = 1;
           
            //transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);
        }
    }

   
    private void FlipUFOByDirection()
    {
        Vector3 currentScale = transform.localScale;
        
        if (moveDirection == 1)
        {
            currentScale.x = 1f;
        }
        // 向左走 → scale.x=-1（反转）
        else if (moveDirection == -1)
        {
            currentScale.x = -1f;
        }
        transform.localScale = currentScale;
    }

    
}