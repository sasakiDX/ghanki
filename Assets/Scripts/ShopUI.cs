using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance;

    [System.Serializable]
    public class ShopDrinkEntry
    {
        public string label;
        public DrinkItem drinkItem;  // メイン画面側のドリンク
        public GameObject shopRoot;  // ショップ内のドリンクルート
        public TMP_Text costText;    // 購入/強化コスト表示
        public long unlockCost = 1000;
        public bool isUnlocked = false;
    }

    [Header("Drinks")]
    public ShopDrinkEntry[] drinks;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SyncFromDrinkItems();
        UpdateShopText();
        UpdateAllDrinkVisuals();
    }

    void SyncFromDrinkItems()
    {
        if (drinks == null) return;

        for (int i = 0; i < drinks.Length; i++)
        {
            if (drinks[i].drinkItem != null)
            {
                drinks[i].isUnlocked = drinks[i].drinkItem.isUnlocked;
            }
        }
    }

    public void UpdateShopText()
    {
        if (drinks == null) return;

        for (int i = 0; i < drinks.Length; i++)
        {
            var entry = drinks[i];
            if (entry.costText == null) continue;

            long cost = entry.isUnlocked && entry.drinkItem != null
                ? entry.drinkItem.upgradeCost
                : entry.unlockCost;

            entry.costText.text = NumberFormatter.FormatLimited(cost);
        }
    }

    void UpdateAllDrinkVisuals()
    {
        if (drinks == null) return;

        for (int i = 0; i < drinks.Length; i++)
        {
            UpdateDrinkVisual(i);
        }
    }

    void UpdateDrinkVisual(int index)
    {
        if (drinks == null || index < 0 || index >= drinks.Length) return;

        var entry = drinks[index];
        if (entry.shopRoot == null) return;

        Image image = entry.shopRoot.GetComponent<Image>();
        if (image == null) return;

        image.color = entry.isUnlocked
            ? Color.white
            : new Color(0.1f, 0.1f, 0.1f, 1f);
    }

    public void OnClickDrinkAction(int index)
    {
        if (drinks == null || index < 0 || index >= drinks.Length) return;

        var entry = drinks[index];

        if (!entry.isUnlocked)
        {
            if (!MoneySystem.Instance.SpendMoney(entry.unlockCost))
                return;

            entry.isUnlocked = true;
            if (entry.drinkItem != null)
            {
                entry.drinkItem.SetUnlocked(true);
            }

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayPurchaseSfx();
            }

            UpdateDrinkVisual(index);
        }
        else
        {
            if (entry.drinkItem == null) return;
            bool upgraded = entry.drinkItem.Upgrade();

            if (upgraded && AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayPurchaseSfx();
            }
        }

        UpdateShopText();
    }
}