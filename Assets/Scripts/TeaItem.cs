using UnityEngine;
using TMPro;

public class TeaItem : MonoBehaviour
{
    [Header("お茶の基本データ")]
    public int basePrice = 100;

    [Header("UI")]
    public TMP_Text priceText;

    void Start()
    {
        UpdatePriceText();
    }

    public void UpdatePriceText()
    {
        priceText.text = NumberFormatter.Format(basePrice);
    }
}