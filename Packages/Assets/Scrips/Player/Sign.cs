using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    public GameObject signSprite;
    public bool canPress;

    public Transform playerTrans;
    private PlayerInputControl playerInput;

    private IInteractable targetItem;
    private void Awake()
    {
        playerInput = new PlayerInputControl();
        playerInput.Enable();

    }

    private void OnEnable()
    {
        //InputSystem.onActionChange += OnActionChange;
        playerInput.Gameplay.Confirm.started += OnConfirm;
    }

    private void OnConfirm(InputAction.CallbackContext obj)
    {
        // 先判断canPress为true，且targetItem不是空的，才执行
        if (canPress && targetItem != null)
        {
            targetItem.TriggerAction(); 
        }
        
        else if (canPress && targetItem == null)
        {
            Debug.LogWarning("互动物体上没有找到IInteractable脚本！");
        }
    }

    private void Start()
    {
        canPress = false;
    }
    private void Update()
    {
        signSprite.SetActive(canPress);
        signSprite.transform.localScale = playerTrans.localScale;
    }

    private void OnActionChange(object obj,InputActionChange actionChange)
    {
        if (actionChange == InputActionChange.ActionStarted)
        {
            ;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            canPress = true;
            targetItem = other.GetComponent<IInteractable>();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        canPress = false;
        targetItem = null;
    }
}
