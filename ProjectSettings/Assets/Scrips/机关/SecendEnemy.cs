using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecendEnemy : MonoBehaviour
{
    public bool a;
    public GameObject oriange;

    private void Start()
    {
        a = true;
        oriange.SetActive(a);
    }


    private void Update()
    {
        oriange.SetActive(a);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            a = false;

        }
    }

    
}


