using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriengeSign : MonoBehaviour
{
    public GameObject oriange;//éÙ×Ó
    
    public GameObject player;
    public GameObject dun;//¶ÜÅÆ
    public Transform startTrans;
    public bool a;



    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        oriange.SetActive(false);
        
        a = true;
        dun.SetActive(a);
        startTrans = player.transform;
    }


    private void Update()
    {
        dun.SetActive(a);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("runbox"))
        {
            oriange.SetActive(true);
            other.gameObject.SetActive(false);
            
        }

        if (other.CompareTag("Player"))
        {
            a = false;
            
        }
    }

}
