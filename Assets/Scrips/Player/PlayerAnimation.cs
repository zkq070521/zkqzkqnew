using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetAnimation();
    }

    public void SetAnimation()
    {
        bool isidle = Mathf.Abs(rb.velocity.x) < 0.1 ;
        //anim.SetFloat("veMathf.Abs(rb.velocity.x)locityX", );
        // anim.SetFloat("velocityY", Mathf.Abs(rb.velocity.y));
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));

    }
}
