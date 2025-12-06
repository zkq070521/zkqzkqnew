using UnityEngine;

public class SimpleDoorTrigger : MonoBehaviour
{
    [Header("2D碰撞体设置")] // 统一用2D碰撞体（适配你的跑酷2D场景）
    public Collider2D triggerCollider; // 指定触发器碰撞体（2D）
    public string playerTag = "Player"; // 玩家Tag（可自定义）

    private bool isPlayerInside = false; // 玩家是否在触发区
    private bool isUIOpen = false; // 避免重复打开UI

    void Start()
    {
        isPlayerInside = false;
        isUIOpen = false;

        // 自动获取2D碰撞体（适配2D场景，原代码用了3D的Collider）
        if (triggerCollider == null)
        {
            triggerCollider = GetComponent<Collider2D>();
        }

        // 校验：确保碰撞体是触发器
        if (triggerCollider != null && !triggerCollider.isTrigger)
        {
            Debug.LogWarning("触发碰撞体未勾选Is Trigger，已自动勾选！");
            triggerCollider.isTrigger = true; // 强制设为触发器
        }
    }

    // 移除Update里的重复调用（原代码每帧调用ShowPasswordUI，会重复初始化UI）
    // void Update() {} 

    // 2D触发器进入（必须带Collider2D参数，原代码无参是3D的OnTriggerExit）
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 判空+判断玩家Tag
        if (other.CompareTag(playerTag) && !isPlayerInside)
        {
            isPlayerInside = true;
            OpenPasswordUI(); // 只调用一次打开UI
        }
    }

    // 2D触发器离开（必须带Collider2D参数！原代码缺少参数，完全不执行）
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && isPlayerInside)
        {
            isPlayerInside = false;
            ClosePasswordUI(); // 关闭UI
        }
    }

    // 封装打开UI逻辑（加判空+防重复）
    private void OpenPasswordUI()
    {
        if (isUIOpen || DigitalPasswordManager.Instance == null) return;

        DigitalPasswordManager.Instance.ShowPasswordUI();
        isUIOpen = true;
        Debug.Log("玩家进入触发区，打开密码UI");
    }

    // 封装关闭UI逻辑（加判空）
    private void ClosePasswordUI()
    {
        if (!isUIOpen || DigitalPasswordManager.Instance == null) return;

        DigitalPasswordManager.Instance.HidePasswordUI();
        isUIOpen = false;
        Debug.Log("玩家离开触发区，关闭密码UI");
    }

    // 可选：Gizmos绘制触发区（方便调试）
    private void OnDrawGizmos()
    {
        if (triggerCollider != null)
        {
            Gizmos.color = new Color(0, 1, 0, 0.3f); // 半透明绿色
            Gizmos.DrawCube(triggerCollider.bounds.center, triggerCollider.bounds.size);
        }
    }
}