using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BWText : MonoBehaviour
{
    private Spawn spawnPos;        // スポーン

    [SerializeField]
    private TextMeshProUGUI bwText;   // bwのテキスト

    [SerializeField]
    private TextMeshProUGUI bwuText;  // bwuのテキスト

    // Start is called before the first frame update
    void Start()
    {
        // ウェーブマネージャーを取得
        spawnPos = GetComponent<Spawn>();

        // テキストを取得
        bwText = GameObject.Find("BWText").GetComponent<TextMeshProUGUI>();

        // テキストを取得
        bwuText = GameObject.Find("BWUText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // 今いるシーンがタイトルとステージセレクト以外のとき)
        if (SceneManager.GetActiveScene().name != "Title" || SceneManager.GetActiveScene().name != "StageSelect")
        {
            // テキスト表示用の値を計算
            spawnPos.deathCharacterCountText = spawnPos.deathCharacterCountMax - spawnPos.deathCharacterCount;

            // 味方の死亡数が最大を超えたら
            if (spawnPos.deathCharacterCount >= spawnPos.deathCharacterCountMax)
            {
                // bw出撃準備完了テキストを表示
                bwText.text = "出撃準備完了" + "\n" + "！！";

                // コルーチンを開始
                StartCoroutine(Blink());
            }
            else
            {
                // 透明度を元に戻す
                bwText.color = new Color(bwText.color.r, bwText.color.g, bwText.color.b, 1.0f);

                // bw出撃までのテキストを表示
                bwText.text = "出撃可能まで" + "\n" + "あと" + "\n" + spawnPos.deathCharacterCountText.ToString("N0") + "体" + "\n" + "死亡";
            }

            // テキスト表示用の値を計算
            spawnPos.deathCharacterCountTextBWU = spawnPos.deathCharacterCountMaxBWU - spawnPos.deathCharacterCount;

            // 味方の死亡数が最大を超えたら
            if (spawnPos.deathCharacterCount >= spawnPos.deathCharacterCountMaxBWU)
            {
                // bwu出撃準備完了テキストを表示
                bwuText.text = "出撃準備完了" + "\n" + "！！";

                // コルーチンを開始
                StartCoroutine(BlinkBWU());
            }
            else
            {
                // 透明度を元に戻す
                bwuText.color = new Color(bwuText.color.r, bwuText.color.g, bwuText.color.b, 1.0f);

                // bwu出撃までのテキストを表示
                bwuText.text = "出撃可能まで" + "\n" + "あと" + "\n" + spawnPos.deathCharacterCountTextBWU.ToString("N0") + "体" + "\n" + "死亡";
            }
        }
    }

    // 点滅のコルーチン
    IEnumerator Blink()
    {
        // 現在の透明度を取得
        float alpha = Mathf.PingPong(Time.time * spawnPos.blinkSpeed, 1.0f);

        // SpriteRendererの透明度を更新
        bwText.color = new Color(bwText.color.r, bwText.color.g, bwText.color.b, alpha);

        // 0.02秒待つ
        yield return new WaitForSeconds(0.02f);
    }

    // 点滅のコルーチン
    IEnumerator BlinkBWU()
    {
        // 現在の透明度を取得
        float alpha = Mathf.PingPong(Time.time * spawnPos.blinkSpeed, 1.0f);

        // SpriteRendererの透明度を更新
        bwuText.color = new Color(bwuText.color.r, bwuText.color.g, bwuText.color.b, alpha);

        // 0.02秒待つ
        yield return new WaitForSeconds(0.02f);
    }
}
