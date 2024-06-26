using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyRAU : EnemyController
{
    private Animator anim;                  // アニメーション
    private EnemySpawn enemySpawn;          // 敵の出現位置
    private Coin coin;                      // コイン
    [SerializeField]
    private EnemyCastle[] enemyCastles;     // 敵の城
    private WaveManager waveManager;        // Waveマネージャー
    public Dino dino;                      // Dino

    [SerializeField]
    private GameObject bulletPrefab;        // 弾のプレハブ
    [SerializeField]
    private Transform firePoint;            // 弾の発射位置
    [SerializeField]
    private float bulletSpeed = 1.0f;       // 弾の速度

    AudioSource audioSource;                // オーディオソース
    [SerializeField]
    private AudioClip attackSE;             // 攻撃音
    [SerializeField]
    private AudioClip hitSE;                // 被弾音

    private GameObject canvas;              // ダメージ用キャンバス
    [SerializeField]
    private GameObject damageTextPrefab;    // ダメージのテキスト
    [SerializeField]
    private GameObject addCoinTextPrefab;   // 増えたコインのテキスト

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
        addCoin = 1200.0f;

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

        // 移動クールタイムの設定
        moveTimer = moveIntarval;
    }

    // Update is called once per frame
    void Update()
    {
        moveTimer += Time.deltaTime;

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

        // 体力が無くなる、城が壊れる、下に落ちたら
        if (hp <= 0 || enemyCastles[0].die == true || enemyCastles[1].die == true || enemyCastles[2].die == true || rb.position.y < -1.0f)
        {
            // 死亡アニメーションを再生
            DieAnim();
        }
    }

    // 移動処理
    private void Move()
    {
        // キャラクターを移動させる
        rb.velocity = new Vector2(-speed, rb.velocity.y);

        // 移動のアニメーション再生
        anim.SetBool("Move", true);
    }

    // 移動を止める処理
    public void StopMove()
    {
        // 移動を止める
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

    public void Shoot()
    {
        // 弾のプレハブから新しい弾を生成
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // 弾に速度を与える
        Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();
        rbb.velocity = firePoint.right * -bulletSpeed;
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

        // 敵の出現数を減らす
        enemySpawn.enemyCount--;

        enemySpawn.rauFlg = false;

        // パーティクルを生成して自分の位置に表示
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
        // 敵か敵の城に触れていたら
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Castle"))
        {
            StopMove();
        }

        // 味方に触れていたら
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyCastle") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            // 当たらないようにする
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

        // 矢に当たったら
        if (collision.gameObject.CompareTag("Bullet"))
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

    private void ShowDamageText(int damage)
    {
        // 今いるシーンがステージのとき
        if (SceneManager.GetActiveScene().name != "Title" && SceneManager.GetActiveScene().name != "StageSelect")
        {
            // 自身の頭上にテキストを生成
            Vector3 textPosition = transform.position + new Vector3(0.4f, 1.2f, 0);
            GameObject damageTextInstance = Instantiate(damageTextPrefab, textPosition, Quaternion.identity);

            // TextMeshPro に反映
            damageTextInstance.GetComponentInChildren<TextMeshProUGUI>().text = "-" + damage.ToString("N0");
            // 子テキストの生成
            damageTextInstance.transform.SetParent(canvas.transform);
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
