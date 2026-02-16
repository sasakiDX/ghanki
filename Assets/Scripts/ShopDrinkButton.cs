using UnityEngine;

public class ShopDrinkButton : MonoBehaviour
{
    public ShopUI shopUI;
    public int drinkIndex = 0;

    //ボタン押下で対象ドリンクの処理へ
    public void OnClick()
    {
        if (shopUI == null) return;
        shopUI.OnClickDrinkAction(drinkIndex);
    }
}