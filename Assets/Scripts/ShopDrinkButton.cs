using UnityEngine;

public class ShopDrinkButton : MonoBehaviour
{
    public ShopUI shopUI;
    public int drinkIndex = 0;

    public void OnClick()
    {
        if (shopUI == null) return;
        shopUI.OnClickDrinkAction(drinkIndex);
    }
}