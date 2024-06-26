using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyRW : EnemyController
{
    private Animator anim;                  // アニメーション

    private EnemySpawn enemySpawn;          // 敵の出現位置
    private Coin coin;                      // コイン

    [SerializeField]
    private EnemyCastle[] enemyCastles;     // 敵の城

    private WaveManager waveManager;        // Waveマネージャー

    AudioSource audioSource;                // オーディオソース
    [SerializeField]
    private AudioClip attackSE;              // 攻撃音

    private GameObject canvas;              // ダメージ用キャンバス
    [SerializeField]
    private GameObject addCoinTextPrefab;    // 増えたコインのテキスト

    [SerializeField]
    private float deathTimer = 0;            // 死亡時間計算用タイマー
    private bool attackFlg = false;         // 攻撃フラグ

    // Start is called before the first frame update
    void Start()
    {
        // キャラクターを取得
        rb = GetComponent<Rigidbody2D>();

        // アニメーションを取得
        anim = GetComponent<Animator>();

        // オーディオソースを取得
        audioSource = GetComponent<AudioSource>();

        // 敵の出現位置を取得
        enemySpawn = GameObject.Find("GameManager").GetComponent<EnemySpawn>();

        waveManager = GameObject.Find("GameManager").GetComponent<WaveManager>();

        // 今いるシーンがステージのとき
        if (SceneManager.GetActiveScene().name == "Title")
        {
            // 敵の城を取得
            enemyCastles[0] = GameObject.Find("BaseRed").GetComponent<EnemyCastle>();
        }

        // 今いるシーンがステージのとき
        if (SceneManager.GetActiveScene().name != "Title" && SceneManager.GetActiveScene().name != "StageSelect")
        {
            // 現在のウェーブが1の時
            if (waveManager.currentWave == 1)
            {
                // 敵の城を取得
                enemyCastles[0] = GameObject.Find("BaseRed").GetComponent<EnemyCastle>();
            }
            // 現在のウェーブが2の時
            if (waveManager.currentWave == 2)
            {
                // 敵の城を取得
                enemyCastles[1] = GameObject.Find("BaseRed2").GetComponent<EnemyCastle>();
            }
            // 現在のウェーブが3の時
            if (waveManager.currentWave == 3)
            {
                enemyCastles[2] = GameObject.Find("BaseRed3").GetComponent<EnemyCastle>();
            }
        }

        // コイン取得
        coin = GameObject.Find("GameManager").GetComponent<Coin>();

        // 増やすコインの数
        addCoin = 250.0f;

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

        // タイマーが大きくならないように上限をつける
        if (moveTimer > moveIntarval)
        {
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

        // 攻撃した後
        if (attackFlg == true)
        {
            // 死亡タイマーが2秒を超えたら
            if (deathTimer >= 2.0f)
            {
                Die();
            }
        }

        // 体力が無くなる、城が壊れる、下に落ちたら
        if (hp <= 0 || enemyCastles[0].die == true || enemyCastles[1].die == true || enemyCastles[2].die == true)
        {
            // 死亡アニメーションを再生
            Die();
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

        enemySpawn.rwFlg = false;

        // キャラクターの破壊処理などを行う
        Destroy(gameObject);

        // 敵の出現数を減らす
        enemySpawn.enemyCount--;

        // パーティクルを生成して敵の位置に表示
        if (particlePrefab != null)
        {
            // パーティクル再生
            GameObject particleInstance = Instantiate(particlePrefab, this.transform.position, Quaternion.identity);

            // パーティクルを一定時間後に削除
            Destroy(particleInstance, particleLifetime);
        }

        // キャラクターの死亡カウントを増やす
        enemySpawn.deathCharacterCount++;

        // 増やすコインの数を表示
        ShowAddCoinText();
    }

    // 衝突処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 味方に触れていたら
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
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
