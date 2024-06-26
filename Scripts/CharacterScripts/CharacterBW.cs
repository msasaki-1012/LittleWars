using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterBW : Character
{
    private Animator anim;                              // アニメーション
    private Spawn spawnPos;                          // キャラクター生成
    private TitleCharacterSpawn titleCharacterSpawn;    // タイトルのキャラクター生成
    private Castle castle;                              // 味方側の城
    private WaveManager waveManager;                    // ウェーブマネージャー
    private Coin coin;                                  // コイン

    AudioSource audioSource;                            // オーディオソース
    public AudioClip attackSE;                          // 攻撃音

    private GameObject canvas;                          // ダメージ用キャンバス
    public GameObject addCoinTextPrefab;                // 増えたコインのテキスト

    public float deathTimer = 0;                        // 死亡時間計算用タイマー
    private bool attackFlg = false;                     // 攻撃フラグ

    // Start is called before the first frame update
    void Start()
    {
        // キャラクターを取得
        rb = GetComponent<Rigidbody2D>();

        // アニメーションを取得
        anim = GetComponent<Animator>();

        // オーディオソースを取得
        audioSource = GetComponent<AudioSource>();

        // キャラクターの出現位置を取得
        spawnPos = GameObject.Find("GameManager").GetComponent<Spawn>();

        // 味方の城を取得
        castle = GameObject.Find("BaseBlue").GetComponent<Castle>();

        // ウェーブマネージャーを取得
        waveManager = GameObject.Find("GameManager").GetComponent<WaveManager>();

        // 今いるシーンがタイトルのとき
        if (SceneManager.GetActiveScene().name == "Title")
        {
            // タイトルでのキャラクターの生成を取得
            titleCharacterSpawn = GameObject.Find("GameManager").GetComponent<TitleCharacterSpawn>();
        }

        // コイン取得
        coin = GameObject.Find("GameManager").GetComponent<Coin>();

        // 増やすコインの数
        addCoin = 125.0f;

        // 今いるシーンがステージのとき
        if (SceneManager.GetActiveScene().name == "Stage2")
        {
            addCoin *= 1.5f;
        }

        // 今いるシーンがステージのとき
        if (SceneManager.GetActiveScene().name == "Stage3")
        {
            addCoin *= 2.0f;
        }

        // キャンバス取得
        canvas = GameObject.Find("DamageCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        // キャラクター移動用タイマーを更新
        moveTimer += Time.deltaTime;

        // キャラクター死亡用タイマーを更新
        deathTimer += Time.deltaTime;

        // 移動用タイマーが指定した間隔を超えたら 
        if (moveTimer > moveIntarval)
        {
            // 値が大きくならないようにする
            moveTimer = moveIntarval;
        }

        // キャラクターが生きているとき
        if (die == false)
        {
            // 移動待機時間がインターバルを超えたら
            if (moveTimer >= moveIntarval)
            {
                // 移動する
                Move();
            }
        }

        // 攻撃できるとき
        if (attackFlg == true)
        {
            // 死亡タイマーが2秒を超えたら
            if (deathTimer >= 2.0f)
            {
                Die();
            }
        }

        // 体力が無くなる、城が壊れる、下に落ちる
        if (hp <= 0 || castle.die == true)
        {
            // 死亡アニメーションを再生
            Die();
        }

        // 今いるシーンがステージのとき
        if (SceneManager.GetActiveScene().name != "Title" && SceneManager.GetActiveScene().name != "StageSelect")
        {
            // 次のウェーブに行ったら
            if (waveManager.nextWaveFlg == true)
            {
                // 死亡アニメーションを再生
                Die();
            }
        }
    }

    // 移動処理
    private void Move()
    {
        // 移動のアニメーション再生
        anim.SetBool("Move", true);
    }

    // 移動を止める処理
    public void StopMove()
    {
        // 移動のアニメーションを消す
        anim.SetBool("Move", false);
        moveTimer = 0;
    }

    // 敵への攻撃処理
    public void AttackAnim()
    {
        deathTimer = 0;

        // 攻撃のアニメーション再生
        anim.SetTrigger("Attack");

        // 攻撃の効果音再生
        audioSource.PlayOneShot(attackSE);

        attackFlg = true;
    }

    // ダメージ処理
    public void TakeDamage(int damage)
    {
        // 体力を減らす
        hp -= damage;

        hit = true;
    }

    // 死亡処理
    private void Die()
    {
        die = true;

        // 今いるシーンがタイトルのとき
        if (SceneManager.GetActiveScene().name == "Title")
        {
            // 存在フラグをfalseに
            titleCharacterSpawn.bwFlg = false;

            // キャラクターの出現数を減らす
            titleCharacterSpawn.characterCount--;

            // キャラクターの死亡カウントを増やす
            titleCharacterSpawn.deathCharacterCount++;
        }
        else
        {
            // 存在フラグをfalseに
            spawnPos.bwFlg = false;

            // キャラクターの出現数を減らす
            spawnPos.characterCount--;

            // 次のウェーブに行かない場合
            if (waveManager.nextWaveFlg == false)
            {
                // キャラクターの死亡カウントを増やす
                spawnPos.deathCharacterCount++;
            }
            else
            {
                // 増やすコインの数を表示
                ShowAddCoinText();
            }
        }

        // キャラクターの破壊処理などを行う
        Destroy(gameObject);

        // パーティクルを生成して敵の位置に表示
        if (particlePrefab != null)
        {
            // パーティクル再生
            GameObject particleInstance = Instantiate(particlePrefab, this.transform.position, Quaternion.identity);

            // パーティクルを一定時間後に削除
            Destroy(particleInstance, particleLifetime);
        }
    }

    // 衝突処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 味方に触れていたら
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet"))
        {
            // 当たらないようにする
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    // 衝突処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Dinoに当たったら
        if (collision.gameObject.CompareTag("Dino"))
        {
            // 死亡アニメーションを再生
            Die();
        }
    }

    // 取得したコインを表示する関数
    private void ShowAddCoinText()
    {
        // 今いるシーンがタイトル以外のとき
        if (SceneManager.GetActiveScene().name != "Title")
        {
            // コインを増やす
            coin.coinCount += addCoin;

            // 自身の頭上にテキストを生成
            Vector3 textPosition = transform.position + new Vector3(0.4f, 1.5f, 0);
            GameObject addCoinTextInstance = Instantiate(addCoinTextPrefab, textPosition, Quaternion.identity);

            // TextMeshPro に反映
            addCoinTextInstance.GetComponentInChildren<TextMeshProUGUI>().text = "+" + addCoin.ToString("N0") + "G";

            // 子テキストの生成
            addCoinTextInstance.transform.SetParent(canvas.transform);
        }
    }
}
