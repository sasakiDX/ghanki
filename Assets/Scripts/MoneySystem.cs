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
        money += amount;
        UpdateMoneyText();
    }

    // 所持金を減らす
    public bool SpendMoney(long amount)
    {
        if (money < amount) return false;

        money -= amount;
        sales += amount;
        UpdateMoneyText();
        return true;
    }

    // UIを更新（ここでリンクする）
    public void UpdateMoneyText()
    {
        moneyText.text = $"{money}円";
    }
}
