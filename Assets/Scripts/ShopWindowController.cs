using UnityEngine;

public class ShopWindowController : MonoBehaviour
{
    public GameObject shopWindow;

    [Header("Optional")]
    public CanvasGroup mainCanvasGroup; // メイン画面の操作を一括で止める

    // ショップを開く
    public void OpenShop()
    {
        shopWindow.SetActive(true);
        SetMainInteractable(false);
    }

    // ショップを閉じる
    public void CloseShop()
    {
        shopWindow.SetActive(false);
        SetMainInteractable(true);
    }

    void SetMainInteractable(bool enable)
    {
        if (mainCanvasGroup == null) return;
        mainCanvasGroup.interactable = enable;
        mainCanvasGroup.blocksRaycasts = enable;
    }
}