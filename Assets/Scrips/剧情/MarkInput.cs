using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkInput : MonoBehaviour
{

    public MarkManager galleryManager;

    private void Update()
    {
        galleryManager = FindObjectOfType<MarkManager>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("mark"))
        {
            galleryManager.OpenGallery();
        }
    }
}
