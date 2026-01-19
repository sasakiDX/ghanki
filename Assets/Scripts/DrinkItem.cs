using UnityEngine;
using UnityEngine.UI;

public class DrinkItem : MonoBehaviour
{
    [Header("基本情報")]
    public string drinkName;

    [Header("アンロック設定")]
    public bool isUnlocked = false;
    public int unlockCost = 0;

    [Header("価格・売上設定")]
    public int price = 30;
    public float clickMultiplier = 0.5f;
    public float autoInterval = 1f;

    [Header("表示用（プレイ画面）")]
    public Image drinkImage;

    private float timer;

    void Start()
    {
        UpdateVisibility();
    }

    void Update()
    {
        if (!isUnlocked) return;

        timer += Time.deltaTime;
        if (timer >= autoInterval)
        {
            timer = 0f;
            MoneySystem.Instance.AddMoney(price);
        }
    }
    public void OnClickSell()
    {
        if (!isUnlocked) return;

        int clickValue = Mathf.FloorToInt(price * clickMultiplier);
        MoneySystem.Instance.AddMoney(clickValue);
    }

    public bool Buy()
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

    public void UpdateVisibility()
    {
        if (drinkImage == null) return;

        Color c = drinkImage.color;

        if (isUnlocked)
        {
            c.a = 1f;
        }
        else
        {
            c.a = 0f;
        }

        drinkImage.color = c;
    }
}
