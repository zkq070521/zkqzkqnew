using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class YuanShen : MonoBehaviour
{
    public GameObject player;
    public GameObject yuanshen;

    void Awake()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        yuanshen = GameObject.FindGameObjectWithTag("yuanshen");
        
        
        if (player == null)
            Debug.LogError("√ª’“µΩPlayer");
    }

    private void OnTriggerStay2D(Collider2D obj)
    {


        if (obj.gameObject == player)
        {

            yuanshen.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {

        yuanshen.SetActive(false);
        this.gameObject.SetActive(false);
        
    }
}
