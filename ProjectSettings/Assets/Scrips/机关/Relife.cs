using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Relife : MonoBehaviour
{

    public int xxx;
    public int yyy;
    public bool isrb;

    public GameObject player;
    public PlayerHealth health;
    public Camera mainCamera;
    public float cameraYOffset;
    public Rigidbody2D rb;
    public SimpleCamera simpleCamera; 
    [Range(0, 1)] public float smoothSpeed = 0.125f; // 平滑跟随速度
    

    private void Start()
    {
        isrb = false;

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            health = player.GetComponent<PlayerHealth>();
        }
        else
        {
            Debug.LogError("Relife脚本：未找到Tag为Player的物体！");
        }

       rb = player.GetComponent <Rigidbody2D>();

        
        simpleCamera = mainCamera.GetComponent<SimpleCamera>();
        
        
            
        
      
    }


    private void FixedUpdate()
    {
        if (isrb)
        {

            Vector2 currentVelocity = rb.velocity;
            currentVelocity.y = -yyy; // 正数=向上跳，负数=向下落
            rb.velocity = currentVelocity;
        }
    }



    private void Update()
    {
        if (simpleCamera.enabled == false)
        {
            UpdateCameraPosition();
        }
    }

    // 触发复活点时：更新复活点 + 立刻同步摄像机位置
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject == player && health != null && mainCamera != null)
        {
            simpleCamera.enabled = false;

            isrb = true;


            health.startPosition = (Vector2)player.transform.position;
            Debug.Log("复活点更新：" + health.startPosition);
            // 触发时强制同步一次摄像机位置（平滑跟随会自动衔接）
            UpdateCameraPosition();
        }
    }

   
    private void UpdateCameraPosition()
    {

        
       
        Vector3 desiredPosition = new Vector3(
            player.transform.position.x, 
            player.transform.position.y + cameraYOffset, 
            mainCamera.transform.position.z 
        );

        
        Vector3 smoothedPosition = Vector3.Lerp(
            mainCamera.transform.position, // 摄像机当前位置
            desiredPosition, // 目标位置
            smoothSpeed // 平滑速度
        );

        
        mainCamera.transform.position = smoothedPosition;
    }

   
   /* public void SyncCameraOnRespawn()
    {
        if (player != null && mainCamera != null)
        {
            UpdateCameraPosition();
        }
    }*/
}