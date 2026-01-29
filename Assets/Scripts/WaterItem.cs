using UnityEngine;
using TMPro;
using System.Diagnostics;

public class WaterItem : MonoBehaviour
{
    [Header("���̊�{�f�[�^")]
    public int level = 1;
    public int basePrice = 30;

    [Header("�����ݒ�")]
    public int upgradeCost = 100;
    public float priceIncreaseRate = 1.5f;

    [Header("UI")]
    public TMP_Text priceText;

    [Header("����ݒ�")]
    public float clickMultiplier = 0.5f;
    public float autoInterval = 1f;

    /*


     */

    private float timer;

    void Start()
    {
        UpdatePriceText();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= autoInterval)
        {
            timer = 0f;
            MoneySystem.Instance.AddMoney(basePrice);
        }
    }

    // �N���b�N����
    public void OnClickSell()
    {
        int clickValue = Mathf.FloorToInt(basePrice * clickMultiplier);
        MoneySystem.Instance.AddMoney(clickValue);
    }

    // ��������
    public void Upgrade()
    {
        if (!MoneySystem.Instance.SpendMoney(upgradeCost))
            return;

        level++;
        basePrice = Mathf.FloorToInt(basePrice * priceIncreaseRate);
        upgradeCost = Mathf.FloorToInt(upgradeCost * 1.8f);

        UpdatePriceText();
        ShopUI.Instance.UpdateShopText();
    }
    public void UpdatePriceText()
    {
        priceText.text = NumberFormatter.FormatLimited(basePrice);
    }
}