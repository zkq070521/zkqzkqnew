using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Orange : MonoBehaviour
{
    public GameObject player;
    public int amount = 100;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();


            // 直接增加生命值
            playerHealth.currentHealth += amount;

            // 销毁苹果
            gameObject.SetActive(false);


        }
    }
}
