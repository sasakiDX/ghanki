using UnityEngine;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance;

    public WaterItem waterItem;
    public TMP_Text waterUpgradeText;

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
        waterUpgradeText.text =
            $"{waterItem.upgradeCost}‰~";
    }

    public void OnClickUpgradeWater()
    {
        waterItem.Upgrade();
    }
}