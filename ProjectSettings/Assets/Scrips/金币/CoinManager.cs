using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public TextMeshProUGUI coinText;
    public int currentCoin = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        UpdateCoinText(); 
    }

    public void AddCoin(int amount = 1)
    {
        currentCoin += amount;
        UpdateCoinText();
    }

    
    public void UpdateCoinText()
    {
        if (coinText != null)
        {
          
            //coinText.text = currentCoin.ToString();
           coinText.text = $"{currentCoin}";
        }
        else
        {
            Debug.LogError("金币文本未赋值！");
        }
    }
}