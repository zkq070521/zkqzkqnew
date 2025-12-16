using UnityEngine;

public class CoinCollector : MonoBehaviour
{
  
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player碰到coin，加金币");

            
            if (CoinManager.Instance != null)
            {
                CoinManager.Instance.AddCoin(); // 加1个金币
            }
            else
            {
                Debug.LogError("先加载UI场景");
            }

            // 2. 销毁coin
            Destroy(gameObject);
        }
    }
}