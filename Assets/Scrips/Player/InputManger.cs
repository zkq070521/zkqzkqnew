
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private PlayerInput playerInput;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 获取PlayerInput组件（从玩家对象获取）
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                playerInput = player.GetComponent<PlayerInput>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 统一的输入切换方法
    public void SwitchToGameplay() => playerInput?.SwitchCurrentActionMap("Gameplay");
    public void SwitchToUI() => playerInput?.SwitchCurrentActionMap("UI");
    public void SwitchToMouse() => playerInput?.SwitchCurrentActionMap("Mouse");
    public void DisableAll() => playerInput?.DeactivateInput();
}