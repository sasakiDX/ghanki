using UnityEngine;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance;

    [Header("水")]
    public WaterItem waterItem;
    public TMP_Text waterUpgradeText;

    [Header("お茶")]
    public TeaItem teaItem;
    public TMP_Text teaBuyText;   // 「2000円」など表示するテキスト

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
        // 水の強化費用表示
        waterUpgradeText.text = NumberFormatter.Format(waterItem.upgradeCost);

        // お茶の購入費用表示（未購入のときだけ）
        if (!teaItem.isUnlocked)
        {
            teaBuyText.text = NumberFormatter.Format(teaItem.unlockCost);
        }
        else
        {
            teaBuyText.text = "購入済み";
        }
    }

    // 水の強化ボタン
    public void OnClickUpgradeWater()
    {
        waterItem.Upgrade();
        UpdateShopText();
    }

    // ★ お茶の購入ボタン（今回追加）
    public void OnClickBuyTea()
    {
        bool success = teaItem.BuyTea();

        if (success)
        {
            UpdateShopText();
        }
    }
}