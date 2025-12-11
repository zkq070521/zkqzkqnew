using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOmove : MonoBehaviour
{
    public float speed; 
    public PlayerInputControl inputControl; 
    public Rigidbody2D rb;
    public Vector2 inputDirection;

    
    private void Awake()
    {
        
        if (inputControl == null)
            inputControl = new PlayerInputControl();
    }

    
    private void OnEnable()
    {
        if (inputControl != null)
            inputControl.Enable();
    }

    private void OnDisable()
    {
        if (inputControl != null)
            inputControl.Disable();
    }

    private void Update()
    {
        
        inputDirection = inputControl.UFO.Move.ReadValue<Vector2>();
     
        if (inputDirection.magnitude > 1)
            inputDirection = inputDirection.normalized;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
       
        rb.velocity = new Vector2(inputDirection.x * speed, 0f);

        
       /* if (inputDirection.x != 0) // 只有有输入时才翻转
        {
            float faceDir = Mathf.Sign(inputDirection.x); // 直接取输入的符号（1/-1）
            transform.localScale = new Vector3(faceDir, 1, 1);
        }*/
    }

   
   /* private void OnValidate()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }*/
}