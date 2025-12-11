using UnityEngine;

public class ColorObject : MonoBehaviour
{
    [Header("颜色")]
    public ColorType colorType; 
    [Header("反馈")]
    public SpriteRenderer spriteRenderer; 
    public Color selectColor = Color.white; // 选中时的高亮颜色
    public Color normalColor; // 正常颜色

    private void Awake()
    {
        
        if (spriteRenderer != null)
        {
            normalColor = spriteRenderer.color;
        }
    }

    
    public void SetSelected(bool isSelected)
    {
        
        spriteRenderer.color = isSelected ? selectColor : normalColor;
    }

    // 交换位置
    public void SwapPosition(Transform targetTransform)
    {
        Vector2 tempPos = transform.position;
        transform.position = targetTransform.position;
        targetTransform.position = tempPos;
    }


}