using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public double totalMoney = 0;       //合計売上
    public double moneyPerSecond = 1;   //秒間売上の初期値

    private void Awake()
    {
        //シングルトン
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        //毎秒売上を加算
        totalMoney += moneyPerSecond * Time.deltaTime;
    }
}