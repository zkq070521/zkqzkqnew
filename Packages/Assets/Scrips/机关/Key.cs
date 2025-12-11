using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
  
    public string text;
    public GameObject door;
    public GameObject player;
    private bool isTipShowing = false;

    void Start()
    {

        isTipShowing = false;
        
        door = GameObject.FindGameObjectWithTag("Door"); 
        player = GameObject.FindGameObjectWithTag("Player");   

        
        if (door != null)
            door.SetActive(true);

        
        if (TipUI.Instance != null)
            TipUI.Instance.HideTip();
        else
            Debug.LogError("Gift脚本：TipUI.Instance为空！检查是否有跨场景的TipUI");
    }

    private void OnTriggerStay2D(Collider2D obj)
    {

        if (obj.gameObject == player && TipUI.Instance != null && door != null)
        {
            if (!isTipShowing)
            {
                door.SetActive(false);
                TipUI.Instance.ShowTip(text); // 原有逻辑保留

                isTipShowing = true; // 标记为已显示
                StartCoroutine(HideTipAfterDelay(1f)); // 启动3秒延迟隐藏协程
            }
        }

    }
    private IEnumerator HideTipAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 等待3秒

        // 隐藏对话框（还原初始状态）
        //if (bubble != null) bubble.SetActive(false);
        if (TipUI.Instance != null) TipUI.Instance.HideTip(); 
        this.gameObject.SetActive(false);
        // 假设TipUI有HideTip方法，无则用下面注释的SetActive

        //isTipShowing = false; // 重置标志位，允许再次触发


    }

}

   

