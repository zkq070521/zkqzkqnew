using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaUIManager : MonoBehaviour
{
    
    public GameObject gachaPanel;
    public GameObject player;
    public RaySkill raySkill;
    public Button btnOpenGacha;
    public Button btnCloseGacha;
    public Button btnSingleGacha;
    public Button btnTenGacha;
    public Image cardDisplayImage;      // 单抽
    //public Text tipText;
    public bool isFirst;
    public Sprite first;
    public Button firstRead;
    public GameObject shuoming;
    public Image[] tenCardImages;       // 十连抽
    public GameObject tenCardPanel;     

    private GachaSystem gachaSystem;
    void Start()
    {

        isFirst = true;
        firstRead.interactable = false;
        gachaSystem = gachaPanel.GetComponent<GachaSystem>();
        shuoming.SetActive(false);
        raySkill = player.GetComponent<RaySkill>();
        raySkill.enabled = false;

        btnOpenGacha.onClick.AddListener(OpenGachaPanel);
        btnCloseGacha.onClick.AddListener(CloseGachaPanel);
        btnSingleGacha.onClick.AddListener(OnSingleGachaClick);
        btnTenGacha.onClick.AddListener(OnTenGachaClick);
        firstRead.onClick.AddListener(OnFirstClick);


        cardDisplayImage.gameObject.SetActive(false);
        //tipText.text = "";
       
        tenCardPanel.SetActive(false);

       
    }

    private void Update()
    {
        if(CoinManager.Instance.currentCoin < 1)
        {
            btnSingleGacha.interactable = false;
        }
        else 
        {
            btnSingleGacha.interactable = true;
        }

        if (CoinManager.Instance.currentCoin < 10)
        {
            btnTenGacha.interactable = false;
        }
        else
        {
            btnTenGacha.interactable = true;
        }
    }



    public void OnFirstClick()
    {

        shuoming.SetActive(true);
        firstRead.interactable = false;
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
        CoinManager.Instance.currentCoin -= 1;
        CoinManager.Instance.UpdateCoinText();
        tenCardPanel.SetActive(false);
        if (isFirst)
        {
            cardDisplayImage.gameObject.SetActive(true);
            cardDisplayImage.sprite = first;
            raySkill.enabled = true;
            isFirst = false;
            firstRead.interactable = true;
        }
        else 
        {
            CardData drawCard = gachaSystem.SingleGacha();
            if (drawCard == null)
            {

                cardDisplayImage.gameObject.SetActive(false);
                return;
            }


            cardDisplayImage.gameObject.SetActive(true);
            cardDisplayImage.sprite = drawCard.cardSprite;
        }

        
        
    }

   
    void OnTenGachaClick()
    {
        CoinManager.Instance.currentCoin -= 10;
        CoinManager.Instance.UpdateCoinText();
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