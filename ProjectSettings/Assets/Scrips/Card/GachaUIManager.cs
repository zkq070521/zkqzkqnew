using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaUIManager : MonoBehaviour
{
    
    public GameObject gachaPanel;
    public Button btnOpenGacha;
    public Button btnCloseGacha;
    public Button btnSingleGacha;
    public Button btnTenGacha;
    public Image cardDisplayImage;      // 单抽
    //public Text tipText;

    
    public Image[] tenCardImages;       // 十连抽
    public GameObject tenCardPanel;     

    private GachaSystem gachaSystem;
    void Start()
    {

    
        gachaSystem = gachaPanel.GetComponent<GachaSystem>();

        
        btnOpenGacha.onClick.AddListener(OpenGachaPanel);
        btnCloseGacha.onClick.AddListener(CloseGachaPanel);
        btnSingleGacha.onClick.AddListener(OnSingleGachaClick);
        btnTenGacha.onClick.AddListener(OnTenGachaClick);

       
        cardDisplayImage.gameObject.SetActive(false);
        //tipText.text = "";
       
        tenCardPanel.SetActive(false);

       
    }



void OpenGachaPanel()
    {
        gachaPanel.SetActive(true); 
    }

    
   
    void CloseGachaPanel()
    {
        gachaPanel.SetActive(false);

        
        if (cardDisplayImage != null)
        {
            cardDisplayImage.gameObject.SetActive(false);
        }

        
        if (tenCardPanel != null)
        {
            tenCardPanel.SetActive(false);
            
            if (tenCardImages != null)
            {
                foreach (Image img in tenCardImages)
                {
                    if (img != null) img.gameObject.SetActive(false);
                }
            }
        }

       /* if (tipText != null)
        {
            tipText.text = "";
        }*/
    }
    void OnSingleGachaClick()
    {
        
        tenCardPanel.SetActive(false);

        
        CardData drawCard = gachaSystem.SingleGacha();
        if (drawCard == null)
        {
          
            cardDisplayImage.gameObject.SetActive(false);
            return;
        }

       
        cardDisplayImage.gameObject.SetActive(true);
        cardDisplayImage.sprite = drawCard.cardSprite;
        
    }

   
    void OnTenGachaClick()
    {
       
        cardDisplayImage.gameObject.SetActive(false);

       
        btnSingleGacha.interactable = false;
        btnTenGacha.interactable = false;

       
        List<CardData> tenCards = gachaSystem.TenGacha();
        if (tenCards == null || tenCards.Count != 10)
        {
           
            btnSingleGacha.interactable = true;
            btnTenGacha.interactable = true;
            return;
        }

        
        //ShowTenCardsLayout(tenCards);

        
       

        StartCoroutine(ShowTenCards(tenCards));
    }

   
 

  
    void ShowCard(CardData card)
    {
        cardDisplayImage.gameObject.SetActive(true); 
        cardDisplayImage.sprite = card.cardSprite;   
      
    }

   
    IEnumerator ShowTenCards(List<CardData> tenCards)
    {

        tenCardPanel.SetActive(true);

      
        foreach (Image img in tenCardImages)//遍历数组
        {
            if (img != null) img.gameObject.SetActive(false);
        }


        for (int i = 0; i < tenCards.Count; i++)
        {
            if (i >= tenCardImages.Length) break;

            
            CardData card = tenCards[i];
            Image cardImage = tenCardImages[i];

           
            cardImage.gameObject.SetActive(true);
            cardImage.sprite = card.cardSprite;
            yield return new WaitForSeconds(0.05f); 
        }

      
        btnSingleGacha.interactable = true;
        btnTenGacha.interactable = true;

       
      
    }
}