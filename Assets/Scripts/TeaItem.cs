using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TeaItem : MonoBehaviour
{
    [Header("お茶の基本データ")]
    public int basePrice = 100;

    [Header("購入状態")]
    public bool isUnlocked = false;

    [Header("UI")]
    public TMP_Text priceText;
    public Image itemImage;

    void Start()
    {
        UpdateView();
    }

    void UpdateView()
    {
        UpdatePriceText();
        UpdateVisual();
    }

    void UpdatePriceText()
    {
        priceText.text = NumberFormatter.Format(basePrice);
    }

    void UpdateVisual()
    {
        if (itemImage == null) return;

        // 未購入ならグレーアウト
        itemImage.color = isUnlocked ? Color.white : Color.gray;
    }
}