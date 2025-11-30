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
    public PlayerControllerEvent playerController;

    

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;


        // 监听能量变化事件

        playerController.OnEventRaised += (OnPowerChange);
    }

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;

        // 移除能量监听
       
            playerController.OnEventRaised += (OnPowerChange);
    }

    private void OnHealthEvent(PlayerHealth playerHealth)
    {
        var persentage = (float)playerHealth.currentHealth / playerHealth.maxHealth;//var自动推断数据类型
        playerStatBar.OnHealthChange(persentage);
    }

    // 能量条更新方法
    private void OnPowerChange(PlayerController playerController)
    {
        // 计算能量百分比（当前能量 / 最大能量）
        float powerPercentage = playerController.currentPower / playerController.maxPower;
        playerStatBar.OnPowerChange(powerPercentage); // 调用UI更新能量条
    }
}
