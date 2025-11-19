using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject[] guns;
    private int gunNum;
    public PlayerInputControl inputControl;
    public Rigidbody2D rb;

    public Vector2 inputDirection;
    public float speed;
    //public Vector3 start;


    private void Awake()
    {
        inputControl = new PlayerInputControl();

    }

    private void Start()
    {
        guns[0].SetActive(true);
        Vector3 startPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }


    private void OnEnable()
    {
        inputControl.Enable();
    }


    private void DisEnable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        SwitchGun();
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, inputDirection.y * speed * Time.deltaTime);
        //ÈËÎï·­×ª
        int faceDir = (int)transform.localScale.x;
        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;

       transform.localScale = new Vector3(faceDir, 1, 1);
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
