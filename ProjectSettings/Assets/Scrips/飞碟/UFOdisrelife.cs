using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOdisrelife : MonoBehaviour
{
   

    public PlayerHealth playerHealth;
    //public float cameraYOffset;
    public Camera mainCamera;
    public GameObject player;
    public UFOrelife relife;
    public CameraController simpleCamera;
    //[Range(0, 1)] public float smoothSpeed = 0.125f; // Æ½»¬¸úËæËÙ¶È



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            simpleCamera.enabled = true;
            playerHealth.startPosition = player.transform.position;
            relife.enabled = false;
        }


    }


    void Start()
    {
        simpleCamera = mainCamera.GetComponent<CameraController>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

   
}


