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
    public TeaItem teaItem;          // メイン画面側の「お茶」
    public TMP_Text teaUnlockText;   // 解放/強化コスト表示
    public TMP_Text teaUpgradeText;  // 解放/強化コスト表示（任意）
    public int teaUnlockCost = 1000;
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
        if (waterUpgradeText != null)
        {
            waterUpgradeText.text = NumberFormatter.Format(waterItem.upgradeCost);
        }

        long teaCost = teaUnlocked && teaItem != null
            ? teaItem.upgradeCost
            : teaUnlockCost;

        if (teaUnlockText != null)
        {
            teaUnlockText.text = NumberFormatter.Format(teaCost);
        }

        if (teaUpgradeText != null)
        {
            teaUpgradeText.text = NumberFormatter.Format(teaCost);
        }
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

    // 購入と強化が同じボタンの場合はこれを使う
    public void OnClickTeaAction()
    {
        if (!teaUnlocked)
        {
            if (!MoneySystem.Instance.SpendMoney(teaUnlockCost))
                return;

            teaUnlocked = true;
            UpdateTeaVisual();

            if (teaItem != null)
            {
                teaItem.SetUnlocked(true);
            }
        }
        else
        {
            if (teaItem == null) return;
            teaItem.Upgrade();
        }

        UpdateShopText();
    }
}