using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisRelife : MonoBehaviour
{
    public GameObject player;
    public Relife relife;
    public GameObject camerayes;
    public SimpleCamera simpleCamera;





        private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        simpleCamera = camerayes.GetComponent<SimpleCamera>();

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {

            relife.enabled = false;
            simpleCamera.enabled = true;
        }
    }
}
