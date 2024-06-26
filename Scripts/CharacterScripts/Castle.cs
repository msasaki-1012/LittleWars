using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Castle: Character
{
    private Animator anim;                      // アニメーション
    [SerializeField] private Slider slider;     // スライダー
    private StageManager stageManager;          // ステージマネージャー
    private GameObject canvas;                  // ダメージ用キャンバス
    public GameObject damageTextPrefab;         // ダメージのテキスト
    public TextMeshProUGUI healthTextL;         // 体力のテキスト
    public Dino dino;                          // Dino

    // Start is called before the first frame update
    void Start()
    {
        // アニメーションを取得
        anim = GetComponent<Animator>();

        // ステージマネージャーを取得
        stageManager = GameObject.Find("GameManager").GetComponent<StageManager>();

        // キャンバス取得
        canvas = GameObject.Find("DamageCanvas");

        // 今いるシーンがタイトルのとき
        if (SceneManager.GetActiveScene().name == "Title")
        {
            // スライダーを出す
            slider.gameObject.SetActive(true);
        }

        if (SceneManager.GetActiveScene().name != "StageSelect")
        {
            // スライダーを取得
            slider = GameObject.Find("CastleHPSlider").GetComponent<Slider>();

            // 体力のテキストを取得
            healthTextL = GameObject.Find("HealthTextL").GetComponent<TextMeshProUGUI>();
        }

        // 今いるシーンがタイトルのとき
        if (SceneManager.GetActiveScene().name == "Title")
        {
            // スライダーを消す
            slider.gameObject.SetActive(false);
        }
    }
   
    // Update is called once per frame
    void Update()
    {
        // 今いるシーンがタイトル、ステージセレクト以外のとき
        if (SceneManager.GetActiveScene().name != "Title" && SceneManager.GetActiveScene().name != "StageSelect")
        {
            // HPゲージに値を設定
            slider.value = hp;

            // HPが無くなったら
            if (hp <= 0)
            {
                hp = 0;
                DieAnim();
            }

            // テキストに体力を入れる
            healthTextL.text = hp.ToString("N0");
        }
    }

    // ダメージ処理
    public void TakeDamage(int amount)
    {
        hp -= amount;

        // ダメージのテキストを表示
        ShowDamageText(amount);

        // ヒットアニメーション再生
        anim.SetBool("Hit", true);

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

        // 死亡アニメーション再生
        anim.SetBool("Die", true);
    }

    // 死亡処理
    private void Die()
    {
        // 敵の破壊処理などを行う
        Destroy(gameObject);

        // パーティクルを生成して敵の位置に表示
        if (particlePrefab != null)
        {
            // パーティクル再生
            GameObject particleInstance = Instantiate(particlePrefab, this.transform.position, Quaternion.identity);

            // パーティクルを一定時間後に削除
            Destroy(particleInstance, particleLifetime);
        }

        // 敗北フラグをtrueにする
        stageManager.loseFlg = true;
        stageManager.loseSEflg = true;
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
            Vector3 textPosition = transform.position + new Vector3(0, 2, 0);
            GameObject damageTextInstance = Instantiate(damageTextPrefab, textPosition, Quaternion.identity);

            // TextMeshPro に反映
            damageTextInstance.GetComponentInChildren<TextMeshProUGUI>().text = "-" + damage.ToString("N0");
            // 子テキストの生成
            damageTextInstance.transform.SetParent(canvas.transform);
        }
    }
}
