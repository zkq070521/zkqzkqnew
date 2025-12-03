using TMPro;
using UnityEngine;

public class TipUI : MonoBehaviour
{
    public TMP_Text tipText;
    public GameObject tipBg;

    public static TipUI Instance;

    private void Awake()
    {
       
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject); 
            return;
        }
    }

    public void ShowTip(string text)
    {
        tipText.text = text;
        tipBg.SetActive(true);
    }

    public void HideTip()
    {
        tipBg.SetActive(false);
    }
}