using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDownUp : MonoBehaviour
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

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (!other.CompareTag("Player")) return;

        // bounds.center是碰撞体的实际中心
        float colliderCenterY = GetComponent<Collider2D>().bounds.center.y;

      
        float playerEnterY = other.transform.position.y;

        
        if (playerEnterY > colliderCenterY)
        {
           
            Debug.Log("Player 从上方进入碰撞体！");
            
        }
        else
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