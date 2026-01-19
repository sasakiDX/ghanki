using UnityEngine;
using TMPro;

public class TeaItem : MonoBehaviour
{
    [Header("お茶の基本データ")]
    public int level = 1;
    public int basePrice = 100;

    [Header("解放設定")]
    public bool isUnlocked = false;   // 購入済みかどうか

    [Header("UI")]
    public TMP_Text priceText;

    [Header("売上設定")]
    public float clickMultiplier = 0.5f;
    public float autoInterval = 1f;

    [Header("表示制御")]
    public CanvasGroup canvasGroup;   // プレイ画面のお茶を透明にする用

    private float timer;

    void Start()
    {
        UpdatePriceText();
        UpdateVisibility();   // ← 追加：開始時に表示状態を反映
    }

    void Update()
    {
        if (!isUnlocked) return;   // 未購入なら何もしない

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
        if (!isUnlocked) return;   // 未購入なら反応しない

        int clickValue = Mathf.FloorToInt(basePrice * clickMultiplier);
        MoneySystem.Instance.AddMoney(clickValue);
    }

    // 表示更新（透明 or 表示）
    void UpdateVisibility()
    {
        if (canvasGroup == null) return;

        if (isUnlocked)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.alpha = 0f;          // 完全に透明
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    // 後でショップ購入時に呼ぶ予定
    public void Unlock()
    {
        isUnlocked = true;
        UpdateVisibility();
    }

    public void UpdatePriceText()
    {
        if (priceText != null)
        {
            priceText.text = NumberFormatter.Format(basePrice);
        }
    }
}