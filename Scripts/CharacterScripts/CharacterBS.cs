using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterBS : Character
{
    private Animator anim;                              // アニメーション
    private Spawn spawnPos;                          // キャラクター生成
    private TitleCharacterSpawn titleCharacterSpawn;    // タイトルのキャラクター生成
    private Castle castle;                              // 味方側の城
    private WaveManager waveManager;                    // ウェーブマネージャー
    private Coin coin;                                  // コイン
    public Dino dino;                                  //Dino

    public SpriteRenderer characterSpriteRenderer;      // スプライトを表示するコンポーネント
    private FollowCharacterWithParticles followCharacterWithParticles;      // 足元に出てくるパーティクル

    AudioSource audioSource;                            // オーディオソース
    public AudioClip attackSE;                          // 攻撃音
    public AudioClip hitSE;                             // 被弾音

    private GameObject canvas;                          // ダメージ用キャンバス
    public GameObject damageTextPrefab;                 // ダメージのテキスト
    public GameObject addCoinTextPrefab;                // 増えたコインのテキスト

    private float minFlipInterval = 3.0f;               // 最小向き変更間隔（秒）
    private float maxFlipInterval = 5.0f;               // 最大向き変更間隔（秒）
    private float flipTimer = 0.0f;                     // 反転時間の計測タイマー
    private float timeToNextFlip;                       // 次の向き変更までの時間
    private bool isFacingRight = true;                  // キャラクターが右を向いているかどうかのフラグ

    // Start is called before the first frame update
    void Start()
    {
        // キャラクターを取得
        rb = GetComponent<Rigidbody2D>();

        // アニメーションを取得
        anim = GetComponent<Animator>();

        // オーディオソースを取得
        audioSource = GetComponent<AudioSource>();

        // キャラクターの画像を取得
        characterSpriteRenderer = GetComponent<SpriteRenderer>();

        // 足元に出てくるパーティクルを取得
        followCharacterWithParticles = GetComponent<FollowCharacterWithParticles>();

        // キャラクターの出現位置を取得
        spawnPos = GameObject.Find("GameManager").GetComponent<Spawn>();

        // 味方の城を取得
        castle = GameObject.Find("BaseBlue").GetComponent<Castle>();

        // ウェーブマネージャーを取得
        waveManager = GameObject.Find("GameManager").GetComponent<WaveManager>();

        // コイン取得
        coin = GameObject.Find("GameManager").GetComponent<Coin>();

        // キャンバス取得
        canvas = GameObject.Find("DamageCanvas");

        // 今いるシーンがタイトルのとき
        if (SceneManager.GetActiveScene().name == "Title")
        {
            // タイトルでのキャラクターの生成を取得
            titleCharacterSpawn = GameObject.Find("GameManager").GetComponent<TitleCharacterSpawn>();
        }

        // 増やすコインの数
        addCoin = 40.0f;

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

        // 移動クールタイムの設定
        moveTimer = moveIntarval;

        // 最初は右向きのスプライトを表示
        characterSpriteRenderer.flipX = false;
    }

    // Update is called once per frame
    void Update()
    {
        // キャラクター移動用タイマーを更新
        moveTimer += Time.deltaTime;

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

        // 体力が無くなる、城が壊れる、下に落ちたら
        if (hp <= 0 || castle.die == true || rb.position.y < -1.0f)
        {
            // 死亡アニメーションを再生
            DieAnim();
        }

        // 今いるシーンがステージのとき
        if (SceneManager.GetActiveScene().name != "Title" && SceneManager.GetActiveScene().name != "StageSelect")
        {
            // 次のウェーブに行ったら
            if (waveManager.nextWaveFlg == true)
            {
                // 死亡アニメーションを再生
                DieAnim();
            }
        }

        // 今いるシーンがステージセレクトのとき
        if (SceneManager.GetActiveScene().name == "StageSelect")
        {
            // キャラクター反転用タイマーを更新
            flipTimer += Time.deltaTime;

            // 反転用タイマーが指定した間隔を超えたら向きを切り替える
            if (flipTimer >= timeToNextFlip)
            {
                ToggleCharacterFacingDirection();

                // 次のランダムな向き変更間隔を設定
                SetRandomFlipInterval(); 

                // タイマーをリセット
                flipTimer = 0.0f;
            }
        }
    }

    // 移動処理
    private void Move()
    {
        // キャラクターを移動させる
        rb.velocity = new Vector2(speed, rb.velocity.y);

        // 移動のアニメーション再生
        anim.SetBool("Move", true);
    }

    // 移動を止める処理
    public void StopMove()
    {
        // キャラクターの移動を止める
        rb.velocity = new Vector2(0, rb.velocity.y);

        // 移動のアニメーションを消す
        anim.SetBool("Move", false);
        moveTimer = 0;
    }

    // 敵への攻撃処理
    public void AttackAnim()
    {
        // 攻撃のアニメーション再生
        anim.SetTrigger("Attack");

        // 攻撃の効果音再生
        audioSource.PlayOneShot(attackSE);
    }

    // ダメージ処理
    public void TakeDamage(int damage)
    {
        // ヒットアニメーション再生
        anim.SetBool("Hit", true);

        // 体力を減らす
        hp -= damage;

        // ダメージのテキストを表示
        ShowDamageText(damage);

        hit = true;
    }

    public void StopHitAnim()
    {
        // ヒットアニメーションを止める
        hit = false;
        anim.SetBool("Hit", false);
    }

    // 死亡アニメーション
    private void DieAnim()
    {
        die = true;

        // 移動を止める
        StopMove();

        // 死亡アニメーション再生
        anim.SetBool("Die", true);
    }

    // 死亡処理
    private void Die()
    {
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

        // 今いるシーンがタイトルのとき
        if (SceneManager.GetActiveScene().name == "Title")
        {
            // キャラクターの出現数を減らす
            titleCharacterSpawn.characterCount--;

            // キャラクターの死亡カウントを増やす
            titleCharacterSpawn.deathCharacterCount++;
        }
        else
        {
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
    }

    // 衝突処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Enemyか敵の城に触れていたら
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyCastle"))
        {
            StopMove();
        }

        // 味方に触れていたら
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Castle"))
        {
            // 当たらないようにする
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

        // 矢に当たったら
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            // 攻撃の効果音再生
            audioSource.PlayOneShot(hitSE);
        }

    }

    // 衝突処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Dinoに当たったら
        if (collision.gameObject.CompareTag("Dino"))
        {
            // 死亡
            TakeDamage(dino.power);
        }
    }

    // キャラクターの向きを切り替える関数
    void ToggleCharacterFacingDirection()
    {
        // 向きを反転させる
        isFacingRight = !isFacingRight;

        // キャラクターを移動させる
        speed *= -1;

        // スプライトをフリップさせる
        characterSpriteRenderer.flipX = !isFacingRight;

        // パーティクルをフリップさせる
        followCharacterWithParticles.offset.x *= -1;
    }

    // ランダムな向き変更間隔を設定
    void SetRandomFlipInterval()
    {
        timeToNextFlip = Random.Range(minFlipInterval, maxFlipInterval);
    }

    // ダメージを表示する関数
    private void ShowDamageText(int damage)
    {
        // 今いるシーンがステージのとき
        if (SceneManager.GetActiveScene().name != "Title" && SceneManager.GetActiveScene().name != "StageSelect")
        {
            // 自身の頭上にテキストを生成
            Vector3 textPosition = transform.position + new Vector3(-0.4f, 1.2f, 0);
            GameObject damageTextInstance = Instantiate(damageTextPrefab, textPosition, Quaternion.identity);

            // TextMeshPro に反映
            damageTextInstance.GetComponentInChildren<TextMeshProUGUI>().text = "-" + damage.ToString("N0");
            // 子テキストの生成
            damageTextInstance.transform.SetParent(canvas.transform);
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
