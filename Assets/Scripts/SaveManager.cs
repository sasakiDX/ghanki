using System.Collections;
using System.Globalization;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    [Header("Auto Save")]
    public float autosaveInterval = 5f;

    private bool dirty;

    const string KeyVersion = "save_version";
    const int Version = 1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        StartCoroutine(LoadAfterInit());
        StartCoroutine(AutoSaveLoop());
    }

    IEnumerator LoadAfterInit()
    {
        yield return null;
        Load();
    }

    IEnumerator AutoSaveLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(autosaveInterval);
            if (dirty)
            {
                Save();
                dirty = false;
            }
        }
    }

    public void MarkDirty()
    {
        dirty = true;
    }

    public void Save()
    {
        PlayerPrefs.SetInt(KeyVersion, Version);

        var moneySystem = MoneySystem.Instance != null ? MoneySystem.Instance : FindObjectOfType<MoneySystem>();
        if (moneySystem != null)
        {
            PlayerPrefs.SetString("money", moneySystem.money.ToString(CultureInfo.InvariantCulture));
            PlayerPrefs.SetString("sales", moneySystem.sales.ToString(CultureInfo.InvariantCulture));
        }

        var shop = ShopUI.Instance != null ? ShopUI.Instance : FindObjectOfType<ShopUI>();
        if (shop != null && shop.drinks != null)
        {
            PlayerPrefs.SetInt("drink_count", shop.drinks.Length);
            for (int i = 0; i < shop.drinks.Length; i++)
            {
                var entry = shop.drinks[i];
                var item = entry.drinkItem;

                PlayerPrefs.SetInt($"drink_{i}_unlocked", entry.isUnlocked ? 1 : 0);

                if (item != null)
                {
                    PlayerPrefs.SetInt($"drink_{i}_level", item.level);
                    PlayerPrefs.SetString($"drink_{i}_basePrice", item.basePrice.ToString(CultureInfo.InvariantCulture));
                    PlayerPrefs.SetString($"drink_{i}_upgradeCost", item.upgradeCost.ToString(CultureInfo.InvariantCulture));
                }
            }
        }

        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey(KeyVersion))
        {
            return;
        }

        var moneySystem = MoneySystem.Instance != null ? MoneySystem.Instance : FindObjectOfType<MoneySystem>();
        if (moneySystem != null)
        {
            moneySystem.money = ReadLong("money", moneySystem.money);
            moneySystem.sales = ReadLong("sales", moneySystem.sales);
            moneySystem.UpdateMoneyText();
        }

        var shop = ShopUI.Instance != null ? ShopUI.Instance : FindObjectOfType<ShopUI>();
        if (shop != null && shop.drinks != null)
        {
            int count = PlayerPrefs.GetInt("drink_count", shop.drinks.Length);
            int limit = Mathf.Min(count, shop.drinks.Length);
            for (int i = 0; i < limit; i++)
            {
                var entry = shop.drinks[i];
                var item = entry.drinkItem;

                bool unlocked = PlayerPrefs.GetInt($"drink_{i}_unlocked", entry.isUnlocked ? 1 : 0) == 1;
                entry.isUnlocked = unlocked;

                if (item != null)
                {
                    item.level = PlayerPrefs.GetInt($"drink_{i}_level", item.level);
                    item.basePrice = ReadLong($"drink_{i}_basePrice", item.basePrice);
                    item.upgradeCost = ReadLong($"drink_{i}_upgradeCost", item.upgradeCost);
                    item.SetUnlocked(unlocked);
                }
            }

            shop.RefreshAll();
        }
    }

    long ReadLong(string key, long fallback)
    {
        string s = PlayerPrefs.GetString(key, fallback.ToString(CultureInfo.InvariantCulture));
        if (long.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out long v))
        {
            return v;
        }
        return fallback;
    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Save();
        }
    }

    void OnApplicationQuit()
    {
        Save();
    }
}