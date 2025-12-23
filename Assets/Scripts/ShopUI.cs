using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance;

    [Header("Water")]
    public WaterItem waterItem;
    public TMP_Text waterUpgradeText;

    [Header("Tea (Shop Only)")]
    public GameObject teaShopRoot;   // ショップ内の「お茶」
    public bool teaUnlocked = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateShopText();
        UpdateTeaVisual();
    }

    public void UpdateShopText()
    {
        waterUpgradeText.text = NumberFormatter.Format(waterItem.upgradeCost);
    }

    void UpdateTeaVisual()
    {
        Image image = teaShopRoot.GetComponent<Image>();
        if (image == null) return;

        image.color = teaUnlocked
    ? Color.white
    : new Color(0.1f, 0.1f, 0.1f, 1f);
    }

    public void OnClickUpgradeWater()
    {
        waterItem.Upgrade();
        UpdateShopText();
    }
}