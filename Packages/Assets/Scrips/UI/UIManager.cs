using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using a_Scripts.Tools;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    [Header("事件监听")]
    public PlayerHealthEvent healthEvent;
    public PlayerController playerController;

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;


        // 新增：监听能量变化事件
        if (playerController != null)
            playerController.OnPowerChange.AddListener(OnPowerChange);
    }

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;

        // 新增：移除能量监听（避免内存泄漏）
        if (playerController != null)
            playerController.OnPowerChange.RemoveListener(OnPowerChange);
    }

    private void OnHealthEvent(PlayerHealth playerHealth)
    {
        var persentage = (float)playerHealth.currentHealth / playerHealth.maxHealth;//var自动推断数据类型
        playerStatBar.OnHealthChange(persentage);
    }

    // 新增：能量条更新方法（必须和监听的事件参数匹配！）
    private void OnPowerChange(PlayerController playerController)
    {
        // 计算能量百分比（当前能量 / 最大能量）
        float powerPercentage = playerController.currentPower / playerController.maxPower;
        playerStatBar.OnPowerChange(powerPercentage); // 调用UI更新能量条
    }
}
