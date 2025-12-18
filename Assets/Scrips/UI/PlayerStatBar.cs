using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image healthImage;
    public Image powerImage;
    public Image gainImage;

/// <summary>
/// 接收Health的变更百分比
/// </summary>
/// <param name="persentage">百分比：current/max</param>
    public void OnHealthChange(float persentage)
    {
        healthImage.fillAmount = persentage;
    }

    /// <summary>
    /// 接收PlayerController的变更百分比
    /// </summary>
    /// <param name="persentage">百分比：current/max</param>
    public void OnPowerChange(float persentage)
    {
        powerImage.fillAmount = persentage;
    }
}
