using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DinoText : MonoBehaviour
{
    private Dino dino;  // Dino

    [SerializeField]
    private TextMeshProUGUI dinoText;   // Dinoのテキスト
    private DinoSpawn dinoSpawn;        // Dinoの生成

    public float blinkSpeed = 1.0f;     // 点滅の速さを調整するための変数
    public float dinoTextTimer;         // Dinoが出現するまでの残り時間計算用タイマー
    private float dinoTextIntarval = 20.0f;     // Dino出現20秒前の計算用変数

    // Start is called before the first frame update
    void Start()
    {
        // Dinoを取得
        dino = GameObject.FindGameObjectWithTag("Dino").GetComponent<Dino>();

        // Dinoの生成を取得
        dinoSpawn = GameObject.Find("GameManager").GetComponent<DinoSpawn>();

        // テキストを取得
        dinoText = GameObject.Find("DinoText").GetComponent<TextMeshProUGUI>();

        // テキストを消す
        dinoText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Dinoが再生成されたとき
        if (dinoSpawn.dinoSpawnFlg)
        {
            // タグを使用してオブジェクトを検索
            dino = GameObject.FindGameObjectWithTag("Dino").GetComponent<Dino>();

            // 再生成フラグをfalseに
            dinoSpawn.dinoSpawnFlg = false;
        }

        // Dinoが出現するまでの残り時間計算
        dinoTextTimer = dino.dinoIntarval - dino.dinoTimer;

        // テキストを出す処理
        ActiveText();
    }

    // 点滅のコルーチン
    IEnumerator Blink()
    {
        // 現在の透明度を取得
        float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1.0f);

        // SpriteRendererの透明度を更新
        dinoText.color = new Color(dinoText.color.r, dinoText.color.g, dinoText.color.b, alpha);

        // 0.02秒待つ
        yield return new WaitForSeconds(0.02f);
    }

    // テキストを出す処理
    private void ActiveText()
    {
        // 移動までのクールタイムを超えたら
        if (dino.dinoTimer >= dino.dinoIntarval)
        {
            // テキストを出す
            dinoText.gameObject.SetActive(true);

            // Dino襲来テキストを表示
            dinoText.text = "Dino襲来！！";

            // コルーチンを開始
            StartCoroutine(Blink());
        }
        // Dinoが移動する20秒前
        else if (dinoTextTimer < dinoTextIntarval && dino.dinoTimer < dino.dinoIntarval)
        {
            // テキストを出す
            dinoText.gameObject.SetActive(true);

            // Dino襲来までのテキストを表示
            dinoText.text = "Dino襲来まであと" + "\n" + (dino.dinoIntarval - dino.dinoTimer).ToString("N0") + "秒";
        }
        else
        {
            // テキストを消す
            dinoText.gameObject.SetActive(false);

            // 透明度を元に戻す
            dinoText.color = new Color(dinoText.color.r, dinoText.color.g, dinoText.color.b, 1.0f);
        }
    }
}
