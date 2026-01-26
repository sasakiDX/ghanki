using UnityEngine;
using UnityEngine.EventSystems;

public class ShopDrinkButton : MonoBehaviour, IPointerDownHandler
{
    public ShopUI shopUI;
    public int drinkIndex = 0;

    [Header("Input")]
    public bool triggerOnPointerDown = true;

    public void OnClick()
    {
        if (triggerOnPointerDown) return; // prevent double-fire if Button.OnClick is still wired
        DoAction();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!triggerOnPointerDown) return;
        DoAction();
    }

    void DoAction()
    {
        if (shopUI == null) return;
        shopUI.OnClickDrinkAction(drinkIndex);
    }
}