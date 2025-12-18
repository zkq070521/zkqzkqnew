using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShuomingBack : MonoBehaviour
{
    public GameObject shuoming;
    public Button back;


    private void Start()
    {
        back.onClick.AddListener(Back);
    }

    public void Back()
    {
        this.gameObject.SetActive(false);
    }

}
