using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCup : MonoBehaviour
{
   
    public GameObject oriange;
    public GameObject fruit;
    //public GameObject baoxian;


    private void Start()
    {
        oriange.SetActive(false);
        fruit.SetActive(false);
        //baoxian.SetActive(true);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("green"))
        {
            oriange.SetActive(true);
            fruit.SetActive(true);
            other.gameObject.SetActive(false);
            //baoxian.SetActive(false);
        }
    }

}


