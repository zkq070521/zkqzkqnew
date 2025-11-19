using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("核心设置")]
    public Transform target; 
    public Vector2 playerOffset; // Player在屏幕的偏移
    [Range(0, 1)] public float smoothSpeed = 0.125f; // 相机跟随平滑度

    [Header("视差背景")]
    public Transform farBackground;
    public Transform middleBackground;
    public Transform nearBackground;
    public Transform treeBackground;
    public Transform middleBackground1;
    public Transform nearBackground1;
    public Transform treeBackground1;
    public Transform middleBackground2;
    public Transform nearBackground2;
    public Transform treeBackground2;
    [Header("视差强度（越小越慢，0=不移动）")]
    public float farParallax = 0.1f;
    public float middleParallax = 0.3f;
    public float nearParallax = 0.6f;
    public float treeParallax = 0.8f;

    private Vector3 lastCameraPos; // 上一帧相机位置
    private float cameraHalfWidth; // 相机水平半宽

    private void Start()
    {
        
            target = GameObject.FindGameObjectWithTag("Player").transform;

        // 记录相机初始位置
        lastCameraPos = transform.position;

        // 计算相机水平半宽
        if (GetComponent<Camera>().orthographic)
            cameraHalfWidth = GetComponent<Camera>().orthographicSize * Screen.width / Screen.height;
    }

    private void LateUpdate()
    {
        // 1. 计算相机目标位置（让Player在偏左下方）
        Vector3 desiredPosition = new Vector3(
            target.position.x + playerOffset.x, // Player的X + 左偏移（负数=更靠左）
            target.position.y + playerOffset.y, // Player的Y + 下偏移（负数=更靠下）
            transform.position.z // 保持相机Z轴不变（2D游戏）
        );

        // 2. 平滑跟随
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // 3. 计算相机移动量（用于视差滚动）
        Vector3 cameraDelta = transform.position - lastCameraPos;

        
        if (farBackground != null)
            farBackground.position += new Vector3(cameraDelta.x * farParallax, cameraDelta.y * farParallax, 0);
        if (middleBackground != null)
            middleBackground.position += new Vector3(cameraDelta.x * middleParallax, cameraDelta.y * middleParallax, 0);
        if (nearBackground != null)
            nearBackground.position += new Vector3(cameraDelta.x * nearParallax, cameraDelta.y * nearParallax, 0);
        if (treeBackground != null)
            treeBackground.position += new Vector3(cameraDelta.x * treeParallax, cameraDelta.y * treeParallax, 0);
        if (middleBackground != null)
            middleBackground1.position += new Vector3(cameraDelta.x * middleParallax, cameraDelta.y * middleParallax, 0);
        if (nearBackground != null)
            nearBackground1.position += new Vector3(cameraDelta.x * nearParallax, cameraDelta.y * nearParallax, 0);
        if (treeBackground != null)
            treeBackground1.position += new Vector3(cameraDelta.x * treeParallax, cameraDelta.y * treeParallax, 0);
        if (middleBackground != null)
            middleBackground2.position += new Vector3(cameraDelta.x * middleParallax, cameraDelta.y * middleParallax, 0);
        if (nearBackground != null)
            nearBackground2.position += new Vector3(cameraDelta.x * nearParallax, cameraDelta.y * nearParallax, 0);
        if (treeBackground != null)
            treeBackground2.position += new Vector3(cameraDelta.x * treeParallax, cameraDelta.y * treeParallax, 0);

        // 5. 更新上一帧相机位置
        lastCameraPos = transform.position;
    }

    // 提供给Map脚本的接口：获取相机水平半宽（用于地图循环判断）
    public float GetCameraHalfWidth()
    {
        return cameraHalfWidth;
    }
}