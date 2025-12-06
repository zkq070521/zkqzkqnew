using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections; // 用于检查是否点击在UI上

[RequireComponent(typeof(Collider2D))]
public class GalleryInput : MonoBehaviour
{
    public int muralID = 0;
    private GalleryManager galleryManager;
    private Camera mainCamera;

    // 关键：声明一个对输入动作的引用
    private InputAction clickAction;

    void Start()
    {
        mainCamera = Camera.main;
        galleryManager = FindObjectOfType<GalleryManager>();

        // 1. 获取Input Action Asset
        // 方法A：如果你的PlayerInput组件挂在同一个场景物体上
        PlayerInput playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput != null && playerInput.actions != null)
        {
            // 2. 从Asset中找到名为“Click”的动作 (位于“UI” Action Map中)
            clickAction = playerInput.actions.FindAction("UI/Click"); // 注意格式：ActionMap/Action名

            // 3. 订阅（监听）这个动作
            if (clickAction != null)
            {
                // performed 表示动作“被执行时”（即按下时）
                clickAction.performed += OnClickActionPerformed;
                // 启用这个动作
                clickAction.Enable();
                Debug.Log($"壁画 {muralID} 已成功订阅点击输入。");
            }
            else
            {
                Debug.LogError($"在输入配置中未找到 ‘UI/Click‘ 动作。请检查你的Input Action Asset。");
            }
        }
        else
        {
            Debug.LogError($"未在场景中找到带有Input Action Asset的PlayerInput组件。");
        }
    }

    // 4. 定义当点击动作触发时要执行的方法
    private void OnClickActionPerformed(InputAction.CallbackContext context)
    {
        // 立即启动一个协程来延迟处理
        StartCoroutine(DelayedClickCheck());
    }

    private IEnumerator DelayedClickCheck()
    {
        // 等待一帧，让UI事件系统先更新
        yield return null;

        // 现在再进行射线检测，IsPointerOverGameObject()的结果就是准确的
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            yield break; // 如果点在UI上，则直接退出
        }

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

    // 5. 非常重要：在脚本失效时，取消订阅，防止内存泄漏
    private void OnDestroy()
    {
        if (clickAction != null)
        {
            clickAction.performed -= OnClickActionPerformed;
            clickAction.Disable();
        }
    }

    // 如果壁画物体可能被动态禁用/启用，也建议在OnDisable/OnEnable中处理
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