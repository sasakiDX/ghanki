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
    public CanvasGroup mainCanvasGroup;

    void Start()
    {
        UpdateView();
    }

    void UpdateView()
    {
        UpdatePriceText();
        UpdateVisual();
        UpdateMainVisibility();
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

    void UpdateMainVisibility()
    {
        if (mainCanvasGroup == null) return;

        // 未解放ならメイン画面を透明化
        mainCanvasGroup.alpha = isUnlocked ? 1f : 0f;
        mainCanvasGroup.interactable = isUnlocked;
        mainCanvasGroup.blocksRaycasts = isUnlocked;
    }

    // ショップから解放されたときに呼ぶ
    public void SetUnlocked(bool unlocked)
    {
        isUnlocked = unlocked;
        UpdateView();
    }
}
