using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOattack : MonoBehaviour
{
    public int damage;
    public PlayerHealth playerHealth;

    public GameObject player;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damage);

        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        playerHealth.currentHealth = playerHealth.maxHealth;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }
}
