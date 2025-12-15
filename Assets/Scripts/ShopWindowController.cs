using UnityEngine;

public class ShopWindowController : MonoBehaviour
{
    public GameObject shopWindow;

    // ショップを開く
    public void OpenShop()
    {
        shopWindow.SetActive(true);
    }

    // ショップを閉じる
    public void CloseShop()
    {
        shopWindow.SetActive(false);
    }
}