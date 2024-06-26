using UnityEngine;

public class Dino : MonoBehaviour
{
    private Rigidbody2D rb;             // 当たり判定
    private AudioSource audioSource;    // オーディオソース
    public AudioClip attackSE;          // 攻撃音
    private StageManager stageManager;  // ステージマネージャー
    private DinoSpawn dinoSpawn;        // Dinoの生成
    private Transform dinoPos;          // Dinoが一度止まる位置
   
    public int power { get; } = 999;    // Dinoの攻撃力
    private float speed = -6.0f;        // Dinoの速度
    public float dinoTimer { get; set; }    // 待機時間計算用タイマー
    public float dinoIntarval { get; set; } = 240.0f;    // Dinoの待機時間
    public float dinoMoveTimer { get; set; }    // 移動時間計算用タイマー
    public float dinoMoveIntarval { get; set; } = 3.0f;  // Dinoが動き出すまでの時間
    private bool attackFlg = true;          // 攻撃フラグ

    private float destroyIntarval = 3.0f;   // Dinoが消滅するまでの時間
    private float destroyTimer;             // Dinoが消滅するまでの時間の計算用タイマー

    [SerializeField]
    protected GameObject particlePrefab;        // パーティクルのプレハブを指定するための変数
    protected float particleLifetime = 2.0f;    // パーティクルの寿命（秒）

    // Start is called before the first frame update
    void Start()
    {
        // キャラクターを取得
        rb = GetComponent<Rigidbody2D>();

        // オーディオソースを取得
        audioSource = GetComponent<AudioSource>();

        // Dinoの生成を取得
        dinoSpawn = GameObject.Find("GameManager").GetComponent<DinoSpawn>();

        // ステージマネージャーを取得
        stageManager = GameObject.Find("GameManager").GetComponent<StageManager>();

        // Dinoが一度止まる位置を取得
        dinoPos = GameObject.Find("DinoPos").transform;

        // 存在フラグをtrueに
        dinoSpawn.dinoFlg = true;
    }

    // Update is called once per frame
    void Update()
    {
        // 待機時間計算用タイマー加算処理
        dinoTimer += Time.deltaTime;

        // 勝利フラグがtrueのとき
        if (stageManager.winFlg)
        {
            // 死亡する
            Die();

            // 待機時間計算用タイマーをリセット
            dinoTimer = 0;
        }

        // 敗北フラグがtrueのとき
        if(stageManager.loseFlg)
        {
            // 待機時間計算用タイマーをリセット
            dinoTimer = 0;
        }

        // 待機時間を超えたら
        if (dinoTimer > dinoIntarval)
        {
            // 移動させる
            Move();

            // 待機時間計算用タイマーを最大で止める
            dinoTimer = dinoIntarval;
        }

        // Dinoの現在位置とdinoPosの距離を計算
        float distanceToTarget = Mathf.Abs(transform.position.x - dinoPos.position.x);

        // dinoPosに到達したら
        if (distanceToTarget < 0.2f)
        {
            // 動きを止める
            StopMove();

            // 移動タイマーを加算する
            dinoMoveTimer += Time.deltaTime;
        }

        // 移動までのクールタイムを超えたら
        if (dinoMoveTimer > dinoMoveIntarval)
        {
            // 速度を加算する
            speed -= 3.0f;

            // dinoを移動させる
            Move();

            // 攻撃する
            if (attackFlg)
            {
                // 効果音を鳴らす
                audioSource.PlayOneShot(attackSE);

                // 攻撃フラグをfalseに
                attackFlg = false;
            }

            destroyTimer += Time.deltaTime;

            // 一定時間経過したら
            if (destroyTimer > destroyIntarval)
            {
                // Dinoの破壊処理
                Destroy(gameObject);
                destroyTimer = 0.0f;
            }
        }
    }

    // 移動処理
    private void Move()
    {
        // キャラクターを移動させる
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    // 移動を止める処理
    private void StopMove()
    {
        // キャラクターの移動を止める
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    // 死亡処理
    private void Die()
    {
        // Dinoの存在フラグをfalseに
        dinoSpawn.dinoFlg = false;

        // Dinoの破壊処理
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
}
