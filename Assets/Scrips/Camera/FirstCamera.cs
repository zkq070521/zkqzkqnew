using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCamera : MonoBehaviour
{

    public Transform target;
    public Vector2 playerOffset; // Player在屏幕的偏移
    [Range(0, 1)] public float smoothSpeed = 0.125f; // 相机跟随平滑度

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(
            target.position.x + playerOffset.x, // Player的X + 左偏移（负数=更靠左）
            target.position.y + playerOffset.y, // Player的Y + 下偏移（负数=更靠下）
            transform.position.z // 保持相机Z轴不变（2D游戏）
        );
        // 2. 平滑跟随
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
