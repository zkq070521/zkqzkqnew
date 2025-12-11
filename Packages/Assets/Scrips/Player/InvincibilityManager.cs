using UnityEngine;
using UnityEngine.UI;

public class InvincibilityManager : MonoBehaviour
{
    [Header("核心配置")]
    public string dunTag = "dun"; 
    public float invincibilityDuration; // 无敌持续时间（10秒）
    public PlayerHealth playerHealth; 

    [Header("UI计时条配置")]
    public Image invincibilityBar;

    private bool isInvincible = false; // 是否处于无敌状态
    private float remainingInvincibilityTime; // 剩余无敌时间

    private void Start()
    {
        isInvincible = false;
    }

    private void Update()
    {
        // 无敌状态下，更新计时条和剩余时间
        if (isInvincible)
        {
            remainingInvincibilityTime -= Time.deltaTime;
            UpdateInvincibilityBar(); // 更新计时条填充比例

            // 无敌时间结束，解除无敌
            if (remainingInvincibilityTime <= 0)
            {
                DisableInvincibility();
            }
        }
    }

    // 碰撞到dun时触发无敌
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 只对Tag为"dun"的物体生效，且当前未无敌
        if (other.CompareTag(dunTag) && !isInvincible)
        {
            
            EnableInvincibility();
        }
    }

    // 启用无敌状态
    private void EnableInvincibility()
    {
        isInvincible = true;
        remainingInvincibilityTime = invincibilityDuration;

        // 显示计时条并设为满格
        if (invincibilityBar != null)
        {
            
            invincibilityBar.fillAmount = 1f; // 初始满格
        }

        Debug.Log("获得10秒无敌！");
    }

    // 解除无敌状态
    private void DisableInvincibility()
    {
        isInvincible = false;

        // 隐藏计时条（或设为0，根据需求选择）
        if (invincibilityBar != null)
        {
            
            invincibilityBar.fillAmount = 1f; // 归零
        }

        Debug.Log("无敌状态结束！");
    }

   
    private void UpdateInvincibilityBar()
    {
        if (invincibilityBar == null) return;

        // 计算填充比例：剩余时间 / 总无敌时间
        float fillRatio = remainingInvincibilityTime / invincibilityDuration;
        invincibilityBar.fillAmount = Mathf.Clamp01(fillRatio); // 限制在0~1之间
    }

    // 供生命值脚本调用
    public bool IsInvincible()
    {
        return isInvincible;
    }
}