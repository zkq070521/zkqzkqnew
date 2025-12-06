using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GalleryManager : MonoBehaviour
{
    [Header("UI组件绑定")]
    public GameObject galleryCanvas; // 整个画廊UI画布
    public Image displayImage;       // 显示壁画的核心Image组件
    public Button btnNext;           // 下一张按钮
    public Button btnPrevious;       // 上一张按钮
    public Button btnBack;           // 退出按钮

    [Header("壁画图片资源")]
    public List<Sprite> muralSprites = new List<Sprite>(); // 存储所有壁画Sprite的列表

    private int currentIndex = 0; // 当前显示图片的索引

    void Start()
    {
        // 1. 绑定按钮点击事件
        btnNext.onClick.AddListener(ShowNextImage);
        btnPrevious.onClick.AddListener(ShowPreviousImage);
        btnBack.onClick.AddListener(CloseGallery);

        // 2. 初始时，根据当前索引更新一次显示和按钮状态
        UpdateDisplayAndButtons();
    }

    // 外部调用的方法：打开画廊并加载指定索引的图片（例如从不同壁画触发不同图片）
    public void OpenGallery(int startIndex = 0)
    {
        currentIndex = startIndex;
        galleryCanvas.SetActive(true); // 激活画布，显示UI
        UpdateDisplayAndButtons();
        // 这里可以添加代码暂停游戏、锁定玩家输入等
        // Time.timeScale = 0f;
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
        // 1. 更新显示的图片
        if (muralSprites.Count > 0 && currentIndex >= 0 && currentIndex < muralSprites.Count)
        {
            displayImage.sprite = muralSprites[currentIndex];
        }

        // 2. 更新按钮的交互状态（即可点击性）
        // 如果是第一张，则“上一张”按钮不可点
        btnPrevious.interactable = (currentIndex > 0);
        // 如果是最后一张，则“下一张”按钮不可点
        btnNext.interactable = (currentIndex < muralSprites.Count - 1);
    }

    void CloseGallery()
    {
        galleryCanvas.SetActive(false); // 隐藏画布，回到游戏
        // 这里可以添加代码恢复游戏、解锁玩家输入等
        // Time.timeScale = 1f;
    }
}