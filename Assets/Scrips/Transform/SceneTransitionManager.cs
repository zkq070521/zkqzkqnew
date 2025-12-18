using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class SceneTransitionManager : MonoBehaviour
{
    
    [Header("过渡设置")]
    public Animator transitionAnimator; 
  
    [Header("场景设置")]
    public string targetSceneName; // 目标场景名称（在Inspector中输入）
    public string triDomain;
    [Header("需要保留的物体")]
    public GameObject player; // Player物体引用
    public GameObject monkey; // Monkey物体引用

    //private bool isTransitioning = false; // 是否正在过渡中
    private Mouse mouse;
    public bool isDomain;

    void Start()
    {
        // 获取鼠标引用
        mouse = Mouse.current;

    }




     void Update()
    {
        if (mouse != null && mouse.leftButton.wasPressedThisFrame)
        {
            // 射线检测
            Vector2 mousePosition = mouse.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                //切换场景
                //isTransitioning = true;
                /*isDomain = true;
                transitionAnimator.SetBool("isDomain",isDomain);
                isDomain = false;*/
                transitionAnimator.SetTrigger(triDomain);
                DontDestroyOnLoad(player);
                DontDestroyOnLoad(monkey);
                SceneManager.LoadScene(targetSceneName);
                player.transform.position = new Vector3(-6, -1, 0);
                monkey.transform.position = new Vector3(5, -1, 0);


            }
        }
    }

    // 鼠标点击事件（通过Input System绑定）
   /* public void OnMouseClick(InputAction.CallbackContext context)
    {
        if (context.performed && !isTransitioning)
        {
            StartTransition();
        }
    }*/

    // 开始过渡
    public void StartTransition()
    {
        /*if (string.IsNullOrEmpty(targetSceneName))
        {
            Debug.LogError("请在Inspector中设置目标场景名称！");
            return;
        }*/

        //isTransitioning = true;
        //transitionAnimator.SetTrigger(transitionTrigger); // 播放过渡动画
    }

    // 过渡动画结束后调用（在动画剪辑的最后一帧添加此事件）
    public void OnTransitionComplete()
    {
        // 保留Player和Monkey
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(monkey);

        // 切换场景
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(targetSceneName);
    }

    // 场景加载完成后调用
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // 可以在这里设置Player和Monkey在新场景中的初始位置
        // 例如：player.transform.position = new Vector3(0, 0, 0);

        //isTransitioning = false;
        Debug.Log($"成功切换到场景：{scene.name}");
    }
}