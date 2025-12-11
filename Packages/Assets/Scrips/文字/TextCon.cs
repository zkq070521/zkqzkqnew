using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCon : MonoBehaviour
{
    public GameObject player;
    public string text;

    void Start()
    {
        TipUI.Instance.HideTip();
        player = GameObject.FindGameObjectWithTag("Player");
        
        if (player == null)
            Debug.LogError("Ã»ÕÒµ½Player");
    }

    private void OnTriggerStay2D(Collider2D obj)
    {


        if (obj.gameObject == player)
        {

            if (TipUI.Instance != null)
                TipUI.Instance.ShowTip(text);
            else
                Debug.LogError("TipUI.InstanceÎª¿Õ");
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject == player && TipUI.Instance != null)
        {
            TipUI.Instance.HideTip();
            this.gameObject.SetActive(false);
        }
    }
}