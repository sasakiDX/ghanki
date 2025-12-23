using UnityEngine;
using TMPro;
using System.Diagnostics;

public class WaterItem : MonoBehaviour
{
    [Header("水の基本データ")]
    public int level = 1;
    public int basePrice = 30;

    [Header("強化設定")]
    public int upgradeCost = 100;
    public float priceIncreaseRate = 1.5f;

    [Header("UI")]
    public TMP_Text priceText;

    [Header("売上設定")]
    public float clickMultiplier = 0.5f;
    public float autoInterval = 1f;

    /*


     */

    private float timer;

    void Start()
    {
        UpdatePriceText();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= autoInterval)
        {
            timer = 0f;
            MoneySystem.Instance.AddMoney(basePrice);
        }
    }

    // クリック売上
    public void OnClickSell()
    {
        int clickValue = Mathf.FloorToInt(basePrice * clickMultiplier);
        MoneySystem.Instance.AddMoney(clickValue);
    }

    // 強化処理
    public void Upgrade()
    {
        if (!MoneySystem.Instance.SpendMoney(upgradeCost))
            return;

        level++;
        basePrice = Mathf.FloorToInt(basePrice * priceIncreaseRate);
        upgradeCost = Mathf.FloorToInt(upgradeCost * 1.8f);

        UpdatePriceText();
        ShopUI.Instance.UpdateShopText();
    }
    public void UpdatePriceText()
    {
        priceText.text = NumberFormatter.Format(basePrice);
    }
}