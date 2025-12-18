using UnityEngine;
using UnityEngine.InputSystem;

public class RaySkill : MonoBehaviour
{
    public GameObject fish;
    public LayerMask targetLayer;
    [Header("射线配置")]
    public Transform handTransform; 
    public LineRenderer lineRenderer;
    public float rayMaxLength; 
    public float rotateSpeed; 
    public float rotateRange1;
    public float rotateRange2;
    public float rayWidth;
    public Vector3 rayEnd;
    [Header("发射后消失配置")]
    public float fadeSpeed; // 射线消失速度
    private float currentRotateAngle = 0f; 
    private int rotateDirection = 1; // 旋转方向（1=顺时针，-1=逆时针）
    private bool isHoldingTab = false;
    private bool isFading = false;

    public PlayerInputControl inputControl;
    
    private void Awake()
    {
      
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2; 
        lineRenderer.startWidth = rayWidth;
        lineRenderer.endWidth = rayWidth;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color")); 
        lineRenderer.material.color = Color.red;

       
        inputControl = new PlayerInputControl();
    }

    private void OnEnable()
    {
       
        inputControl.Player.Enable();
        inputControl.Player.TabSkill.performed += OnTabPressed;
        inputControl.Player.TabSkill.canceled += OnTabReleased;
    }

    private void OnDisable()
    {
        
        inputControl.Player.TabSkill.performed -= OnTabPressed;
        inputControl.Player.TabSkill.canceled -= OnTabReleased;
        inputControl.Player.Disable();
    }

    private void Update()
    {
        fish = GameObject.FindGameObjectWithTag("fish");
        if (isHoldingTab && !isFading)
        {
            UpdateRayRotation(); 
            UpdateRayPosition();
        }

        
        if (isFading)
        {
            FadeRay();
        }
    }

    
    private void OnTabPressed(InputAction.CallbackContext context)
    {
       
        isHoldingTab = true;
        isFading = false;
        lineRenderer.enabled = true;
        lineRenderer.useWorldSpace = true;
        //lineRenderer.sortingLayerName = "UI"; // 最上层渲染
        lineRenderer.sortingOrder = 999;
        currentRotateAngle = 0f; // 重置旋转角度
        rotateDirection = 1;
    }

    
    private void OnTabReleased(InputAction.CallbackContext context)
    {
        
        isHoldingTab = false;
        isFading = true;
        FireRay(); 
    }

   
    private void UpdateRayRotation()
    {
        currentRotateAngle += rotateDirection * rotateSpeed * Time.deltaTime;
       
        if (currentRotateAngle >= rotateRange1)
        {
            currentRotateAngle = rotateRange1;
            rotateDirection = -1; 
        }
        else if (currentRotateAngle <= rotateRange2)
        {
            currentRotateAngle = rotateRange2;
            rotateDirection = 1; 
        }
    }

  
    private void UpdateRayPosition()
    {
        if (handTransform == null)
        {
            Debug.LogError("handTransform未赋值！请拖入Player手部的子物体（位置固定）");
            return;
        }

      
        Camera mainCam = Camera.main;
        float safeZ = mainCam.orthographic ? (mainCam.transform.position.z + 1f) : 0f;

       
        Vector3 rayStart = handTransform.position;
        rayStart.z = safeZ;
        lineRenderer.SetPosition(0, rayStart);

    
  
        Vector2 rayDir = Quaternion.Euler(0, 0, currentRotateAngle) * Vector2.right;
        
        rayEnd = rayStart + new Vector3(rayDir.x, rayDir.y, 0) * rayMaxLength;
        rayEnd.z = safeZ;

    
        lineRenderer.SetPosition(1, rayEnd);

       
        Debug.Log($"旋转角度：{currentRotateAngle:F1}° | 射线长度：{Vector3.Distance(rayStart, rayEnd):F1}");
    }

 
    private void FireRay()
    {
        if (handTransform == null) return;
        isHoldingTab = false;
        isFading = true;

       
        Vector2 rayDir2D = Quaternion.Euler(0, 0, currentRotateAngle) * Vector2.right;
        if (rayDir2D.magnitude < 0.01f) rayDir2D = Vector2.right;

        
        Vector2 rayOrigin2D = handTransform.position;
        Vector2 directionToPlayer = (rayEnd - handTransform.position).normalized;//必须归一化，方向向量的模长是1
        float distanceToPlayer = Vector3.Distance(handTransform.position,rayEnd);

        // 发射射线 (起点：激光点，方向：指向主角，距离：到主角的距离)
        RaycastHit2D hit = Physics2D.Raycast(handTransform.position, directionToPlayer, distanceToPlayer, targetLayer);

        Debug.DrawRay(rayOrigin2D, rayDir2D * rayMaxLength, Color.green, 2f);
        if (hit && hit.collider.CompareTag("fish"))
        {
            Debug.Log("命中fish");
            hit.collider.GetComponent<FishMove>()?.OnHitByRay();
        }
        else
        {
            Debug.Log("未命中");
        }
    }

    
    private void FadeRay()
    {
        if (handTransform == null) return;

        Vector3 rayStart = lineRenderer.GetPosition(0);
        Vector3 rayEnd = lineRenderer.GetPosition(1);
        Vector3 dir = (rayStart - rayEnd).normalized;
        float currentLength = Vector3.Distance(rayStart, rayEnd);
        float newLength = currentLength - fadeSpeed * Time.deltaTime;

        // 长度<=0时隐藏射线
        if (newLength <= 0)
        {
            lineRenderer.enabled = false;
            isFading = false;
            return;
        }

        // 缩短射线终点
        lineRenderer.SetPosition(1, rayStart + dir * newLength);
    }
}