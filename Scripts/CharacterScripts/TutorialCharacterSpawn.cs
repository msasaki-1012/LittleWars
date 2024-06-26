using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCharacterSpawn : CharacterSpawn
{
    private Castle castle;                  // 味方の城
    [SerializeField]
    private AudioClip buySE;                // キャラクター購入時の効果音
    AudioSource audioSource;                // オーディオソース
    private WaveManager waveManager;        // ウェーブマネージャー
    private StageManager stageManager;      // ステージマネージャー
    private Coin coin;                      // コイン

    private float tutorialBGCoin = 50.0f;   // BGの消費コイン
    private bool timerFlg = false;          // タイマーを初期化したかどうかのフラグ

    [SerializeField]
    private Slider sliderBG;                // BGのクールタイム用スライダー

    [SerializeField]
    private TextMeshProUGUI bgPriceText;    // BGの値段用テキスト
    // Start is called before the first frame update
    void Start()
    {
        // オーディオソースを取得
        audioSource = GetComponent<AudioSource>();

        // ウェーブマネージャーを取得
        waveManager = GetComponent<WaveManager>();

        // ステージマネージャーを取得
        stageManager = GetComponent<StageManager>();

        // コインの取得
        coin = GetComponent<Coin>();

        // クールタイムの設定
        bgTimer = bgIntarval;

        // スライダーを一度アクティブにする
        sliderBG.gameObject.SetActive(true);

        // スライダーを取得
        sliderBG = GameObject.Find("SliderBG").GetComponent<Slider>();

        // スライダーを消す
        sliderBG.gameObject.SetActive(false);

        // 味方側の城を取得
        castle = GameObject.Find("BaseBlue").GetComponent<Castle>();

        // 値段用テキストを取得
        bgPriceText = GameObject.Find("BGPriceText").GetComponent<TextMeshProUGUI>();

        bgPriceText.text = tutorialBGCoin.ToString("N0") + "G";
    }

    // Update is called once per frame
    void Update()
    {
        // タイマー加算処理
        bgTimer += Time.deltaTime;

        // 勝利したとき
        if (stageManager.winFlg == true)
        {
            timerFlg = false;
        }

        // タイマーを初期化していないとき
        if (timerFlg == false)
        {
            // 現在のウェーブが2または3のとき
            if (waveManager.currentWave == 2 || waveManager.currentWave == 3)
            {
                // キャラクターのクールタイムの初期化
                bgTimer = bgIntarval;
                timerFlg = true;
            }
        }

        // 生成クールタイムを超えたら
        if (bgTimer > sliderBG.maxValue)
        {
            // それ以上大きくならないようにする
            bgTimer = bgIntarval;

            // 値段を表示する
            bgPriceText.gameObject.SetActive(true);

            // クールタイムのスライダーを非表示にする
            sliderBG.gameObject.SetActive(false);
        }

        // スライダーにタイマーの値を入れる
        sliderBG.value = bgTimer;
    }

    // ボタンをクリックしたときに呼ばれる関数
    public void ClickCharacterBG()
    {
        //// 最初のチュートリアルが終わった時
        //if (Tutorial.firstTutorialflg == true)
        //{
            //キャラクターのクールタイムが終わっていたら
            if (bgTimer >= bgIntarval)
            {
                // 所持しているコインが消費するコインより多い時
                if (coin.coinCount >= tutorialBGCoin)
                {
                    //コインを消費する
                    coin.UseCoin(tutorialBGCoin);

                    // キャラクター購入時の効果音再生
                    audioSource.PlayOneShot(buySE);

                    //キャラクターを生成する
                    Instantiate(bg, characterSpawnPos.position, Quaternion.identity);

                    // キャラクターの数を加算する
                    characterCount += 1;

                    // 値段を非表示にする
                    bgPriceText.gameObject.SetActive(false);

                    // クールタイムのスライダーを表示する
                    sliderBG.gameObject.SetActive(true);

                    bgTimer = 0;
                }
            }
        //}
    }
}
