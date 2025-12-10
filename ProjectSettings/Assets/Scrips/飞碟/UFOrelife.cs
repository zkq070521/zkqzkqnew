using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOrelife : MonoBehaviour
{
    public Vector2 playerOffset2; // Player在屏幕的偏移
    public PlayerHealth playerHealth;
    public float cameraYOffset;
    public Camera mainCamera;
    public GameObject player;
    public CameraController simpleCamera;
    [Range(0, 1)] public float smoothSpeed = 0.125f; // 平滑跟随速度



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
           
            playerHealth.startPosition = player.transform.position;
            simpleCamera.playerOffset = playerOffset2;
        }

       
    }

   
    void Awake()
    {
        mainCamera = Camera.main;
        simpleCamera = mainCamera.GetComponent<CameraController>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

        if(player == null)
        {
            Debug.Log("没有找到player");
        }
    }

   
}
