using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float coinCount { get; set; } = 0;   // コインの数
    private float maxCoin = 10000;              // コインの最大値
    private float minCoin = 0;                  // コインの最小値

    [SerializeField]
    private TextMeshProUGUI coinCountText;      // コインのテキスト

    // Start is called before the first frame update
    void Start()
    {
        // テキストを取得
        coinCountText = GameObject.Find("CoinCountText").GetComponent<TextMeshProUGUI>();

        // コインを最大にする
        coinCount = maxCoin;
    }

    // Update is called once per frame
    void Update()
    {
        // テキストにコインの数を入れる
        coinCountText.text = coinCount.ToString("N0") + "G";

        // コインの数が最小値を超えたら
        if (coinCount < minCoin)
        {
            // 最小値より小さくならないようにする
            coinCount = minCoin;
        }
    }

    // コインの消費処理
    public void UseCoin(float coin)
    {
        // コインの数を減らす
        coinCount -= coin;
    }
}
