using UnityEngine;

public enum CardRarity
{
    R,    
    SR,   
    SSR   
}


[System.Serializable]
public class CardData//就是结构体吧
{
    public string cardName;       
    public CardRarity rarity;    
    public Sprite cardSprite;    
}