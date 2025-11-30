using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public Transform groundCheck;
    public float checkRaduis;
    public bool isGround;
    public LayerMask groundLayer;
    void Update()
    {
        Check(); ;
    }

    private void Check()
    {
        //ºÏ≤‚µÿ√Ê
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRaduis,groundLayer);
    }
}
