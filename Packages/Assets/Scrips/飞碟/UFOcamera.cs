using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOcamera : MonoBehaviour
{
    public Vector2 playerOffset2; // Player在屏幕的偏移
    
    public float cameraYOffset;
    public Camera mainCamera;
    
    public CameraController simpleCamera;
    [Range(0, 1)] public float smoothSpeed = 0.125f; // 平滑跟随速度



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            
            simpleCamera.playerOffset = playerOffset2;
        }


    }


    void Awake()
    {
        mainCamera = Camera.main;
        simpleCamera = mainCamera.GetComponent<CameraController>();
        
        

        
    }


}
