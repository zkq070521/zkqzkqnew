using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SignPress : MonoBehaviour
{
    private PlayerInputControl playerInput;
    public bool canPress;


    private void Start()
    {
        canPress = false;
    }

    private void Awake()
    {
        playerInput.Enable();

    }

    private void OnEnable() 
    {

        playerInput.Gameplay.Confirm.started += OnConfirm;
    }

    private void OnConfirm(InputAction.CallbackContext obj)
    {
        if (canPress)
        {

        }
    }
}


