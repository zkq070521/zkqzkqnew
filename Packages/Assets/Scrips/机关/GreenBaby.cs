using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBaby : MonoBehaviour
{
   
    public GameObject player;
    


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();


            // 直接增加生命值
            playerHealth.currentHealth = 1000;

            // 销毁苹果
            gameObject.SetActive(false);


        }
    }
}


