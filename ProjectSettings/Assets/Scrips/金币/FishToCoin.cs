using UnityEngine;

public class FishToCoin : MonoBehaviour
{
    
    public GameObject coinPrefab;

    
   public LayerMask groundLayer;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (((1 << other.gameObject.layer) & groundLayer) != 0)//掩码是啥
        {
            Debug.Log("fish碰到地面，转换为coin");

            
            if (coinPrefab != null)
            {
                Instantiate(coinPrefab, transform.position, transform.rotation);//rotation是角度
            }
            else
            {
                Debug.LogError("coin预制体未赋值！");
            }

            
            Destroy(gameObject);
        }
    }

    // 备选：若用非触发器碰撞（IsTrigger=false），用此方法
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (((1 << other.gameObject.layer) & groundLayer) != 0)
    //     {
    //         // 同上生成coin+销毁fish逻辑
    //     }
    // }
}