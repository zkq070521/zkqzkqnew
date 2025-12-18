using UnityEngine;

public class SimpleDoorTrigger : MonoBehaviour
{
    
    public Collider2D triggerCollider; 
    public string playerTag = "Player"; 

    private bool isPlayerInside = false;
    private bool isUIOpen = false; 

    void Start()
    {
        isPlayerInside = false;
        isUIOpen = false;

        
        if (triggerCollider == null)
        {
            triggerCollider = GetComponent<Collider2D>();
        }

       
      
    }

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag(playerTag) && !isPlayerInside)
        {
            isPlayerInside = true;
            OpenPasswordUI(); 
        }
    }

    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && isPlayerInside)
        {
            isPlayerInside = false;
            ClosePasswordUI(); 
        }
    }

    
    private void OpenPasswordUI()
    {
        if (isUIOpen || DigitalPasswordManager.Instance == null) return;

        DigitalPasswordManager.Instance.ShowPasswordUI();
        isUIOpen = true;
      
    }

   
    private void ClosePasswordUI()
    {
        if (!isUIOpen || DigitalPasswordManager.Instance == null) return;

        DigitalPasswordManager.Instance.HidePasswordUI();
        isUIOpen = false;
   
    }

   
}