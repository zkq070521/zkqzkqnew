using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class YuanShen : MonoBehaviour
{
    public GameObject player;
    public GameObject yuanshen;

    void Update()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
       // yuanshen = GameObject.FindGameObjectWithTag("yuanshen");
        

        if (player == null)
            Debug.LogError("没找到Player");

        // 假设Player挂载了PlayerMovement脚本

            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            if (playerHealth != null)
            {
                player = playerHealth.gameObject;
            }
            else
            {
                Debug.LogError("没找到挂载PlayerMovement的对象！");
            }
        }
    


    private void OnTriggerStay2D(Collider2D obj)
    {


        if (obj.gameObject == player)
        {

            yuanshen.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {

        yuanshen.SetActive(false);
        this.gameObject.SetActive(false);
        
    }
}
