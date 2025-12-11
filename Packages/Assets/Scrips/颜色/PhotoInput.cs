using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class PhotoInput : MonoBehaviour
{
    public int muralID = 0;
    private PhotoManger photoManager;
    private Camera mainCamera;


    private InputAction clickAction;

    void Start()
    {
        mainCamera = Camera.main;
        photoManager = FindObjectOfType<PhotoManger>();

        PlayerInput playerInput = FindObjectOfType<PlayerInput>();

        if (playerInput != null && playerInput.actions != null)
        {
           
            clickAction = playerInput.actions.FindAction("PhotoMouse/click");
            if (clickAction != null)
            {
               
                clickAction.performed -= OnClickActionPerformed;
                clickAction.performed += OnClickActionPerformed;
                clickAction.Enable();
                
            }
            
        }
        else
        {
            Debug.LogError($"没有PlayerInput组件或InputActions为空。");
        }
    }


    private void OnClickActionPerformed(InputAction.CallbackContext context)
    {

       
        StartCoroutine(DelayedClickCheck());
        // 启动协程延迟处理,暂停

       // StartCoroutine(DelayedClickCheck());
    }

    private IEnumerator DelayedClickCheck()
    {
        yield return null;

       

        Vector2 screenPos = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(screenPos);
        // 新增：扩大射线检测距离（默认100，改成1000，避免距离不够）
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 1000f);

        if (hit.collider == null)
        {
            Debug.Log("射线未命中任何Collider2D → 检查物体是否有Collider2D/层级是否匹配");
        }
        else
        {
            Debug.Log($"射线命中：{hit.collider.gameObject.name}，当前物体名：{this.gameObject.name}");
        }

        if (hit.collider != null && hit.collider.gameObject == this.gameObject)
        {
            if (photoManager != null)
            {
                photoManager.OpenGallery(muralID);
            }
        }
    }




    private void OnDisable()
    {
        if (clickAction != null)
        {
            clickAction.performed -= OnClickActionPerformed;
            
        }
    }

    private void OnEnable()
    {
        if (clickAction != null)
        {
            clickAction.performed += OnClickActionPerformed;
            // 只有Action未启用时才启用
            if (!clickAction.enabled) clickAction.Enable();
        }
    }
}
