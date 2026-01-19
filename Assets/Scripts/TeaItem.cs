using UnityEngine;
using UnityEngine.UI;

public class TeaItem : MonoBehaviour
{
    [Header("購入設定")]
    public bool isUnlocked = false;
    public int unlockCost = 2000;

    [Header("価格・売上設定")]
    public int price = 100;              // お茶1本の値段（仮で100円）
    public float clickMultiplier = 0.5f; // クリック売上倍率
    public float autoInterval = 1f;      // 自動売上の間隔（秒）

    [Header("表示用")]
    public Image teaImage;

    private float timer;

    void Start()
    {
        UpdateVisibility();
    }

    void Update()
    {
        // 未購入なら売上は発生しない
        if (!isUnlocked) return;

        // 自動売上
        timer += Time.deltaTime;
        if (timer >= autoInterval)
        {
            timer = 0f;
            MoneySystem.Instance.AddMoney(price);
        }
    }

    // クリック売上（ボタンや画像から呼ぶ）
    public void OnClickSell()
    {
        if (!isUnlocked) return;

        int clickValue = Mathf.FloorToInt(price * clickMultiplier);
        MoneySystem.Instance.AddMoney(clickValue);
    }

    // 購入処理（ショップから呼ばれる）
    public bool BuyTea()
    {
        if (isUnlocked) return false;

        if (!MoneySystem.Instance.SpendMoney(unlockCost))
        {
            return false;
        }

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
            Color c = teaImage.color;
            c.a = 1f;
            teaImage.color = c;
        }
        else
        {
            Color c = teaImage.color;
            c.a = 0f;
            teaImage.color = c;
        }
    }
}
