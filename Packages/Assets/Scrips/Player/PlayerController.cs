using RPG.Timer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("跳跃设置")]
    public float jumpForce; // 基础跳跃力（按一下的力度）
    public float maxJumpForce ;  // 最大跳跃力（长按到顶的力度）
    public float jumpHoldTimeMax ; // 达到最大跳跃力所需的长按时间（秒）
    public float jumpHoldTime; // 记录按键按下的累计时间
    private bool isJumping;     // 标记是否正在跳跃中（避免空中重复累积时间


    [Header("走路设置")]
    public float speed;
    public PlayerInputControl inputControl;
    public Rigidbody2D rb;
    public PhysicsCheck physicsCheck;
    public Vector2 inputDirection;
    

    [Header("冲刺能量设置")]
    public float maxPower = 120f; // 最大能量（能量条满值）
    public float currentPower;    // 当前能量
    public float dashCost = 40f;  // 冲刺一次消耗的能量
    public float dashSpeed ; // 冲刺速度（比基础speed快）
    public float dashDuration ; // 冲刺持续时间（秒）
    public float powerRecoveryRate = 10f; // 每秒恢复的能量
    private bool isDashing; // 是否正在冲刺（避免重复冲刺）
    //private float originalSpeed; // 保存玩家基础速度（冲刺后恢复用）

    // 广播给UI，传当前脚本实例让UI获取能量值）
    public UnityEvent<PlayerController> OnPowerChange;

    [Header("切换手枪")]
    public GameObject[] guns;
    private int gunNum;


    //计时器
    private TimerManager _timerManager; // 计时器管理器实例
    private string jumpHoldTimerKey = "Player_Jump_Hold"; // 跳跃计时器唯一标识

    private void Awake()
    {
        
        inputControl = new PlayerInputControl();
        // 绑定跳跃
        inputControl.Gameplay.Jump.started += OnJumpStarted; // 按键按下瞬间
        inputControl.Gameplay.Jump.canceled += OnJumpCanceled; // 按键松开瞬间

        // 绑定冲刺
        inputControl.Gameplay.Dash.started += OnDashStarted;

        
        physicsCheck = GetComponent<PhysicsCheck>();

        //  初始化TimerManager
        _timerManager = new TimerManager();

       
    
}


    private void Start()
    {
        isDashing = false;
         guns[0].SetActive(true);
        Vector3 startPosition = GameObject.FindGameObjectWithTag("Player").transform.position;


        // 初始化能量（满能量），并广播给UI
        currentPower = maxPower;
        OnPowerChange?.Invoke(this); // UI初始显示满能量

        // 保存玩家基础移动速度（冲刺后恢复用）
        //originalSpeed = speed;
    }




    private void OnEnable()
    {
        inputControl.Enable(); // 启用输入
    }

    private void OnDisable()
    {
        inputControl.Disable(); // 禁用输入
        // 清理计时器（避免脚本禁用后残留）
        _timerManager.Remove(jumpHoldTimerKey);
    }

    private void Update()
    {

        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();


        // 如果长按超过最大时间没松开，自动用最大力跳跃
        if (isJumping && _timerManager.IsFinished(jumpHoldTimerKey))
        {
            ExecuteJump(maxJumpForce); // 用最大力跳跃
            CleanupJumpTimer(); // 清理计时器和标记
        }

        SwitchGun();

        // 非冲刺状态下，自动恢复能量
        if (!isDashing && currentPower < maxPower)
        {
            currentPower += powerRecoveryRate * Time.deltaTime;
            currentPower = Mathf.Min(currentPower, maxPower); // 不超过最大能量
            OnPowerChange?.Invoke(this); // 广播能量变化，UI实时更新
        }

    }

    /// <summary>
    /// 跳跃键按下瞬间：启动计时器（只有在地面才有效）
    /// </summary>
    private void OnJumpStarted(InputAction.CallbackContext obj)
    {
        if (physicsCheck.isGround)
        {
            isJumping = true;
            // 启动时间计时器：key=唯一标识，duration=最长长按时间
            // TimerManager.Start方法会自动处理：存在则重启，不存在则创建
            _timerManager.Start(jumpHoldTimerKey, jumpHoldTimeMax);
        }
    }

    /// <summary>
    /// 跳跃键松开瞬间：计算跳跃力并执行跳跃
    /// </summary>
    private void OnJumpCanceled(InputAction.CallbackContext obj)
    {
        if (isJumping && _timerManager.Exists(jumpHoldTimerKey))
        {
            // 获取计时器实际累积的时间（TimerManager.GetElapsed方法）
            float actualHoldTime = _timerManager.GetElapsed(jumpHoldTimerKey);

            // 计算最终跳跃力（线性插值：基础力 → 最大力）
            float finalJumpForce = Mathf.Lerp(jumpForce, maxJumpForce, actualHoldTime / jumpHoldTimeMax);

            // 执行跳跃
            ExecuteJump(finalJumpForce);

            // 清理计时器和标记
            CleanupJumpTimer();
        }
    }

    /// <summary>
    /// 执行跳跃的通用方法（避免代码重复）
    /// </summary>
    private void ExecuteJump(float force)
    {
        // 重置Y轴速度，避免空中叠加速度导致跳跃异常
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        // 施加跳跃力
        rb.AddForce(transform.up * force, ForceMode2D.Impulse);
    }

    /// <summary>
    /// 清理跳跃相关的计时器和标记
    /// </summary>
    private void CleanupJumpTimer()
    {
        _timerManager.Remove(jumpHoldTimerKey); // 移除计时器
        isJumping = false; // 重置跳跃标记
    }


    


    



    // ---------------------- 冲刺 ----------------------
    /// <summary>
    /// 冲刺按键（左Shift）按下瞬间
    /// </summary>
    private void OnDashStarted(InputAction.CallbackContext obj)
    {
        Debug.Log("冲刺按键被按下");
        // 冲刺条件：在地面 + 不在冲刺中 + 能量足够
        if (physicsCheck.isGround && !isDashing && currentPower >= dashCost)
        {
            Debug.Log("满足冲刺条件"); 
            StartCoroutine(DashCoroutine());
        }
    }

    /// <summary>
    /// 冲刺协程（控制冲刺速度和持续时间）
    /// </summary>
    private IEnumerator DashCoroutine()
    {
        Debug.Log("冲刺协程启动"); 
        isDashing = true;
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed * Time.deltaTime, rb.velocity.y);
       
        currentPower -= dashCost; 
        OnPowerChange?.Invoke(this); // 广播能量变化，UI同步减少

        // 播放冲刺动画
        // animator?.SetTrigger("Dash");

        // 冲刺计时器key（唯一标识，避免和跳跃计时器冲突）
        string dashTimerKey = "Player_Dash_Timer";
        // TimerManager
        _timerManager.Start(dashTimerKey, dashDuration);

        // 播放冲刺动画（如需启用，取消注释并赋值animator）
        // animator?.SetTrigger("Dash");

        // 循环直到计时器结束
        while (!_timerManager.IsFinished(dashTimerKey))
        {
            Debug.Log($"等待冲刺结束：已过{_timerManager.GetElapsed(dashTimerKey):F2}秒");
            yield return null; // 每帧等待，不阻塞其他逻辑
        }

        // 冲刺结束X轴速度归零
        rb.velocity = new Vector2(0f, rb.velocity.y);
        Debug.Log("冲刺结束速度归零");
        // 清理冲刺计时器
        _timerManager.Remove(dashTimerKey);
        isDashing = false;
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if (!isDashing)
        {
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
            //人物翻转
            int faceDir = (int)transform.localScale.x;
            if (inputDirection.x > 0)
                faceDir = 1;
            if (inputDirection.x < 0)
                faceDir = -1;

            transform.localScale = new Vector3(faceDir, 1, 1);
        }
    }

    void SwitchGun()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            guns[gunNum].SetActive(false);
            if(--gunNum < 0)
            {
                gunNum = guns.Length - 1;
            }
            guns[gunNum].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            guns[gunNum].SetActive(false);
            if (++gunNum >guns.Length-1)
            {
                gunNum = 0;
            }
            guns[gunNum].SetActive(true);
        }
    }
}
