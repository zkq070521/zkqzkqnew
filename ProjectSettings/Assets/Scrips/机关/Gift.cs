using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    public string text;
    public GameObject bubble;
    public GameObject gift;
    private bool isTipShowing = false;

    void Start()
    {

        isTipShowing = false;
        // 错误1：顺序反了！先Find找到物体，再操作SetActive（否则bubble是null，会报错）
        bubble = GameObject.FindGameObjectWithTag("bubble"); // 先找气泡
        gift = GameObject.FindGameObjectWithTag("fruit");   // 先找礼物

        // 修复1：找完物体后，再隐藏气泡（加空值判断，避免null报错）
        if (bubble != null)
            bubble.SetActive(false);

        // 修复2：调用TipUI前先判断Instance是否存在（跨场景防止null）
        if (TipUI.Instance != null)
            TipUI.Instance.HideTip();
        else
            Debug.LogError("Gift脚本：TipUI.Instance为空！检查是否有跨场景的TipUI");
    }

    private void OnTriggerStay2D(Collider2D obj)
    {

        if (obj.gameObject == gift && TipUI.Instance != null && bubble != null)
        {
            if (!isTipShowing)
            {
                bubble.SetActive(true);
                TipUI.Instance.ShowTip(text); // 原有逻辑保留

                isTipShowing = true; // 标记为已显示
                StartCoroutine(HideTipAfterDelay(5f)); // 启动3秒延迟隐藏协程
            }
        }

    }
    private IEnumerator HideTipAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 等待3秒

        // 隐藏对话框（还原初始状态）
        //if (bubble != null) bubble.SetActive(false);
        if (TipUI.Instance != null) TipUI.Instance.HideTip(); // 假设TipUI有HideTip方法，无则用下面注释的SetActive

        //isTipShowing = false; // 重置标志位，允许再次触发


    }

}

   