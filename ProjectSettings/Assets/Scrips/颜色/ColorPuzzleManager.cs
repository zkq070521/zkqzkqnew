using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ColorPuzzleManager : MonoBehaviour
{
    [Header("核心配置")]
    public LayerMask targetLayer;
    public ColorType[] correctColorOrder; // 正确的颜色顺序
    //public bool sortByXAxis = true;
    private ColorObject firstSelected; // 第一个选中的物体的ColorObject脚本
    private ColorObject secondSelected; // 第二个选中的物体

    [Header("输入系统配置")]
    public InputActionAsset inputActionAsset; 
    private InputAction clickAction;

    public GameObject skull;
    public string text;


    private void Awake()
    {
        
        if (inputActionAsset == null)
        {
            
            return;
        }

        
        clickAction = inputActionAsset.FindActionMap("Mouse")?.FindAction("Click");
        if (clickAction == null)
        {
           
            return;
        }
        
    }

    private void Start()
    {
        TipUI.Instance?.HideTip();
    }
    private void OnEnable()
    {
        
        if (clickAction != null)
        {
            clickAction.Enable();
            clickAction.performed += OnMouseClickPerformed;
        }
    }

    private void OnDisable()
    {
       
        if (clickAction != null)
        {
            clickAction.performed -= OnMouseClickPerformed;
            clickAction.Disable();
        }
    }

   
    private void OnMouseClickPerformed(InputAction.CallbackContext context)//context是一个结构体
    {
        if (context.performed)
        {
            
            ColorObject clickedObject = GetClickedColorObject();
            if (clickedObject == null) return;

            
            if (firstSelected == null)
            {
                firstSelected = clickedObject;
                firstSelected.SetSelected(true); // 高亮
               
            }
            // 处理第二次点击（不能是同一个物体）
            else if (firstSelected != clickedObject)
            {
                secondSelected = clickedObject;
                secondSelected.SetSelected(true); // 高亮
                

                // 交换位置
                SwapTwoObjects();

                
                if (CheckCorrectOrder())
                {
                    Debug.Log("排序正确");
                    OnPuzzleSuccess(); 
                }

                
                ResetSelectedObjects();
            }
            // 点击同一个取消
            else
            {
                firstSelected.SetSelected(false);
                firstSelected = null;
                Debug.Log("取消选中：" + clickedObject.colorType);
            }
        }
    }

    
    private ColorObject GetClickedColorObject()//和c语言一样，ColorObject是函数返回的类型
    {
        
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        //RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, targetLayer);

        if (hit.collider != null)
        {
            return hit.collider.GetComponent<ColorObject>();
        }
        return null;
    }

    
    private void SwapTwoObjects()
    {
        if (firstSelected == null || secondSelected == null) return;
        firstSelected.SwapPosition(secondSelected.transform);
        Debug.Log("交换位置：");
    }


    private bool CheckCorrectOrder()
    {
        // 1. 校验配置：正确数组不能为空，且长度要和场景中ColorObject数量一致
        ColorObject[] allSceneObjects = FindObjectsOfType<ColorObject>();
        if (correctColorOrder == null || correctColorOrder.Length == 0)
        {
            Debug.LogError("请在Inspector配置正确的颜色顺序数组！");
            return false;
        }
        if (allSceneObjects.Length != correctColorOrder.Length)
        {
            Debug.LogError($"场景中有{allSceneObjects.Length}个颜色物体，但正确顺序数组长度是{correctColorOrder.Length}，数量不匹配！");
            return false;
        }

        
        System.Array.Sort(allSceneObjects, (a, b) =>
        {
           
            
                
                return a.transform.position.x.CompareTo(b.transform.position.x);
          
        });

       
        for (int i = 0; i < correctColorOrder.Length; i++)
        {
            ColorType sceneType = allSceneObjects[i].colorType;
            ColorType correctType = correctColorOrder[i];

            // 只要有一个位置不匹配，直接返回false
            if (sceneType != correctType)
            {
                Debug.Log($"第{i + 1}个位置颜色错误！实际：{sceneType}，正确：{correctType}");
                return false;
            }
        }

        
        Debug.Log("所有颜色顺序完全正确！");
        return true;
    }

    private ColorObject FindColorObjectByType(ColorType type)
    {
        ColorObject[] allObjects = FindObjectsOfType<ColorObject>();
        foreach (ColorObject obj in allObjects)
        {
            if (obj.colorType == type)
            {
                return obj;
            }
        }
        return null;
    }

    
    private void ResetSelectedObjects()
    {
        if (firstSelected != null)
        {
            firstSelected.SetSelected(false);
            firstSelected = null;
        }
        if (secondSelected != null)
        {
            secondSelected.SetSelected(false);
            secondSelected = null;
        }
    }


    private void OnPuzzleSuccess()
    {
        skull.SetActive(false);
        TipUI.Instance.ShowTip(text);
     
        StartCoroutine(HideTipAfter3Seconds());
    }

    
    private IEnumerator HideTipAfter3Seconds()
    {
        yield return new WaitForSeconds(3f); // 计时3秒
        if (TipUI.Instance != null) // 判空避免报错
        {
            TipUI.Instance.HideTip();
        }
    }


}