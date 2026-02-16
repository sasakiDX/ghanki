using System;
using UnityEngine;
using TMPro;

public class MoneySystem : MonoBehaviour
{
    public static MoneySystem Instance;

    [Header("UI")]
    public TMP_Text moneyText;  // MoneyText をアサインする

    [Header("Money Data")]
    public long money = 0;     // 所持金
    public long sales = 0;     // 売上（UIに表示しない）

    [Header("Fever")]
    public float moneyMultiplier = 1f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateMoneyText();
    }

    // 所持金を増やす
    public void AddMoney(long amount)
    {
        long add = (long)Math.Floor(amount * moneyMultiplier);
        money += add;
        UpdateMoneyText();
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.MarkDirty();
        }
    }

    // 所持金を減らす
    public bool SpendMoney(long amount)
    {
        if (money < amount) return false;

        money -= amount;
        sales += amount;
        UpdateMoneyText();
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.MarkDirty();
        }
        return true;
    }

    public void SetMultiplier(float multiplier)
    {
        moneyMultiplier = Mathf.Max(0f, multiplier);
    }

    // UIを更新（ここでリンクする）
    public void UpdateMoneyText()
    {
        moneyText.text = NumberFormatter.FormatFull(money);
    }
}