using UnityEngine;
using System.Collections.Generic;

public class GachaSystem : MonoBehaviour
{
    
    public List<CardData> allCards;  
    // 抽卡概率
    public int rProb = 80;           
    public int srProb = 18;          
    public int ssrProb = 2;       

    // 单抽方法
    public CardData SingleGacha()
    {
        // 生成0-99的随机数
        int randomValue = Random.Range(0, 100);

       
        if (randomValue < ssrProb)
        {
            // 抽SSR：筛选出所有SSR卡，随机选一张
            List<CardData> ssrCards = allCards.FindAll(card => card.rarity == CardRarity.SSR);
            return ssrCards[Random.Range(0, ssrCards.Count)];//ssrCards.Count是总数
        }
        else if (randomValue < ssrProb + srProb)
        {
            
            List<CardData> srCards = allCards.FindAll(c => c.rarity == CardRarity.SR);
            return srCards[Random.Range(0, srCards.Count)];
        }
        else
        {
            
            List<CardData> rCards = allCards.FindAll(c => c.rarity == CardRarity.R);
            return rCards[Random.Range(0, rCards.Count)];
        }
    }

    // 十连抽
    public List<CardData> TenGacha()
    {
        List<CardData> tenCards = new List<CardData>();
        for (int i = 0; i < 10; i++)
        {
            tenCards.Add(SingleGacha());//Add()括号里面的东西加到tenCards末尾
        }
        return tenCards;
    }
}