using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    
        private PlayerInput playerInput;

        void Awake()  // 改用 Awake 而不是 Start
        {
            playerInput = GetComponent<PlayerInput>();
            if (playerInput == null)
            {
                Debug.LogError("PlayerInput component not found!", this);
            }
        }

        void OnEnable()
        {
            // 确保输入被正确激活
            if (playerInput != null && !playerInput.inputIsActive)
            {
                playerInput.ActivateInput();
            }
        }

        void OnDisable()
        {
            DisableAllInput();
        }

        void OnDestroy()
        {
            DisableAllInput();
        }

        public void DisableAllInput()
        {
            if (playerInput != null)
            {
                Debug.Log("Disabling PlayerInput: " + playerInput.name);
                playerInput.DeactivateInput();

                // 额外保险：直接禁用所有Action Maps
                if (playerInput.actions != null)
                {
                    foreach (InputActionMap actionMap in playerInput.actions.actionMaps)
                    {
                        actionMap.Disable();
                    }
                }
            }
        }
    
}
