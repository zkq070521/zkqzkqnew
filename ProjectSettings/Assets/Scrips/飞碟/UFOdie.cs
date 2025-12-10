using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOdie : MonoBehaviour
{

    public GameObject player;
    public PlayerHealth playerHealth;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.transform.position = playerHealth.startPosition ;

        }
    }

}
