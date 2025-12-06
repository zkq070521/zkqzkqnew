using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigitalPasswordManager : MonoBehaviour
{
    // 单例模式（跨场景持久化）
    public static DigitalPasswordManager Instance;

    [Header("UI组件引用")]
    public GameObject passwordCanvas;
    public Image[] digitSlots;     // 4个数字显示槽（必须赋值4个）
    public Button[] numberButtons; // 0-9数字按钮（必须赋值10个）
    public Button backButton;      // Back按钮
    public Button exitButton;       // 新增：退出密码界面按钮
    //public Text messageText;       // 提示信息（取消注释需赋值）

    [Header("数字图片设置")]
    public Sprite[] digitSprites;  // 0-9的数字显示图片（必须赋值10个）
    public Sprite emptySlotSprite; // 空槽位图片（可选，优先用这个）

    [Header("密码门设置")]
    public string correctPassword = "6511";
    public GameObject door;  // 门的碰撞体（可手动拖入，无需代码查找）
    public bool autoFindDoor = true; // 是否自动查找门物体（手动拖入则设为false）

    // 输入记录：存储<位置索引, 数字值>
    private Dictionary<int, int> currentInput = new Dictionary<int, int>();
    private int currentPosition = 0; // 当前输入位置（0-3）
    private bool isActive = false;
    private CursorLockMode originalCursorMode; // 保存原始光标状态
    private bool originalCursorVisible;        // 保存原始光标可见性

    void Awake()
    {
        // 单例+跨场景持久化
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 避免场景切换销毁
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        door = GameObject.FindGameObjectWithTag("Opendoor");

        // 2. 无论是否找到门，都初始化UI（核心修复：原代码只在找到门时初始化）
        HidePasswordUI();
        InitializeDisplay();

        // 3. 校验关键数组（避免越界）
        if (digitSlots.Length != 4)
        {
            Debug.LogError("digitSlots必须赋值4个Image组件！");
        }
        if (numberButtons.Length != 10)
        {
            Debug.LogError("numberButtons必须赋值10个Button组件（0-9）！");
        }
        if (digitSprites.Length != 10)
        {
            Debug.LogError("digitSprites必须赋值10个Sprite（0-9）！");
        }

        // 4. 绑定按钮事件（核心修复：原代码只在找到门时绑定）
        if (numberButtons.Length == 10)
        {
            for (int i = 0; i < numberButtons.Length; i++)
            {
                int number = i;
                if (numberButtons[i] != null)
                {
                    numberButtons[i].onClick.AddListener(() => AddNumber(number));
                }
                else
                {
                    Debug.LogError($"第{i}个数字按钮未赋值！");
                }
            }
        }

        if (backButton != null)
        {
            backButton.onClick.AddListener(RemoveLastNumber);
        }
        else
        {
            Debug.LogError("Back按钮未赋值！");
        }

        // 新增：绑定退出按钮事件
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitPasswordUI);
        }
        else
        {
            Debug.LogError("退出按钮未赋值！");
        }

        UpdateMessage("请输入4位密码", Color.white);
    }

    // 初始化显示
    public void InitializeDisplay()
    {
        ClearAllSlots();
        currentInput.Clear();
        currentPosition = 0;
    }

    // 显示密码界面（保存原始光标状态）
    public void ShowPasswordUI()
    {
        if (isActive) return;

        // 保存原始光标状态（兼容原有游戏设置）
        originalCursorMode = Cursor.lockState;
        originalCursorVisible = Cursor.visible;

        passwordCanvas.SetActive(true);
        isActive = true;
        InitializeDisplay();

        UpdateMessage("请输入4位密码", Color.white);

        // 暂停游戏+显示光标
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // 隐藏密码界面（恢复原始光标状态）
    public void HidePasswordUI()
    {
        if (!isActive) return;

        passwordCanvas.SetActive(false);
        isActive = false;

        // 恢复游戏+光标状态
        Time.timeScale = 1f;
        Cursor.lockState = originalCursorMode;
        Cursor.visible = originalCursorVisible;
    }

    // 新增：退出密码界面的核心方法
    public void ExitPasswordUI()
    {
        if (!isActive) return;
       
        // 1. 清空当前输入（可选，退出时重置输入）
        InitializeDisplay();
        // 2. 隐藏UI并恢复游戏状态
        HidePasswordUI();
        // 3. 提示信息（可选）
        UpdateMessage("已退出密码输入", Color.gray);
        Debug.Log("玩家点击退出按钮，关闭密码界面");
    }

// 添加数字（防重复点击）
public void AddNumber(int number)
{
    if (isActive == false || currentPosition >= 4) return;

    // 记录输入
    currentInput[currentPosition] = number;

    // 显示对应的数字图片
    DisplayDigit(currentPosition, number);

    // 移动到下一个位置
    currentPosition++;

    Debug.Log($"输入: {number}, 位置: {currentPosition - 1}, 当前输入: {GetCurrentInputString()}");

    // 输入满4位时验证
    if (currentPosition == 4)
    {
        CheckPassword();
    }
}

// 在指定位置显示数字图片
void DisplayDigit(int position, int digit)
{
    if (position < 0 || position >= digitSlots.Length || digitSlots[position] == null) return;
    if (digit < 0 || digit >= digitSprites.Length || digitSprites[digit] == null) return;

    Image slot = digitSlots[position];
    slot.sprite = digitSprites[digit];
    slot.color = Color.white;
}

// 移除最后一个数字
public void RemoveLastNumber()
{
    if (!isActive || currentPosition <= 0)
    {
        UpdateMessage("没有输入可删除", Color.yellow);
        return;
    }

    // 移动到上一个位置
    currentPosition--;

    // 清空该位置的显示
    ClearSlot(currentPosition);

    // 从记录中移除
    if (currentInput.ContainsKey(currentPosition))
    {
        currentInput.Remove(currentPosition);
    }

    UpdateMessage("已删除上一个输入", Color.yellow);
    Debug.Log($"Back操作，当前位置: {currentPosition}, 当前输入: {GetCurrentInputString()}");
}

// 清空指定槽位（优先用空图片，无则半透明）
void ClearSlot(int position)
{
    if (position < 0 || position >= digitSlots.Length || digitSlots[position] == null) return;

    Image slot = digitSlots[position];
    if (emptySlotSprite != null)
    {
        slot.sprite = emptySlotSprite;
    }
    slot.color = new Color(1, 1, 1, 0.3f); // 半透明
}

// 清空所有槽位
void ClearAllSlots()
{
    for (int i = 0; i < digitSlots.Length; i++)
    {
        ClearSlot(i);
    }
}

// 获取当前输入字符串
string GetCurrentInputString()
{
    string result = "";
    for (int i = 0; i < 4; i++)
    {
        result += currentInput.ContainsKey(i) ? currentInput[i].ToString() : "_";
    }
    return result;
}

// 验证密码
void CheckPassword()
{
    string input = "";
    for (int i = 0; i < 4; i++)
    {
        input += currentInput.ContainsKey(i) ? currentInput[i].ToString() : "";
    }

    Debug.Log($"验证密码，输入: {input}, 正确密码: {correctPassword}");

    if (input == correctPassword)
    {
        UpdateMessage("密码正确！门已打开", Color.green);
        OpenDoor();
        PlaySuccessEffects();
        // 修复：Time.timeScale=0时用协程+WaitForSecondsRealtime
        StartCoroutine(DelayHideUI(2f));
    }
    else
    {
        UpdateMessage("密码错误！请重试", Color.red);
        PlayErrorEffects();
        StartCoroutine(DelayResetInput(1.5f));
    }
}

// 开门（加容错）
void OpenDoor()
{
    if (door != null)
    {
        door.SetActive(false);
        Debug.Log("门已打开，碰撞体已禁用");
    }
    else
    {
        Debug.LogWarning("门碰撞体未赋值，无法开门！");
    }
}

// 延迟隐藏UI（不受Time.timeScale影响）
IEnumerator DelayHideUI(float delay)
{
    yield return new WaitForSecondsRealtime(delay);
    HidePasswordUI();
}

// 延迟重置输入（不受Time.timeScale影响）
IEnumerator DelayResetInput(float delay)
{
    yield return new WaitForSecondsRealtime(delay);
    InitializeDisplay();
    UpdateMessage("请输入4位密码", Color.white);
}

// 更新提示信息
void UpdateMessage(string text, Color color)
{
    /*if (messageText != null)
    {
        messageText.text = text;
        messageText.color = color;
    }*/
}

// 成功效果（闪光）
void PlaySuccessEffects()
{
    StartCoroutine(FlashDigits(Color.green));
    Debug.Log("密码正确！");
}

// 错误效果（抖动）
void PlayErrorEffects()
{
    StartCoroutine(ShakeDigits());
    Debug.Log("密码错误！");
}

// 数字闪光效果协程（优化索引）
IEnumerator FlashDigits(Color flashColor)
{
    // 保存原始颜色
    Color[] originalColors = new Color[digitSlots.Length];
    for (int i = 0; i < digitSlots.Length; i++)
    {
        originalColors[i] = digitSlots[i] != null ? digitSlots[i].color : Color.white;
    }

    // 闪烁3次
    for (int flash = 0; flash < 3; flash++)
    {
        // 变绿色
        for (int i = 0; i < digitSlots.Length; i++)
        {
            if (digitSlots[i] != null) digitSlots[i].color = flashColor;
        }
        yield return new WaitForSecondsRealtime(0.2f);

        // 恢复
        for (int i = 0; i < digitSlots.Length; i++)
        {
            if (digitSlots[i] != null) digitSlots[i].color = originalColors[i];
        }
        yield return new WaitForSecondsRealtime(0.2f);
    }
}

// 数字抖动效果协程（优化索引，避免Array.IndexOf）
IEnumerator ShakeDigits()
{
    // 保存原始位置
    Vector3[] originalPositions = new Vector3[digitSlots.Length];
    for (int i = 0; i < digitSlots.Length; i++)
    {
        originalPositions[i] = digitSlots[i] != null ? digitSlots[i].transform.localPosition : Vector3.zero;
    }

    // 抖动效果
    float shakeIntensity = 10f;
    for (int shake = 0; shake < 10; shake++)
    {
        for (int i = 0; i < digitSlots.Length; i++)
        {
            if (digitSlots[i] == null) continue;
            Vector3 shakeOffset = new Vector3(
                Random.Range(-shakeIntensity, shakeIntensity),
                Random.Range(-shakeIntensity, shakeIntensity),
                0
            );
            digitSlots[i].transform.localPosition = originalPositions[i] + shakeOffset;
        }
        yield return new WaitForSecondsRealtime(0.05f);
    }

    // 恢复位置
    for (int i = 0; i < digitSlots.Length; i++)
    {
        if (digitSlots[i] != null)
        {
            digitSlots[i].transform.localPosition = originalPositions[i];
        }
    }
}

   /* // 可选：关门方法（复用）
    public void CloseDoor()
    {
        if (door != null)
        {
            door.enabled = true;
            Debug.Log("门已关闭，碰撞体已启用");
        }
    }*/
}