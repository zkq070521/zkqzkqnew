using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriangeSign : MonoBehaviour
{
    public GameObject oriange;

    private void Start()
    {
        oriange.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            oriange.SetActive(true);
           
        }
    }

    /*private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            oriange.SetActive(false);

        }
    }*/
}
