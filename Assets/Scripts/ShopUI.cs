using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance;

    [Header("水")]
    public WaterItem waterItem;
    public TMP_Text waterUpgradeText;

    [Header("お茶")]
    public TeaItem teaItem;
    public TMP_Text teaBuyText;
    public Image teaShopImage;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateShopText();
        UpdateTeaShopView();
    }

    public void UpdateShopText()
    {
        waterUpgradeText.text = NumberFormatter.Format(waterItem.upgradeCost);

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

    // 水の強化ボタン
    public void OnClickUpgradeWater()
    {
        waterItem.Upgrade();
        UpdateShopText();
    }

    // お茶の購入ボタン
    public void OnClickBuyTea()
    {
        bool success = teaItem.BuyTea();

        if (success)
        {
            UpdateShopText();
        }
    }

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