using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    [Header("事件监听")]
    public PlayerHealthEvent healthEvent;

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
    }

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
    }

    private void OnHealthEvent(PlayerHealth playerHealth)
    {
        var persentage = playerHealth.currentHealth / playerHealth.maxHealth;//var自动推断数据类型
        playerStatBar.OnHealthChange(persentage);
    }
}
