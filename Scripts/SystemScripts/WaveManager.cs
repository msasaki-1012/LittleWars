using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    private StageManager stageManager;      // ステージマネージャー

    private DinoSpawn dinoSpawn;            // Dinoの生成
    private DinoText dinoText;              // Dinoのテキスト

    public int currentWave { get; set; } = 1;   // 現在のウェーブ
    public int maxWave { get; set; } = 3;       // ウェーブの最大数
    public bool nextWaveFlg { get; set; } = false;  // 次のウェーブになった時に呼ぶフラグ
    private float blinkSpeed = 1.0f;        // 点滅の速さを調整するための変数
    [SerializeField]
    private Transform []waveTextPos;        // ウェーブテキストの表示位置
    private bool waveTextFlg = false;       // テキストが中央にあるかどうかのフラグ

    [SerializeField]
    private TextMeshProUGUI waveText;       // ウェーブのテキスト

    [SerializeField]
    private Button nextWaveButton;          // 次のウェーブに進むボタン

    private void Start()
    {
        // ウェーブの更新 
        UpdateWaveText();

        // ステージマネージャーを取得
        stageManager = GetComponent<StageManager>();

        // Dinoの生成を取得
        dinoSpawn = GetComponent<DinoSpawn>();

        // Dinoのテキストを取得
        dinoText = GetComponent<DinoText>();

        // 次のウェーブに進むボタンを一度アクティブにする
        nextWaveButton.gameObject.SetActive(true);

        // 次のウェーブに進むボタンを取得
        nextWaveButton = GameObject.Find("NextWaveButton").GetComponent<Button>();
        
        // 次のウェーブに進むボタンを消す
        nextWaveButton.gameObject.SetActive(false);

        // ウェーブのテキストをアクティブにする
        StartCoroutine(ActiveWaveTextCoroutine());
    }

    private void Update()
    {
        // 戦いに勝利したとき
        if (stageManager.winFlg == true && currentWave < maxWave)
        {
            // ウェーブのテキストを消す
            waveText.gameObject.SetActive(false);

            // テキストを移動させる
            waveText.gameObject.transform.position = waveTextPos[0].position;

            // Dinoのテキストを出すまでの時間をリセットする
            dinoText.dinoTextTimer = 0;

            // 次のウェーブに進むボタンをアクティブにする
            nextWaveButton.gameObject.SetActive(true);
        }

        // テキストが中央に出てきたとき
        if (waveTextFlg == true)
        {
            // コルーチンを開始
            StartCoroutine(Blink());
        }
        else
        {
            // 透明度を元に戻す
            waveText.color = new Color(waveText.color.r, waveText.color.g, waveText.color.b, 1.0f);

            // コルーチンを止める
            StopCoroutine(Blink());
        }
    }

    public void StartNextWave()
    {
        if (nextWaveFlg == false)
        {
            // 次のウェーブに進む処理
            StartCoroutine(NextWaveCoroutine());
        }
    }

    void UpdateWaveText()
    {
        // 表示する文字
        waveText.text = "Wave：" + currentWave + "/3";
    }

    IEnumerator NextWaveCoroutine()
    {
        // 次のウェーブに進んだかどうか
        nextWaveFlg = true;

        // 3秒待機
        yield return new WaitForSeconds(3f);

        nextWaveFlg = false;

        // 勝利フラグをfalseに
        stageManager.winFlg = false;

        // 次のウェーブに進むボタンを消す
        nextWaveButton.gameObject.SetActive(false);

        // ウェーブを進めてテキストを更新
        currentWave++;

        // ウェーブの更新
        UpdateWaveText();

        // 2秒待機
        yield return new WaitForSeconds(2f);

        // ウェーブのテキストをアクティブにする
        StartCoroutine(ActiveWaveTextCoroutine());
    }

    IEnumerator ActiveWaveTextCoroutine()
    {
        // ウェーブのテキストを出す
        waveText.gameObject.SetActive(true);

        // テキストの点滅フラグをtrueに
        waveTextFlg = true;

        // 今いるシーンがタイトルのとき
        if (SceneManager.GetActiveScene().name != "Title" || SceneManager.GetActiveScene().name != "Tutorial")
        {
            // Dinoを生成する
            dinoSpawn.SpawnDino();
        }

        // 3秒待機してテキストをクリア
        yield return new WaitForSeconds(3f);

        // テキストを移動させる
        waveText.gameObject.transform.position = waveTextPos[1].position;

        // テキストの点滅フラグをfalseに
        waveTextFlg = false;
    }

    // 点滅のコルーチン
    IEnumerator Blink()
    {
        // 現在の透明度を取得
        float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1.0f);

        // SpriteRendererの透明度を更新
        waveText.color = new Color(waveText.color.r, waveText.color.g, waveText.color.b, alpha);

        // 0.02秒待つ
        yield return new WaitForSeconds(0.02f);
    }
}