using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MarkManager : MonoBehaviour
{
    [Header("UI组件")]
    public GameObject galleryCanvas;
    public Image displayImage;
    public Button btnNext;
    public Button btnPrevious;
    //public Button btnBack;

    [Header("壁画图片")]
    public List<Sprite> muralSprites = new List<Sprite>(); // 存储所有壁画Sprite的列表

    private int currentIndex = 0; // 当前显示图片

    void Start()
    {

        btnNext.onClick.AddListener(ShowNextImage);
        btnPrevious.onClick.AddListener(ShowPreviousImage);
        //btnBack.onClick.AddListener(CloseGallery);

        // 初始时更新一次显示和按钮状态
        UpdateDisplayAndButtons();
    }

    // 外部调用打开画廊
    public void OpenGallery(int startIndex = 0)
    {
        currentIndex = startIndex;
        galleryCanvas.SetActive(true);
        UpdateDisplayAndButtons();

        Time.timeScale = 0f;
    }

    void ShowNextImage()
    {
        if (currentIndex < muralSprites.Count - 1)
        {
            currentIndex++;
            UpdateDisplayAndButtons();
        }
    }

    void ShowPreviousImage()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateDisplayAndButtons();
        }
    }

    void UpdateDisplayAndButtons()
    {

        if (muralSprites.Count > 0 && currentIndex >= 0 && currentIndex < muralSprites.Count)
        {
            displayImage.sprite = muralSprites[currentIndex];//其实是数组吧
        }


        // 如果是第一张，则“上一张”按钮不可点
        btnPrevious.interactable = (currentIndex > 0);
        // 如果是最后一张，则“下一张”按钮不可点
        btnNext.interactable = (currentIndex < muralSprites.Count - 1);
    }

    void CloseGallery()
    {
        galleryCanvas.SetActive(false);

        Time.timeScale = 1f;
    }
}