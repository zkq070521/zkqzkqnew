using UnityEngine;

public class UFO_PlayerFollow : MonoBehaviour
{
    [Header("ÅäÖÃ")]
    public string playerTag = "Player";
    public float triggerHeight = 0.3f;
    private GameObject currentPlayer; 

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag(playerTag))
        {
            GameObject player = other.gameObject;
            
            float yOffset = player.transform.position.y - transform.position.y;
            if (yOffset > 0 && yOffset <= triggerHeight)
            {
                currentPlayer = player;
                currentPlayer.transform.SetParent(transform);
                Debug.Log("PlayerÌøÉÏ·Éµú£¬¿ªÊ¼¸úËæ");
            }
        }
    }

    
    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.CompareTag(playerTag) && currentPlayer != null && other.gameObject == currentPlayer)
        {
            
            currentPlayer.transform.SetParent(null);
            Debug.Log("PlayerÀë¿ª·Éµú£¬½â³ý¸úËæ");
            currentPlayer = null; 
        }
    }
}