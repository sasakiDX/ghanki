using UnityEngine;
using UnityEngine.UI;

public class DrinkItem : MonoBehaviour
{
    [Header("Item Settings")]
    public string drinkName;
    public int basePrice = 30;
    public int level = 1;
    public bool unlocked = true;

    [Header("UI")]
    public Text priceText;

    MoneySystem moneySystem;

    void Start()
    {
        moneySystem = Object.FindFirstObjectByType<MoneySystem>();
        UpdateView();
        gameObject.SetActive(unlocked);
    }

    public int CurrentPrice()
    {
        return basePrice * level;
    }

    public int ClickIncome()
    {
        return CurrentPrice() / 2;
    }

    public void OnClickDrink()
    {
        moneySystem.AddMoney(ClickIncome());
    }

    public void LevelUp()
    {
        level++;
        UpdateView();
    }

    public void Unlock()
    {
        unlocked = true;
        gameObject.SetActive(true);
    }

    void UpdateView()
    {
        priceText.text = CurrentPrice().ToString();
    }
}