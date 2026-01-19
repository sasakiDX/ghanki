using UnityEngine;
using UnityEngine.UI;

public class TeaItem : MonoBehaviour
{
    [Header("購入設定")]
    public bool isUnlocked = false;
    public int unlockCost = 2000;

    [Header("表示用")]
    public Image teaImage;   // プレイ画面のお茶のImage

    void Start()
    {
        UpdateVisibility();
    }

    // 購入処理（ショップから呼ばれる）
    public bool BuyTea()
    {
        // すでに購入済みなら何もしない
        if (isUnlocked) return false;

        // お金が足りなければ失敗
        if (!MoneySystem.Instance.SpendMoney(unlockCost))
        {
            return false;
        }

        // 購入成功
        isUnlocked = true;
        UpdateVisibility();

        return true;
    }

    // 表示状態の更新
    public void UpdateVisibility()
    {
        if (teaImage == null) return;

        if (isUnlocked)
        {
            // 表示する（完全表示）
            Color c = teaImage.color;
            c.a = 1f;
            teaImage.color = c;
        }
        else
        {
            // 非表示（透明）
            Color c = teaImage.color;
            c.a = 0f;
            teaImage.color = c;
        }
    }
}