using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    protected GameObject rg;                // キャラクターRG
    [SerializeField]                        
    protected GameObject rs;                // キャラクターRS
    [SerializeField]                        
    protected GameObject ra;                // キャラクターRA
    [SerializeField]                        
    protected GameObject rw;                // キャラクターRW
    [SerializeField]                        
    protected GameObject rgu;               // キャラクターRGU
    [SerializeField]                        
    protected GameObject rsu;               // キャラクターRSU
    [SerializeField]                        
    protected GameObject rau;               // キャラクターRAU
    [SerializeField]                        
    protected GameObject rwu;               // キャラクターRWU

    [SerializeField]
    protected EnemyCastle[] enemyCastles;   // 敵の城

    [SerializeField]
    protected Transform enemySpawnPos;      // キャラクター出現位置
    [SerializeField]
    protected Transform rwSpawnPos;         // rwの出現位置
    [SerializeField]
    protected Transform enemySpawnPos2;     // キャラクター出現位置
    [SerializeField]
    protected Transform rwSpawnPos2;        // rwの出現位置
    [SerializeField]
    protected Transform enemySpawnPos3;     // キャラクター出現位置
    [SerializeField]
    protected Transform rwSpawnPos3;        // rwの出現位置

    private WaveManager waveManager;        // ウェーブマネージャー
    private StageManager stageManager;      // ステージマネージャー

    [SerializeField]
    private GameObject rwBack;              // rwのテキスト背景
    [SerializeField]
    private TextMeshProUGUI rwText;         // rwのテキスト
    [SerializeField]
    private TextMeshProUGUI rwuText;        // rwuのテキスト

    protected float rgTimer;                // RGの計算用タイマー
    protected float rsTimer;                // RSの計算用タイマー
    protected float raTimer;                // RAの計算用タイマー
    protected float rguTimer;               // RGの計算用タイマー
    protected float rsuTimer;               // RSの計算用タイマー
    protected float rauTimer;               // RAの計算用タイマー
    private bool timerFlg = false;          // タイマーを初期化したかどうかのフラグ
    public bool rwFlg { get; set; } = false;    // RWの生存フラグ
    public bool rguFlg { get; set; } = false;   // RGUの生存フラグ
    public bool rsuFlg { get; set; } = false;   // RSUの生存フラグ
    public bool rauFlg { get; set; } = false;   // RAUの生存フラグ
    public bool rwuFlg { get; set; } = false;   // RWUの生存フラグ

    protected float rgIntarval = 5.0f;       // RGのクールタイム
    protected float rsIntarval = 8.0f;       // RSのクールタイム
    protected float raIntarval = 12.0f;      // RAのクールタイム
    protected float rguIntarval = 12.0f;     // RGUのクールタイム
    protected float rsuIntarval = 14.0f;     // RSUのクールタイム
    protected float rauIntarval = 18.0f;     // RAUのクールタイム

    public int enemyCount { get; set; } = 0; // 敵の数
    [SerializeField]
    protected int enemyCountMax = 10;               // 敵の最大数
    public int deathCharacterCount { get; set; }    // 死亡した敵の数
    public int deathCharacterCountMax { get; set; } = 10;        // 死亡した敵の数の最大数
    private int deathCharacterCountText = 0;        // テキスト用死亡数 
    protected int deathCharacterCountMaxRWU = 25;   // RWU用の死亡した味方の数の最大数
    protected int deathCharacterCountTextRWU = 0;   // RWU用のテキスト用死亡数

    // Start is called before the first frame update
    void Start()
    {
        // ウェーブマネージャーを取得
        waveManager = GetComponent<WaveManager>();

        // ステージマネージャーを取得
        stageManager = GetComponent<StageManager>();

        // 最初から出てくるようになる
        //rgTimer += rgIntarval;
        //rsTimer += rsIntarval;
        //raTimer += raIntarval;

        // 敵の城を取得
        enemyCastles[0] = GameObject.Find("BaseRed").GetComponent<EnemyCastle>();

        // 今いるシーンがタイトル、ステージセレクト画面以外のとき
        if (SceneManager.GetActiveScene().name != "Title" && SceneManager.GetActiveScene().name != "StageSelect")
        {
            // 城を出す
            enemyCastles[1].gameObject.SetActive(true);
            enemyCastles[2].gameObject.SetActive(true);

            // 敵の城を取得
            enemyCastles[1] = GameObject.Find("BaseRed2").GetComponent<EnemyCastle>();
            enemyCastles[2] = GameObject.Find("BaseRed3").GetComponent<EnemyCastle>();

            // 城を消す
            enemyCastles[1].gameObject.SetActive(false);
            enemyCastles[2].gameObject.SetActive(false);
        }

        // 今いるシーンがステージ34,5,6のときのとき
        if (SceneManager.GetActiveScene().name == "Stage3" || SceneManager.GetActiveScene().name == "Stage4" || SceneManager.GetActiveScene().name == "Stage5" || SceneManager.GetActiveScene().name == "Stage6")
        {
            // イメージを取得
            rwText = GameObject.Find("RWText").GetComponent<TextMeshProUGUI>();
        }

        // 今いるシーンがステージ7のとき
        if (SceneManager.GetActiveScene().name == "Stage7")
        {
            // イメージを取得
            rwuText = GameObject.Find("RWUText").GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // タイマー加算処理
        rgTimer += Time.deltaTime;
        rsTimer += Time.deltaTime;
        raTimer += Time.deltaTime;

        rguTimer += Time.deltaTime;
        rsuTimer += Time.deltaTime;
        rauTimer += Time.deltaTime;

        // 今いるシーンがタイトル、ステージセレクト画面以外のとき
        if (SceneManager.GetActiveScene().name != "Title" || SceneManager.GetActiveScene().name != "StageSelect")
        {
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
                    // 敵のクールタイムの初期化
                    rgTimer = 0;
                    rsTimer = 0;
                    raTimer = 0;

                    rguTimer = 0;
                    rsuTimer = 0;
                    rauTimer = 0;

                    // 敵の死亡数を初期化
                    deathCharacterCount = 0;
                    timerFlg = true;
                }
            }
        }

        // 今いるシーンがステージ1のとき
        if (SceneManager.GetActiveScene().name == "Stage1")
        {   
            // 敵を出現させる
            SpownEnemy();
        }

        // 今いるシーンがステージ2のとき
        if (SceneManager.GetActiveScene().name == "Stage2")
        {  
            // 敵を出現させる
            SpownEnemy2();
        }

        // 今いるシーンがステージ3のとき
        if (SceneManager.GetActiveScene().name == "Stage3")
        {
            // 敵の死亡数が最大数を超えたら
            if (deathCharacterCount > deathCharacterCountMax)
            {
                deathCharacterCount = deathCharacterCountMax;
            }

            // テキスト表示用の値を計算
            deathCharacterCountText = deathCharacterCountMax - deathCharacterCount;

            // テキストに敵の死亡数を入れる
            rwText.text = "出現まで" + "\n" + "あと" + "\n" + deathCharacterCountText.ToString("N0") + "体" + "\n" + "死亡";

            // 敵を出現させる
            SpownEnemy3();
        }

        // 今いるシーンがステージ4のとき
        if (SceneManager.GetActiveScene().name == "Stage4")
        {
            // 敵を出現させる
            SpownEnemy4();
        }

        // 今いるシーンがステージ5のとき
        if (SceneManager.GetActiveScene().name == "Stage5")
        {
            // 敵を出現させる
            SpownEnemy5();
        }

        // 今いるシーンがステージ6のとき
        if (SceneManager.GetActiveScene().name == "Stage6")
        {
            // 敵を出現させる
            SpownEnemy6();
        }

        // 今いるシーンがステージ7のとき
        if (SceneManager.GetActiveScene().name == "Stage7")
        {
            // 敵の死亡数が最大数を超えたら
            if (deathCharacterCount > deathCharacterCountMaxRWU)
            {
                deathCharacterCount = deathCharacterCountMaxRWU;
            }

            // 敵を出現させる
            SpownEnemy7();

            // テキスト表示用の値を計算
            deathCharacterCountTextRWU = deathCharacterCountMaxRWU - deathCharacterCount;

            // テキストに敵の死亡数を入れる
            rwuText.text = "出現まで" + "\n" + "あと" + "\n" + deathCharacterCountTextRWU.ToString("N0") + "体" + "\n" + "死亡";
        }

        // 今いるシーンがステージ3,4,5,6のとき
        if (SceneManager.GetActiveScene().name == "Stage4" || SceneManager.GetActiveScene().name == "Stage5" || SceneManager.GetActiveScene().name == "Stage6")
        {
            // 現在のウェーブが3のとき
            if (waveManager.currentWave == 3)
            {
                // rwの背景、テキストを出す
                rwBack.gameObject.SetActive(true);
            }
            else
            {
                // rwの背景、テキストを消す
                rwBack.gameObject.SetActive(false);
            }

            // 敵の死亡数が最大数を超えたら
            if (deathCharacterCount > deathCharacterCountMax)
            {
                deathCharacterCount = deathCharacterCountMax;
            }

            // テキスト表示用の値を計算
            deathCharacterCountText = deathCharacterCountMax - deathCharacterCount;

            // テキストに敵の死亡数を入れる
            rwText.text = "出現まで" + "\n" + "あと" + "\n" + deathCharacterCountText.ToString("N0") + "体" + "\n" + "死亡";
        }
    }

    // エネミー生成処理
    private void SpownEnemy()
    {
        // 1ウェーブ目の処理
        {
            // 現在のウェーブが1だったら
            if (waveManager.currentWave == 1)
            {
                // 敵の出現クールタイムの調整
                rgIntarval = 5.0f;
                
                // 城の体力が無くなっていたら
                if (enemyCastles[0].die == true)
                {
                    return;
                }

                // 敵の数が上限より下の時
                if (enemyCount < enemyCountMax)
                {
                    //敵(rg)の生成クールタイムが終わったら
                    if (rgTimer >= rgIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rg, enemySpawnPos.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rgTimer = 0;
                    }
                }
            }
        }

        // 2ウェーブ目の処理
        {
            // 現在のウェーブが2かつ、勝利していないとき
            if (waveManager.currentWave == 2 && stageManager.winFlg == false)
            {
                // 敵の出現クールタイムの調整
                rgIntarval = 6.0f;
                rsIntarval = 5.0f;

                // 城を出す
                enemyCastles[1].gameObject.SetActive(true);
                // 城の体力が無くなっていたら
                if (enemyCastles[1].die == true)
                {
                    return;
                }

                // 敵の数が上限より下の時
                if (enemyCount < enemyCountMax)
                {
                    //敵(rg)の生成クールタイムが終わったら
                    if (rgTimer >= rgIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rg, enemySpawnPos2.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rgTimer = 0;
                    }

                    //敵(rs)の生成クールタイムが終わったら
                    if (rsTimer >= rsIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rs, enemySpawnPos2.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rsTimer = 0;
                    }
                }
            }
        }

        // 3ウェーブ目の処理
        {
            // 現在のウェーブが3かつ、勝利していないとき
            if (waveManager.currentWave == 3 && stageManager.winFlg == false)
            {
                // 敵の出現クールタイムの調整
                rgIntarval = 4.0f;
                rsIntarval = 5.0f;
              
                // 城を出す
                enemyCastles[2].gameObject.SetActive(true);

                // 城の体力が無くなっていたら
                if (enemyCastles[2].die == true)
                {
                    return;
                }

                // 敵の数が上限より下の時
                if (enemyCount < enemyCountMax)
                {
                    //敵(rg)の生成クールタイムが終わったら
                    if (rgTimer >= rgIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rg, enemySpawnPos3.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rgTimer = 0;
                    }

                    //敵(rs)の生成クールタイムが終わったら
                    if (rsTimer >= rsIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rs, enemySpawnPos3.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rsTimer = 0;
                    }
                }
            }
        }
    }

    private void SpownEnemy2()
    {
        // 1ウェーブ目の処理
        {
            if (waveManager.currentWave == 1)
            {
                // 敵の出現クールタイムの調整
                rgIntarval = 5.0f;
                raIntarval = 7.0f;

                // 城の体力が無くなっていたら
                if (enemyCastles[0].die == true)
                {
                    return;
                }

                // 敵の数が上限より下の時
                if (enemyCount < enemyCountMax)
                {
                    //敵(rg)の生成クールタイムが終わったら
                    if (rgTimer >= rgIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rg, enemySpawnPos.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rgTimer = 0;
                    }

                    // 敵(ra)の生成クールタイムが終わったら
                    if (raTimer >= raIntarval)
                    {
                        // 敵を生成する
                        Instantiate(ra, enemySpawnPos.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        raTimer = 0;
                    }
                }
            }
        }

        // 2ウェーブ目の処理
        {
            if (waveManager.currentWave == 2 && stageManager.winFlg == false)
            {
                // 敵の出現クールタイムの調整
                rgIntarval = 9.0f;
                rsIntarval = 10.0f;
                raIntarval = 7.0f;

                // 城を出す
                enemyCastles[1].gameObject.SetActive(true);

                // 城の体力が無くなっていたら
                if (enemyCastles[1].die == true)
                {
                    return;
                }

                // 敵の数が上限より下の時
                if (enemyCount < enemyCountMax)
                {
                    //敵(rg)の生成クールタイムが終わったら
                    if (rgTimer >= rgIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rg, enemySpawnPos2.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rgTimer = 0;
                    }

                    //敵(rs)の生成クールタイムが終わったら
                    if (rsTimer >= rsIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rs, enemySpawnPos2.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rsTimer = 0;
                    }

                    // 敵(ra)の生成クールタイムが終わったら
                    if (raTimer >= raIntarval)
                    {
                        // 敵を生成する
                        Instantiate(ra, enemySpawnPos2.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        raTimer = 0;
                    }
                }
            }
        }

        // 3ウェーブ目の処理
        {
            if (waveManager.currentWave == 3 && stageManager.winFlg == false)
            {
                // 敵の出現クールタイムの調整
                rgIntarval = 5.0f;
                rsIntarval = 12.0f;
                raIntarval = 6.0f;

                // 城を出す
                enemyCastles[2].gameObject.SetActive(true);

                // 城の体力が無くなっていたら
                if (enemyCastles[2].die == true)
                {
                    return;
                }

                // 敵の数が上限より下の時
                if (enemyCount < enemyCountMax)
                {
                    //敵(rg)の生成クールタイムが終わったら
                    if (rgTimer >= rgIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rg, enemySpawnPos3.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rgTimer = 0;
                    }

                    //敵(rs)の生成クールタイムが終わったら
                    if (rsTimer >= rsIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rs, enemySpawnPos3.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rsTimer = 0;
                    }
                    // 敵(ra)の生成クールタイムが終わったら
                    if (raTimer >= raIntarval)
                    {
                        // 敵を生成する
                        Instantiate(ra, enemySpawnPos3.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        raTimer = 0;
                    }
                }
            }
        }
    }

    private void SpownEnemy3()
    {
        // 1ウェーブ目の処理
        {
            if (waveManager.currentWave == 1)
            {
                // 敵の出現クールタイムの調整
                rgIntarval = 5.0f;

                // 城の体力が無くなっていたら
                if (enemyCastles[0].die == true)
                {
                    return;
                }

                // 敵の数が上限より下の時
                if (enemyCount < enemyCountMax)
                {
                    //敵(rg)の生成クールタイムが終わったら
                    if (rgTimer >= rgIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rg, enemySpawnPos.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rgTimer = 0;
                    }

                    //rwがいないとき
                    if (rwFlg == false)
                    {
                        // 敵(rw)の生成クールタイムが終わったら
                        if (deathCharacterCount >= deathCharacterCountMax)
                        {
                            // 敵を生成する
                            Instantiate(rw, rwSpawnPos.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;
                         
                            deathCharacterCount = 0;

                            // bwの存在フラグをtrueに
                            rwFlg = true;
                        }
                    }
                }
            }
        }

        // 2ウェーブ目の処理
        {
            if (waveManager.currentWave == 2 && stageManager.winFlg == false)
            {
                // 敵の出現クールタイムの調整
                rgIntarval = 6.0f;
                rsIntarval = 8.0f;
                deathCharacterCountMax = 12;

                // 城を出す
                enemyCastles[1].gameObject.SetActive(true);

                // 城の体力が無くなっていたら
                if (enemyCastles[1].die == true)
                {
                    return;
                }

                // 敵の数が上限より下の時
                if (enemyCount < enemyCountMax)
                {
                    //敵(rg)の生成クールタイムが終わったら
                    if (rgTimer >= rgIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rg, enemySpawnPos2.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rgTimer = 0;
                    }

                    //敵(rs)の生成クールタイムが終わったら
                    if (rsTimer >= rsIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rs, enemySpawnPos2.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rsTimer = 0;
                    }

                    //rwがいないとき
                    if (rwFlg == false)
                    {
                        // 敵(rw)の生成クールタイムが終わったら
                        //if (rwTimer >= rwIntarval)
                        if (deathCharacterCount >= deathCharacterCountMax)
                        {
                            // 敵を生成する
                            Instantiate(rw, rwSpawnPos2.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;
                        
                            deathCharacterCount = 0;

                            // bwの存在フラグをtrueに
                            rwFlg = true;
                        }
                    }
                }
            }
        }

        // 3ウェーブ目の処理
        {
            if (waveManager.currentWave == 3 && stageManager.winFlg == false)
            {
                // 敵の出現クールタイムの調整
                rgIntarval = 7.0f;
                rsIntarval = 9.0f;
                raIntarval = 12.0f;
                deathCharacterCountMax = 15;

                // 城を出す
                enemyCastles[2].gameObject.SetActive(true);

                // 城の体力が無くなっていたら
                if (enemyCastles[2].die == true)
                {
                    return;
                }

                // 敵の数が上限より下の時
                if (enemyCount < enemyCountMax)
                {
                    //敵(rg)の生成クールタイムが終わったら
                    if (rgTimer >= rgIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rg, enemySpawnPos3.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rgTimer = 0;
                    }

                    //敵(rs)の生成クールタイムが終わったら
                    if (rsTimer >= rsIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rs, enemySpawnPos3.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rsTimer = 0;
                    }
                    // 敵(ra)の生成クールタイムが終わったら
                    if (raTimer >= raIntarval)
                    {
                        // 敵を生成する
                        Instantiate(ra, enemySpawnPos3.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        raTimer = 0;
                    }

                    //rwがいないとき
                    if (rwFlg == false)
                    {
                        // キャラクターの死亡数が最大数を超えたら
                        if (deathCharacterCount >= deathCharacterCountMax)
                        {
                            // 敵を生成する
                            Instantiate(rw, rwSpawnPos3.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;

                            // 敵の死亡数をリセット
                            deathCharacterCount = 0;

                            // rwの存在フラグをtrueに
                            rwFlg = true;
                        }
                    }
                }
            }
        }
    }

    private void SpownEnemy4()
    {
        // 1ウェーブ目の処理
        {
            if (waveManager.currentWave == 1)
            {
                // 敵の出現クールタイムの調整
                rgIntarval = 8.0f;
                rsIntarval = 10.0f;
                deathCharacterCountMax = 15;

                // 城の体力が無くなっていたら
                if (enemyCastles[0].die == true)
                {
                    return;
                }

                // 敵の数が上限より下の時
                if (enemyCount < enemyCountMax)
                {
                    //敵(rg)の生成クールタイムが終わったら
                    if (rgTimer >= rgIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rg, enemySpawnPos.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rgTimer = 0;
                    }

                    //敵(rs)の生成クールタイムが終わったら
                    if (rsTimer >= rsIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rs, enemySpawnPos.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rsTimer = 0;
                    }

                    //rguがいないとき
                    if (rguFlg == false)
                    {
                        //敵(rgu)の生成クールタイムが終わったら
                        if (rguTimer >= rguIntarval)
                        {
                            // 敵を生成する
                            Instantiate(rgu, enemySpawnPos.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;
                            rguTimer = 0;

                            // rguの存在フラグをtrueに
                            rguFlg = true;
                        }
                    }
                }
            }
        }

        // 2ウェーブ目の処理
        {
            if (waveManager.currentWave == 2 && stageManager.winFlg == false)
            {
                // 敵の出現クールタイムの調整
                rgIntarval = 8.0f;
                rsIntarval = 10.0f;
                raIntarval = 15.0f;
                deathCharacterCountMax = 12;

                // 城を出す
                enemyCastles[1].gameObject.SetActive(true);

                // 城の体力が無くなっていたら
                if (enemyCastles[1].die == true)
                {
                    return;
                }

                // 敵の数が上限より下の時
                if (enemyCount < enemyCountMax)
                {
                    //敵(rg)の生成クールタイムが終わったら
                    if (rgTimer >= rgIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rg, enemySpawnPos2.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rgTimer = 0;
                    }

                    //敵(rs)の生成クールタイムが終わったら
                    if (rsTimer >= rsIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rs, enemySpawnPos2.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rsTimer = 0;
                    }

                    // 敵(ra)の生成クールタイムが終わったら
                    if (raTimer >= raIntarval)
                    {
                        // 敵を生成する
                        Instantiate(ra, enemySpawnPos2.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        raTimer = 0;
                    }

                    //rguがいないとき
                    if (rguFlg == false)
                    {
                        //敵(rgu)の生成クールタイムが終わったら
                        if (rguTimer >= rguIntarval)
                        {
                            // 敵を生成する
                            Instantiate(rgu, enemySpawnPos2.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;
                            rguTimer = 0;

                            // rguの存在フラグをtrueに
                            rguFlg = true;
                        }
                    }
                }
            }
        }

        // 3ウェーブ目の処理
        {
            if (waveManager.currentWave == 3 && stageManager.winFlg == false)
            {
                // 敵の出現クールタイムの調整
                rgIntarval = 8.0f;
                rsIntarval = 10.0f;
                raIntarval = 15.0f;
                deathCharacterCountMax = 15;

                // 城を出す
                enemyCastles[2].gameObject.SetActive(true);

                // 城の体力が無くなっていたら
                if (enemyCastles[2].die == true)
                {
                    return;
                }

                // 敵の数が上限より下の時
                if (enemyCount < enemyCountMax)
                {
                    //敵(rg)の生成クールタイムが終わったら
                    if (rgTimer >= rgIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rg, enemySpawnPos3.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rgTimer = 0;
                    }

                    //敵(rs)の生成クールタイムが終わったら
                    if (rsTimer >= rsIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rs, enemySpawnPos3.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rsTimer = 0;
                    }
                    // 敵(ra)の生成クールタイムが終わったら
                    if (raTimer >= raIntarval)
                    {
                        // 敵を生成する
                        Instantiate(ra, enemySpawnPos3.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        raTimer = 0;
                    }

                    //rwがいないとき
                    if (rwFlg == false)
                    {
                        // キャラクターの死亡数が最大数を超えたら
                        if (deathCharacterCount >= deathCharacterCountMax)
                        {
                            // 敵を生成する
                            Instantiate(rw, rwSpawnPos3.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;

                            // 敵の死亡数をリセット
                            deathCharacterCount = 0;

                            // rwの存在フラグをtrueに
                            rwFlg = true;
                        }
                    }

                    //rguがいないとき
                    if (rguFlg == false)
                    {
                        //敵(rgu)の生成クールタイムが終わったら
                        if (rguTimer >= rguIntarval)
                        {
                            // 敵を生成する
                            Instantiate(rgu, enemySpawnPos3.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;
                            rguTimer = 0;

                            // rguの存在フラグをtrueに
                            rguFlg = true;
                        }
                    }
                }
            }
        }
    }

    private void SpownEnemy5()
    {
        // 1ウェーブ目の処理
        {
            if (waveManager.currentWave == 1)
            {
                // 敵の出現クールタイムの調整
                rgIntarval = 8.0f;
                rsIntarval = 10.0f;

                // 城の体力が無くなっていたら
                if (enemyCastles[0].die == true)
                {
                    return;
                }

                // 敵の数が上限より下の時
                if (enemyCount < enemyCountMax)
                {
                    //敵(rg)の生成クールタイムが終わったら
                    if (rgTimer >= rgIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rg, enemySpawnPos.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rgTimer = 0;
                    }

                    //敵(rs)の生成クールタイムが終わったら
                    if (rsTimer >= rsIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rs, enemySpawnPos.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rsTimer = 0;
                    }

                    //rsuがいないとき
                    if (rsuFlg == false)
                    {
                        //敵(rsu)の生成クールタイムが終わったら
                        if (rsuTimer >= rsuIntarval)
                        {
                            // 敵を生成する
                            Instantiate(rsu, enemySpawnPos.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;
                            rsuTimer = 0;

                            // rguの存在フラグをtrueに
                            rsuFlg = true;
                        }
                    }
                }
            }
        }

        // 2ウェーブ目の処理
        {
            if (waveManager.currentWave == 2 && stageManager.winFlg == false)
            {
                // 敵の出現クールタイムの調整
                rgIntarval = 8.0f;
                rsIntarval = 10.0f;
                raIntarval = 15.0f;

                // 城を出す
                enemyCastles[1].gameObject.SetActive(true);

                // 城の体力が無くなっていたら
                if (enemyCastles[1].die == true)
                {
                    return;
                }

                // 敵の数が上限より下の時
                if (enemyCount < enemyCountMax)
                {
                    //敵(rg)の生成クールタイムが終わったら
                    if (rgTimer >= rgIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rg, enemySpawnPos2.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rgTimer = 0;
                    }

                    //敵(rs)の生成クールタイムが終わったら
                    if (rsTimer >= rsIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rs, enemySpawnPos2.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rsTimer = 0;
                    }

                    // 敵(ra)の生成クールタイムが終わったら
                    if (raTimer >= raIntarval)
                    {
                        // 敵を生成する
                        Instantiate(ra, enemySpawnPos2.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        raTimer = 0;
                    }

                    //rguがいないとき
                    if (rguFlg == false)
                    {
                        //敵(rgu)の生成クールタイムが終わったら
                        if (rguTimer >= rguIntarval)
                        {
                            // 敵を生成する
                            Instantiate(rgu, enemySpawnPos2.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;
                            rguTimer = 0;

                            // rguの存在フラグをtrueに
                            rguFlg = true;
                        }
                    }

                    //rsuがいないとき
                    if (rsuFlg == false)
                    {
                        //敵(rsu)の生成クールタイムが終わったら
                        if (rsuTimer >= rsuIntarval)
                        {
                            // 敵を生成する
                            Instantiate(rsu, enemySpawnPos2.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;
                            rsuTimer = 0;

                            // rguの存在フラグをtrueに
                            rsuFlg = true;
                        }
                    }
                }
            }
        }

        // 3ウェーブ目の処理
        {
            if (waveManager.currentWave == 3 && stageManager.winFlg == false)
            {
                // 敵の出現クールタイムの調整
                rgIntarval = 8.0f;
                rsIntarval = 10.0f;
                raIntarval = 15.0f;
                deathCharacterCountMax = 20;

                // 城を出す
                enemyCastles[2].gameObject.SetActive(true);

                // 城の体力が無くなっていたら
                if (enemyCastles[2].die == true)
                {
                    return;
                }

                // 敵の数が上限より下の時
                if (enemyCount < enemyCountMax)
                {
                    //敵(rg)の生成クールタイムが終わったら
                    if (rgTimer >= rgIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rg, enemySpawnPos3.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rgTimer = 0;
                    }

                    //敵(rs)の生成クールタイムが終わったら
                    if (rsTimer >= rsIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rs, enemySpawnPos3.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rsTimer = 0;
                    }
                    // 敵(ra)の生成クールタイムが終わったら
                    if (raTimer >= raIntarval)
                    {
                        // 敵を生成する
                        Instantiate(ra, enemySpawnPos3.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        raTimer = 0;
                    }

                    //rwがいないとき
                    if (rwFlg == false)
                    {
                        // キャラクターの死亡数が最大数を超えたら
                        if (deathCharacterCount >= deathCharacterCountMax)
                        {
                            // 敵を生成する
                            Instantiate(rw, rwSpawnPos3.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;

                            // 敵の死亡数をリセット
                            deathCharacterCount = 0;

                            // rwの存在フラグをtrueに
                            rwFlg = true;
                        }
                    }

                    //rguがいないとき
                    if (rguFlg == false)
                    {
                        //敵(rgu)の生成クールタイムが終わったら
                        if (rguTimer >= rguIntarval)
                        {
                            // 敵を生成する
                            Instantiate(rgu, enemySpawnPos3.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;
                            rguTimer = 0;

                            // rguの存在フラグをtrueに
                            rguFlg = true;
                        }
                    }

                    //rsuがいないとき
                    if (rsuFlg == false)
                    {
                        //敵(rsu)の生成クールタイムが終わったら
                        if (rsuTimer >= rsuIntarval)
                        {
                            // 敵を生成する
                            Instantiate(rsu, enemySpawnPos3.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;
                            rsuTimer = 0;

                            // rguの存在フラグをtrueに
                            rsuFlg = true;
                        }
                    }
                }
            }
        }
    }

    private void SpownEnemy6()
    {
        // 1ウェーブ目の処理
        {
            if (waveManager.currentWave == 1)
            {
                // 敵の出現クールタイムの調整
                rgIntarval = 10.0f;
                rsIntarval = 12.0f;

                // 城の体力が無くなっていたら
                if (enemyCastles[0].die == true)
                {
                    return;
                }

                // 敵の数が上限より下の時
                if (enemyCount < enemyCountMax)
                {
                    //敵(rg)の生成クールタイムが終わったら
                    if (rgTimer >= rgIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rg, enemySpawnPos.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rgTimer = 0;
                    }

                    //敵(rs)の生成クールタイムが終わったら
                    if (rsTimer >= rsIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rs, enemySpawnPos.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rsTimer = 0;
                    }

                    //rauがいないとき
                    if (rauFlg == false)
                    {
                        //敵(rsu)の生成クールタイムが終わったら
                        if (rauTimer >= rauIntarval)
                        {
                            // 敵を生成する
                            Instantiate(rau, enemySpawnPos.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;
                            rauTimer = 0;

                            // rguの存在フラグをtrueに
                            rauFlg = true;
                        }
                    }
                }
            }
        }

        // 2ウェーブ目の処理
        {
            if (waveManager.currentWave == 2 && stageManager.winFlg == false)
            {
                // 敵の出現クールタイムの調整
                rgIntarval = 10.0f;
                rsIntarval = 12.0f;
                raIntarval = 15.0f;

                // 城を出す
                enemyCastles[1].gameObject.SetActive(true);

                // 城の体力が無くなっていたら
                if (enemyCastles[1].die == true)
                {
                    return;
                }

                // 敵の数が上限より下の時
                if (enemyCount < enemyCountMax)
                {
                    //敵(rg)の生成クールタイムが終わったら
                    if (rgTimer >= rgIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rg, enemySpawnPos2.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rgTimer = 0;
                    }

                    //敵(rs)の生成クールタイムが終わったら
                    if (rsTimer >= rsIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rs, enemySpawnPos2.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rsTimer = 0;
                    }

                    // 敵(ra)の生成クールタイムが終わったら
                    if (raTimer >= raIntarval)
                    {
                        // 敵を生成する
                        Instantiate(ra, enemySpawnPos2.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        raTimer = 0;
                    }

                    //rguがいないとき
                    if (rguFlg == false)
                    {
                        //敵(rgu)の生成クールタイムが終わったら
                        if (rguTimer >= rguIntarval)
                        {
                            // 敵を生成する
                            Instantiate(rgu, enemySpawnPos2.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;
                            rguTimer = 0;

                            // rguの存在フラグをtrueに
                            rguFlg = true;
                        }
                    }

                    //rauがいないとき
                    if (rauFlg == false)
                    {
                        //敵(rsu)の生成クールタイムが終わったら
                        if (rauTimer >= rauIntarval)
                        {
                            // 敵を生成する
                            Instantiate(rau, enemySpawnPos2.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;
                            rauTimer = 0;

                            // rguの存在フラグをtrueに
                            rauFlg = true;
                        }
                    }
                }
            }
        }

        // 3ウェーブ目の処理
        {
            if (waveManager.currentWave == 3 && stageManager.winFlg == false)
            {
                // 敵の出現クールタイムの調整
                rgIntarval = 10.0f;
                rsIntarval = 12.0f;
                raIntarval = 15.0f;
                deathCharacterCountMax = 20;

                // 城を出す
                enemyCastles[2].gameObject.SetActive(true);

                // 城の体力が無くなっていたら
                if (enemyCastles[2].die == true)
                {
                    return;
                }

                // 敵の数が上限より下の時
                if (enemyCount < enemyCountMax)
                {
                    //敵(rg)の生成クールタイムが終わったら
                    if (rgTimer >= rgIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rg, enemySpawnPos3.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rgTimer = 0;
                    }

                    //敵(rs)の生成クールタイムが終わったら
                    if (rsTimer >= rsIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rs, enemySpawnPos3.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rsTimer = 0;
                    }
                    // 敵(ra)の生成クールタイムが終わったら
                    if (raTimer >= raIntarval)
                    {
                        // 敵を生成する
                        Instantiate(ra, enemySpawnPos3.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        raTimer = 0;
                    }

                    //rwがいないとき
                    if (rwFlg == false)
                    {
                        // キャラクターの死亡数が最大数を超えたら
                        if (deathCharacterCount >= deathCharacterCountMax)
                        {
                            // 敵を生成する
                            Instantiate(rw, rwSpawnPos3.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;

                            // 敵の死亡数をリセット
                            deathCharacterCount = 0;

                            // rwの存在フラグをtrueに
                            rwFlg = true;
                        }
                    }

                     //rsuがいないとき
                    if (rsuFlg == false)
                    {
                        //敵(rsu)の生成クールタイムが終わったら
                        if (rsuTimer >= rsuIntarval)
                        {
                            // 敵を生成する
                            Instantiate(rsu, enemySpawnPos3.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;
                            rsuTimer = 0;

                            // rguの存在フラグをtrueに
                            rsuFlg = true;
                        }
                    }

                    //rauがいないとき
                    if (rauFlg == false)
                    {
                        //敵(rsu)の生成クールタイムが終わったら
                        if (rauTimer >= rauIntarval)
                        {
                            // 敵を生成する
                            Instantiate(rau, enemySpawnPos3.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;
                            rauTimer = 0;

                            // rguの存在フラグをtrueに
                            rauFlg = true;
                        }
                    }
                }
            }
        }
    }

    private void SpownEnemy7()
    {
        // 1ウェーブ目の処理
        {
            if (waveManager.currentWave == 1)
            {
                // 敵の出現クールタイムの調整
                rgIntarval = 10.0f;
                rsIntarval = 12.0f;

                // 城の体力が無くなっていたら
                if (enemyCastles[0].die == true)
                {
                    return;
                }

                // 敵の数が上限より下の時
                if (enemyCount < enemyCountMax)
                {
                    //敵(rg)の生成クールタイムが終わったら
                    if (rgTimer >= rgIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rg, enemySpawnPos.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rgTimer = 0;
                    }

                    //敵(rs)の生成クールタイムが終わったら
                    if (rsTimer >= rsIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rs, enemySpawnPos.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rsTimer = 0;
                    }

                    //rguがいないとき
                    if (rguFlg == false)
                    {
                        //敵(rgu)の生成クールタイムが終わったら
                        if (rguTimer >= rguIntarval)
                        {
                            // 敵を生成する
                            Instantiate(rgu, enemySpawnPos.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;
                            rguTimer = 0;

                            // rguの存在フラグをtrueに
                            rguFlg = true;
                        }
                    }

                    //rwuがいないとき
                    if (rwuFlg == false)
                    {
                        // キャラクターの死亡数が最大数を超えたら
                        if (deathCharacterCount >= deathCharacterCountMaxRWU)
                        {
                            // 敵を生成する
                            Instantiate(rwu, rwSpawnPos.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;

                            // 敵の死亡数をリセット
                            deathCharacterCount = 0;

                            // rwの存在フラグをtrueに
                            rwuFlg = true;
                        }
                    }
                }
            }
        }

        // 2ウェーブ目の処理
        {
            if (waveManager.currentWave == 2 && stageManager.winFlg == false)
            {
                // 敵の出現クールタイムの調整
                rgIntarval = 10.0f;
                rsIntarval = 12.0f;
                raIntarval = 15.0f;

                // 城を出す
                enemyCastles[1].gameObject.SetActive(true);

                // 城の体力が無くなっていたら
                if (enemyCastles[1].die == true)
                {
                    return;
                }

                // 敵の数が上限より下の時
                if (enemyCount < enemyCountMax)
                {
                    //敵(rg)の生成クールタイムが終わったら
                    if (rgTimer >= rgIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rg, enemySpawnPos2.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rgTimer = 0;
                    }

                    //敵(rs)の生成クールタイムが終わったら
                    if (rsTimer >= rsIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rs, enemySpawnPos2.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rsTimer = 0;
                    }

                    // 敵(ra)の生成クールタイムが終わったら
                    if (raTimer >= raIntarval)
                    {
                        // 敵を生成する
                        Instantiate(ra, enemySpawnPos2.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        raTimer = 0;
                    }

                    //rguがいないとき
                    if (rguFlg == false)
                    {
                        //敵(rgu)の生成クールタイムが終わったら
                        if (rguTimer >= rguIntarval)
                        {
                            // 敵を生成する
                            Instantiate(rgu, enemySpawnPos2.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;
                            rguTimer = 0;

                            // rguの存在フラグをtrueに
                            rguFlg = true;
                        }
                    }

                    //rsuがいないとき
                    if (rsuFlg == false)
                    {
                        //敵(rsu)の生成クールタイムが終わったら
                        if (rsuTimer >= rsuIntarval)
                        {
                            // 敵を生成する
                            Instantiate(rsu, enemySpawnPos2.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;
                            rsuTimer = 0;

                            // rguの存在フラグをtrueに
                            rsuFlg = true;
                        }
                    }

                    //rwuがいないとき
                    if (rwuFlg == false)
                    {
                        // キャラクターの死亡数が最大数を超えたら
                        if (deathCharacterCount >= deathCharacterCountMaxRWU)
                        {
                            // 敵を生成する
                            Instantiate(rwu, rwSpawnPos2.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;

                            // 敵の死亡数をリセット
                            deathCharacterCount = 0;

                            // rwの存在フラグをtrueに
                            rwuFlg = true;
                        }
                    }
                }
            }
        }

        // 3ウェーブ目の処理
        {
            if (waveManager.currentWave == 3 && stageManager.winFlg == false)
            {
                // 敵の出現クールタイムの調整
                rgIntarval = 10.0f;
                rsIntarval = 12.0f;
                raIntarval = 15.0f;

                // 城を出す
                enemyCastles[2].gameObject.SetActive(true);

                // 城の体力が無くなっていたら
                if (enemyCastles[2].die == true)
                {
                    return;
                }

                // 敵の数が上限より下の時
                if (enemyCount < enemyCountMax)
                {
                    //敵(rg)の生成クールタイムが終わったら
                    if (rgTimer >= rgIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rg, enemySpawnPos3.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rgTimer = 0;
                    }

                    //敵(rs)の生成クールタイムが終わったら
                    if (rsTimer >= rsIntarval)
                    {
                        // 敵を生成する
                        Instantiate(rs, enemySpawnPos3.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        rsTimer = 0;
                    }
                    // 敵(ra)の生成クールタイムが終わったら
                    if (raTimer >= raIntarval)
                    {
                        // 敵を生成する
                        Instantiate(ra, enemySpawnPos3.position, Quaternion.identity);

                        //　敵の数を加算する
                        enemyCount++;
                        raTimer = 0;
                    }

                    //rguがいないとき
                    if (rguFlg == false)
                    {
                        //敵(rgu)の生成クールタイムが終わったら
                        if (rguTimer >= rguIntarval)
                        {
                            // 敵を生成する
                            Instantiate(rgu, enemySpawnPos3.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;
                            rguTimer = 0;

                            // rguの存在フラグをtrueに
                            rguFlg = true;
                        }
                    }

                    //rsuがいないとき
                    if (rsuFlg == false)
                    {
                        //敵(rsu)の生成クールタイムが終わったら
                        if (rsuTimer >= rsuIntarval)
                        {
                            // 敵を生成する
                            Instantiate(rsu, enemySpawnPos3.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;
                            rsuTimer = 0;

                            // rguの存在フラグをtrueに
                            rsuFlg = true;
                        }
                    }

                    //rauがいないとき
                    if (rauFlg == false)
                    {
                        //敵(rsu)の生成クールタイムが終わったら
                        if (rauTimer >= rauIntarval)
                        {
                            // 敵を生成する
                            Instantiate(rau, enemySpawnPos3.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;
                            rauTimer = 0;

                            // rguの存在フラグをtrueに
                            rauFlg = true;
                        }
                    }

                    //rwuがいないとき
                    if (rwuFlg == false)
                    {
                        // キャラクターの死亡数が最大数を超えたら
                        if (deathCharacterCount >= deathCharacterCountMaxRWU)
                        {
                            // 敵を生成する
                            Instantiate(rwu, rwSpawnPos3.position, Quaternion.identity);

                            //　敵の数を加算する
                            enemyCount++;

                            // 敵の死亡数をリセット
                            deathCharacterCount = 0;

                            // rwの存在フラグをtrueに
                            rwuFlg = true;
                        }
                    }
                }
            }
        }
    }
}
