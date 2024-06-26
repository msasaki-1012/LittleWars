using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Spawn : CharacterSpawn
{
    [SerializeField]
    private AudioClip buySE;                // キャラクター購入時の効果音
    AudioSource audioSource;                // オーディオソース
    private WaveManager waveManager;        // ウェーブマネージャー
    private StageManager stageManager;      // ステージマネージャー
    private Coin coin;                      // コイン

    private float bgCoin = 50.0f;           // BGの消費コイン
    private float bsCoin = 150.0f;          // BSの消費コイン
    private float baCoin = 250.0f;          // BAの消費コイン
    private float bwCoin = 500.0f;          // BWの消費コイン

    private float bguCoin = 500.0f;         // BGの消費コイン
    private float bsuCoin = 1000.0f;        // BSの消費コイン
    private float bauCoin = 1200.0f;        // BAの消費コイン
    private float bwuCoin = 1500.0f;        // BWの消費コイン
    private bool timerFlg = false;          // タイマーを初期化したかどうかのフラグ
    public float blinkSpeed = 1.0f;         // 点滅の速さを調整するための変数
    private float stage2Coin = 1.5f;        // ステージ1,3以外のコインの倍率
    private float stage3Coin = 2.0f;        // ステージ3のコインの倍率

    [Header("スライダー")]
    [SerializeField]
    private Slider sliderBG;                // BGのクールタイム用スライダー
    [SerializeField]
    private Slider sliderBS;                // BSのクールタイム用スライダー
    [SerializeField]
    private Slider sliderBA;                // BAのクールタイム用スライダー
    [SerializeField]
    private Slider sliderBW;                // BWのクールタイム用スライダー

    [SerializeField]
    private Slider sliderBGU;               // BGUのクールタイム用スライダー
    [SerializeField]                             
    private Slider sliderBSU;               // BSUのクールタイム用スライダー
    [SerializeField]                             
    private Slider sliderBAU;               // BAUのクールタイム用スライダー
    [SerializeField]                             
    private Slider sliderBWU;               // BWUのクールタイム用スライダー

    [Header("値段用テキスト")]
    [SerializeField]
    private TextMeshProUGUI bgPriceText;    // BGの値段用テキスト
    [SerializeField]
    private TextMeshProUGUI bsPriceText;    // BSの値段用テキスト
    [SerializeField]
    private TextMeshProUGUI baPriceText;    // BAの値段用テキスト
    [SerializeField]
    private TextMeshProUGUI bwPriceText;    // BWの値段用テキスト

    [SerializeField]
    private TextMeshProUGUI bguPriceText;   // BGUの値段用テキスト
    [SerializeField]                           
    private TextMeshProUGUI bsuPriceText;   // BSUの値段用テキスト
    [SerializeField]                            
    private TextMeshProUGUI bauPriceText;   // BAUの値段用テキスト
    [SerializeField]                            
    private TextMeshProUGUI bwuPriceText;   // BWUの値段用テキスト

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
        bsTimer = bsIntarval;
        baTimer = baIntarval;
        bguTimer = bguIntarval;
        bsuTimer = bsuIntarval;
        bauTimer = bauIntarval;

        // スライダーを一度アクティブにする
        sliderBG.gameObject.SetActive(true);
        sliderBS.gameObject.SetActive(true);
        sliderBA.gameObject.SetActive(true);
        sliderBW.gameObject.SetActive(true);

        sliderBGU.gameObject.SetActive(true);
        sliderBSU.gameObject.SetActive(true);
        sliderBAU.gameObject.SetActive(true);
        sliderBWU.gameObject.SetActive(true);

        // スライダーを取得
        sliderBG = GameObject.Find("SliderBG").GetComponent<Slider>();
        sliderBS = GameObject.Find("SliderBS").GetComponent<Slider>();
        sliderBA = GameObject.Find("SliderBA").GetComponent<Slider>();
        sliderBW = GameObject.Find("SliderBW").GetComponent<Slider>();

        sliderBGU = GameObject.Find("SliderBGU").GetComponent<Slider>();
        sliderBSU = GameObject.Find("SliderBSU").GetComponent<Slider>();
        sliderBAU = GameObject.Find("SliderBAU").GetComponent<Slider>();
        sliderBWU = GameObject.Find("SliderBWU").GetComponent<Slider>();

        // スライダーを消す
        sliderBG.gameObject.SetActive(false);
        sliderBS.gameObject.SetActive(false);
        sliderBA.gameObject.SetActive(false);

        sliderBGU.gameObject.SetActive(false);
        sliderBSU.gameObject.SetActive(false);
        sliderBAU.gameObject.SetActive(false);

        // 今いるシーンがステージ2のとき
        if (SceneManager.GetActiveScene().name != "Stage1" && SceneManager.GetActiveScene().name != "Stage3")
        {
            // 消費コインの変更
            bgCoin *= stage2Coin;
            bsCoin *= stage2Coin;
            baCoin *= stage2Coin;
            bwCoin *= stage2Coin;

            bguCoin *= stage2Coin;
            bsuCoin *= stage2Coin;
            bauCoin *= stage2Coin;
            bwuCoin *= stage2Coin;
        }

        // 今いるシーンがステージ3のとき
        if (SceneManager.GetActiveScene().name == "Stage3")
        {
            // 消費コインの変更
            bgCoin *= stage3Coin;
            bsCoin *= stage3Coin;
            baCoin *= stage3Coin;
            bwCoin *= stage3Coin;

            bguCoin *= stage3Coin;
            bsuCoin *= stage3Coin;
            bauCoin *= stage3Coin;
            bwuCoin *= stage3Coin;
        }

        // 値段用テキストを取得
        bgPriceText = GameObject.Find("BGPriceText").GetComponent<TextMeshProUGUI>();
        bsPriceText = GameObject.Find("BSPriceText").GetComponent<TextMeshProUGUI>();
        baPriceText = GameObject.Find("BAPriceText").GetComponent<TextMeshProUGUI>();
        bwPriceText = GameObject.Find("BWPriceText").GetComponent<TextMeshProUGUI>();

        bguPriceText = GameObject.Find("BGUPriceText").GetComponent<TextMeshProUGUI>();
        bsuPriceText = GameObject.Find("BSUPriceText").GetComponent<TextMeshProUGUI>();
        bauPriceText = GameObject.Find("BAUPriceText").GetComponent<TextMeshProUGUI>();
        bwuPriceText = GameObject.Find("BWUPriceText").GetComponent<TextMeshProUGUI>();

        bgPriceText.text = bgCoin.ToString("N0") + "G";
        bsPriceText.text = bsCoin.ToString("N0") + "G";
        baPriceText.text = baCoin.ToString("N0") + "G";
        bwPriceText.text = bwCoin.ToString("N0") + "G";

        bguPriceText.text = bguCoin.ToString("N0") + "G";
        bsuPriceText.text = bsuCoin.ToString("N0") + "G";
        bauPriceText.text = bauCoin.ToString("N0") + "G";
        bwuPriceText.text = bwuCoin.ToString("N0") + "G";

        bwPriceText.gameObject.SetActive(false);
        bwuPriceText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // タイマー加算処理
        bgTimer += Time.deltaTime;
        bsTimer += Time.deltaTime;
        baTimer += Time.deltaTime;

        bguTimer += Time.deltaTime;
        bsuTimer += Time.deltaTime;
        bauTimer += Time.deltaTime;

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
                bsTimer = bsIntarval;
                baTimer = baIntarval;

                bguTimer = bguIntarval;
                bsuTimer = bsuIntarval;
                bauTimer = bauIntarval;
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

        // 生成クールタイムを超えたら
        if (bsTimer > sliderBS.maxValue)
        {
            // それ以上大きくならないようにする
            bsTimer = bsIntarval;

            // 値段を表示する
            bsPriceText.gameObject.SetActive(true);

            // クールタイムのスライダーを非表示にする
            sliderBS.gameObject.SetActive(false);
        }

        // 生成クールタイムを超えたら
        if (baTimer > sliderBA.maxValue)
        {
            // それ以上大きくならないようにする
            baTimer = baIntarval;

            // 値段を表示する
            baPriceText.gameObject.SetActive(true);

            // クールタイムのスライダーを非表示にする
            sliderBA.gameObject.SetActive(false);
        }

        // キャラクターの死亡数が最大数を超えたら
        if (sliderBW.maxValue <= sliderBW.value)
        {
            // それ以上大きくならないようにする
            sliderBW.value = sliderBW.maxValue;

            // 値段を表示する
            bwPriceText.gameObject.SetActive(true);

            // クールタイムのスライダーを非表示にする
            sliderBW.gameObject.SetActive(false);
        }
        else
        {
            // 値段を非表示にする
            bwPriceText.gameObject.SetActive(false);

            // クールタイムのスライダーを表示する
            sliderBW.gameObject.SetActive(true);
        }

        // 生成クールタイムを超えたら
        if (bguTimer > sliderBGU.maxValue)
        {
            // それ以上大きくならないようにする
            bguTimer = bguIntarval;

            // 値段を表示する
            bguPriceText.gameObject.SetActive(true);

            // クールタイムのスライダーを非表示にする
            sliderBGU.gameObject.SetActive(false);
        }

        // 生成クールタイムを超えたら
        if (bsuTimer > sliderBSU.maxValue)
        {
            // それ以上大きくならないようにする
            bsuTimer = bsuIntarval;

            // 値段を表示する
            bsuPriceText.gameObject.SetActive(true);

            // クールタイムのスライダーを非表示にする
            sliderBSU.gameObject.SetActive(false);
        }

        // 生成クールタイムを超えたら
        if (bauTimer > sliderBAU.maxValue)
        {
            // それ以上大きくならないようにする
            bauTimer = bauIntarval;

            // 値段を表示する
            bauPriceText.gameObject.SetActive(true);

            // クールタイムのスライダーを非表示にする
            sliderBAU.gameObject.SetActive(false);
        }

        // キャラクターの死亡数が最大数を超えたら
        if (sliderBWU.maxValue <= sliderBWU.value)
        {
            // それ以上大きくならないようにする
            sliderBWU.value = sliderBWU.maxValue;

            // 値段を表示する
            bwuPriceText.gameObject.SetActive(true);

            // クールタイムのスライダーを非表示にする
            sliderBWU.gameObject.SetActive(false);
        }
        else
        {
            // 値段を非表示にする
            bwuPriceText.gameObject.SetActive(false);

            // クールタイムのスライダーを表示する
            sliderBWU.gameObject.SetActive(true);
        }

        // スライダーにタイマーの値を入れる
        sliderBG.value = bgTimer;
        sliderBS.value = bsTimer;
        sliderBA.value = baTimer;

        sliderBGU.value = bguTimer;
        sliderBSU.value = bsuTimer;
        sliderBAU.value = bauTimer;

        // スライダーに味方の死亡数を入れる
        sliderBW.value = deathCharacterCount;
        sliderBWU.value = deathCharacterCount;
    }

    // ボタンをクリックしたときに呼ばれる関数
    public void ClickCharacterBG()
    {
        //キャラクターのクールタイムが終わっていたら
        if (bgTimer >= bgIntarval)
        {
            // 所持しているコインが消費するコインより多い時
            if (coin.coinCount >= bgCoin)
            {
                //コインを消費する
                coin.UseCoin(bgCoin);

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
    }

    public void ClickCharacterBS()
    {
        //キャラクターのクールタイムが終わっていたら
        if (bsTimer >= bsIntarval)
        {
            // 所持しているコインが消費するコインより多い時
            if (coin.coinCount >= bsCoin)
            {
                //コインを消費する
                coin.UseCoin(bsCoin);

                // キャラクター購入時の効果音再生
                audioSource.PlayOneShot(buySE);

                //キャラクターを生成する
                Instantiate(bs, characterSpawnPos.position, Quaternion.identity);

                // キャラクターの数を加算する
                characterCount += 1;

                // 値段を非表示にする
                bsPriceText.gameObject.SetActive(false);

                // クールタイムのスライダーを表示する
                sliderBS.gameObject.SetActive(true);

                bsTimer = 0;
            }
        }
    }

    public void ClickCharacterBA()
    {
        //キャラクターのクールタイムが終わっていたら
        if (baTimer >= baIntarval)
        {
            // 所持しているコインが消費するコインより多い時
            if (coin.coinCount >= baCoin)
            {
                //コインを消費する
                coin.UseCoin(baCoin);

                // キャラクター購入時の効果音再生
                audioSource.PlayOneShot(buySE);

                //キャラクターを生成する
                Instantiate(ba, characterSpawnPos.position, Quaternion.identity);

                // キャラクターの数を加算する
                characterCount += 1;

                // 値段を非表示にする
                baPriceText.gameObject.SetActive(false);

                // クールタイムのスライダーを表示する
                sliderBA.gameObject.SetActive(true);

                baTimer = 0;
            }
        }
    }
    public void ClickCharacterBW()
    {
        // bwがいないとき
        if (bwFlg == false && bwuFlg == false)
        {
            // キャラクターの死亡数が最大数を超えたら
            if (deathCharacterCount >= sliderBW.maxValue)
            {
                // 所持しているコインが消費するコインより多い時
                if (coin.coinCount >= bwCoin)
                {
                    //コインを消費する
                    coin.UseCoin(bwCoin);

                    // キャラクター購入時の効果音再生
                    audioSource.PlayOneShot(buySE);

                    //キャラクターを生成する
                    Instantiate(bw, bwSpawnPos.position, Quaternion.identity);

                    // キャラクターの数を加算する
                    characterCount += 1;

                    // キャラクターの死亡数をリセット
                    deathCharacterCount = 0;

                    // bwの存在フラグをtrueに
                    bwFlg = true;
                }
            }
        }
    }

    public void ClickCharacterBGU()
    {
        //キャラクターのクールタイムが終わっていたら
        if (bguTimer >= bguIntarval)
        {
            // 所持しているコインが消費するコインより多い時
            if (coin.coinCount >= bguCoin)
            {
                //コインを消費する
                coin.UseCoin(bguCoin);

                // キャラクター購入時の効果音再生
                audioSource.PlayOneShot(buySE);

                //キャラクターを生成する
                Instantiate(bgu, characterSpawnPos.position, Quaternion.identity);

                // キャラクターの数を加算する
                characterCount += 1;

                // 値段を非表示にする
                bguPriceText.gameObject.SetActive(false);

                // クールタイムのスライダーを表示する
                sliderBGU.gameObject.SetActive(true);

                bguTimer = 0;
            }
        }
    }

    public void ClickCharacterBSU()
    {
        //キャラクターのクールタイムが終わっていたら
        if (bsuTimer >= bsuIntarval)
        {
            // 所持しているコインが消費するコインより多い時
            if (coin.coinCount >= bsuCoin)
            {
                //コインを消費する
                coin.UseCoin(bsuCoin);

                // キャラクター購入時の効果音再生
                audioSource.PlayOneShot(buySE);

                //キャラクターを生成する
                Instantiate(bsu, characterSpawnPos.position, Quaternion.identity);

                // キャラクターの数を加算する
                characterCount += 1;

                // 値段を非表示にする
                bsuPriceText.gameObject.SetActive(false);

                // クールタイムのスライダーを表示する
                sliderBSU.gameObject.SetActive(true);

                bsuTimer = 0;
            }
        }
    }

    public void ClickCharacterBAU()
    {
        //キャラクターのクールタイムが終わっていたら
        if (bauTimer >= bauIntarval)
        {
            // 所持しているコインが消費するコインより多い時
            if (coin.coinCount >= bauCoin)
            {
                //コインを消費する
                coin.UseCoin(bauCoin);

                // キャラクター購入時の効果音再生
                audioSource.PlayOneShot(buySE);

                //キャラクターを生成する
                Instantiate(bau, characterSpawnPos.position, Quaternion.identity);

                // キャラクターの数を加算する
                characterCount += 1;

                // 値段を非表示にする
                bauPriceText.gameObject.SetActive(false);

                // クールタイムのスライダーを表示する
                sliderBAU.gameObject.SetActive(true);

                bauTimer = 0;
            }
        }
    }
    public void ClickCharacterBWU()
    {
        // bwがいないとき
        if (bwFlg == false && bwuFlg == false)
        {
            // キャラクターの死亡数が最大数を超えたら
            if (deathCharacterCount >= sliderBWU.maxValue)
            {
                // 所持しているコインが消費するコインより多い時
                if (coin.coinCount >= bwuCoin)
                {
                    //コインを消費する
                    coin.UseCoin(bwuCoin);

                    // キャラクター購入時の効果音再生
                    audioSource.PlayOneShot(buySE);

                    //キャラクターを生成する
                    Instantiate(bwu, bwSpawnPos.position, Quaternion.identity);

                    // キャラクターの数を加算する
                    characterCount += 1;

                    // キャラクターの死亡数をリセット
                    deathCharacterCount = 0;

                    // bwの存在フラグをtrueに
                    bwuFlg = true;
                }
            }
        }
    }
}
