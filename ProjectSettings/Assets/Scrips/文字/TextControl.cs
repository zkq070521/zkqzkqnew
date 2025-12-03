using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextControl : MonoBehaviour
{
    public GameObject player;
    public string text;

    void Start()
    {
        TipUI.Instance.HideTip();
        player = GameObject.FindGameObjectWithTag("Player");
        // 新增：检查是否找到Player，避免后续报错
        if (player == null)
            Debug.LogError("没找到Player");
    }

    private void OnTriggerStay2D(Collider2D obj)
    {
        
       
        if (obj.gameObject == player)
        {
            
            if (TipUI.Instance != null)
                TipUI.Instance.ShowTip(text);
            else
                Debug.LogError("TipUI.Instance为空");
        }
    }

    
    private void OnTriggerExit2D(Collider2D other)
    {
       
        if (other.gameObject == player && TipUI.Instance != null)
        {
            TipUI.Instance.HideTip();
        }
    }
}