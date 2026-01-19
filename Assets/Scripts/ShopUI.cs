using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance;

    [Header("ドリンク")]
    public DrinkItem waterItem;
    public DrinkItem teaItem;

    [Header("表示")]
    public TMP_Text waterUpgradeText;
    public TMP_Text teaBuyText;

    public Image teaShopImage;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateShopText();
    }

    public void UpdateShopText()
    {
        waterUpgradeText.text = NumberFormatter.Format(waterItem.price);

        if (!teaItem.isUnlocked)
        {
            teaBuyText.text = NumberFormatter.Format(teaItem.unlockCost);
        }
        else
        {
            teaBuyText.text = "購入済み";
        }

        UpdateTeaShopView();
    }

    public void OnClickUpgradeWater()
    {
        // 強化は後で
    }

    public void OnClickBuyTea()
    {
        bool success = teaItem.Buy();

        if (success)
        {
            UpdateShopText();
        }
    }

    //お茶の表示
    void UpdateTeaShopView()
    {
        if (teaShopImage == null) return;

        if (teaItem.isUnlocked)
        {
            teaShopImage.color = Color.white;
        }
        else
        {
            teaShopImage.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
    }
}
