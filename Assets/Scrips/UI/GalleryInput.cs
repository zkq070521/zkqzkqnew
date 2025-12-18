using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections; // 检查是否点击在UI上


public class GalleryInput : MonoBehaviour
{
    public int muralID = 0;
    private GalleryManager galleryManager;
    private Camera mainCamera;

    
    private InputAction clickAction;

    void Start()
    {
        mainCamera = Camera.main;
        galleryManager = FindObjectOfType<GalleryManager>();

        PlayerInput playerInput = FindObjectOfType<PlayerInput>();

        if (playerInput != null && playerInput.actions != null)
        {
            
            clickAction = playerInput.actions.FindAction("UI/Click"); // 格式：ActionMap/Action

           
            if (clickAction != null)
            {
                
                clickAction.performed += OnClickActionPerformed;
                
                clickAction.Enable();
               
            }
            else
            {
                Debug.LogError($"未找到 ‘UI/Click‘");
            }
        }
        else
        {
            Debug.LogError($"没有PlayerInput组件。");
        }
    }

    
    private void OnClickActionPerformed(InputAction.CallbackContext context)
    {
        // 启动协程延迟处理
        StartCoroutine(DelayedClickCheck());
    }

    private IEnumerator DelayedClickCheck()
    {
        // 等待一帧，让UI事件系统先更新
        yield return null;

       

        Vector2 screenPos = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(screenPos);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null && hit.collider.gameObject == this.gameObject)
        {
            if (galleryManager != null)
            {
                galleryManager.OpenGallery(muralID);
            }
        }
    }

   
   

    
    private void OnDisable()
    {
        if (clickAction != null && clickAction.enabled)
        {
            clickAction.performed -= OnClickActionPerformed;
            clickAction.Disable();
        }
    }

    private void OnEnable()
    {
        if (clickAction != null && !clickAction.enabled)
        {
            clickAction.performed += OnClickActionPerformed;
            clickAction.Enable();
        }
    }
}