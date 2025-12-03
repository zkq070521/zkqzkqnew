using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relife : MonoBehaviour
{
    public GameObject player;
    public PlayerHealth health;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj == player)
        {
            health.startPosition = (Vector2)player.transform.position;
        }
    }


}
