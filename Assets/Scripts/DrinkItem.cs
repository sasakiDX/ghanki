using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DrinkItem : MonoBehaviour
{
    [Header("基本データ")]
    public string drinkName = "水";
    public int level = 1;
    public long basePrice = 30;

    [Header("強化設定")]
    public long upgradeCost = 100;
    public float priceIncreaseRate = 1.5f;

    [Header("解放状態")]
    public bool isUnlocked = true;

    [Header("UI")]
    public TMP_Text priceText;
    public Image itemImage;
    public CanvasGroup mainCanvasGroup;

    [Header("売上設定")]
    public float clickMultiplier = 0.5f;
    public float autoInterval = 1f;

    private float timer;

    void Start()
    {
        UpdateView();
    }

    void Update()
    {
        if (!isUnlocked) return;

        timer += Time.deltaTime;
        if (timer >= autoInterval)
        {
            timer = 0f;
            MoneySystem.Instance.AddMoney(basePrice);
        }
    }

    void UpdateView()
    {
        UpdatePriceText();
        UpdateVisual();
        UpdateMainVisibility();
    }

    void UpdatePriceText()
    {
        if (priceText == null) return;
        priceText.text = NumberFormatter.Format(basePrice);
    }

    void UpdateVisual()
    {
        if (itemImage == null) return;

        // 未解放ならグレーアウト
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

    // クリック売上
    public void OnClickSell()
    {
        if (!isUnlocked) return;

        long clickValue = (long)Math.Floor(basePrice * clickMultiplier);
        MoneySystem.Instance.AddMoney(clickValue);
    }

    // 強化処理
    public void Upgrade()
    {
        if (!isUnlocked) return;
        if (!MoneySystem.Instance.SpendMoney(upgradeCost))
            return;

        level++;
        basePrice = (long)Math.Floor(basePrice * priceIncreaseRate);
        upgradeCost = (long)Math.Floor(upgradeCost * 1.8f);

        UpdatePriceText();
        if (ShopUI.Instance != null)
        {
            ShopUI.Instance.UpdateShopText();
        }
    }

    // ショップから解放されたときに呼ぶ
    public void SetUnlocked(bool unlocked)
    {
        isUnlocked = unlocked;
        UpdateView();
    }
}