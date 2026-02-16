using System;
using UnityEngine;
using TMPro;

public class MoneySystem : MonoBehaviour
{
    public static MoneySystem Instance;

    [Header("UI")]
    public TMP_Text moneyText;

    [Header("Money Data")]
    public long money = 0;
    public long sales = 0;

    [Header("Fever")]
    public float moneyMultiplier = 1f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateMoneyText();
    }

    //所持金を増やす
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

    //所持金を減らす
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

    public void UpdateMoneyText()
    {
        if (moneyText == null) return;
        moneyText.text = NumberFormatter.FormatFull(money);
    }
}