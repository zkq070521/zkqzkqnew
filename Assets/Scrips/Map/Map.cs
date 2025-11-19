using UnityEngine;

public class Map : MonoBehaviour
{
    [Header("无限地图设置")]
    public Transform player; // 拖入Player
    public CameraController cameraController; // 拖入挂载CameraController的相机
    public int mapNum = 3; // 地图重复次数（建议3块：中间1块+左右各1块，避免空白）

    private float mapWidth; // 单块地图的实际宽度（基于Sprite）
    private float totalWidth; // 总地图宽度（单块宽度 × 重复次数）
    private float cameraHalfWidth; // 相机水平半宽（用于判断地图边界）

    private void Start()
    {
        // 自动获取Player和相机（如果没手动拖入）
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        if (cameraController == null)
            cameraController = FindObjectOfType<CameraController>();

        // 获取单块地图宽度（SpriteRenderer的实际 bounds 宽度）
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            mapWidth = sr.sprite.bounds.size.x;
        else
        {
            Debug.LogError("Map脚本挂载的物体没有SpriteRenderer！");
            enabled = false;
            return;
        }

        totalWidth = mapWidth * mapNum; // 总宽度 = 单块宽度 × 重复次数
        cameraHalfWidth = cameraController.GetCameraHalfWidth(); // 获取相机半宽
    }

    private void Update()
    {
        // 核心逻辑：当Player超出当前地图块的「右边界」时，地图向右平移
        if (player.position.x > transform.position.x + mapWidth * 1/2 +mapWidth - cameraController.playerOffset.x)
        {
            transform.position += new Vector3(mapWidth * 3, 0, 0);
        }
        // 当Player超出当前地图块的「左边界」时，地图向左平移
        else if (player.position.x < transform.position.x - mapWidth * 2 - cameraController.playerOffset.x)
        {
            transform.position -= new Vector3(mapWidth * 3, 0, 0);
        }
    }
}