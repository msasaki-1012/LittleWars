using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected Rigidbody2D rb;                   // 当たり判定

    [SerializeField]
    protected int hp;                           // キャラクターのHP

    [SerializeField]
    protected float speed;                      // キャラクターの移動速度

    [SerializeField]
    protected float moveIntarval;               // 移動待機時間
    protected float moveTimer;                  // 移動計算用タイマー

    protected float addCoin;                    // 死亡したときに増やすコインの量

    public bool hit = false;                    // 攻撃が当たっているときのフラグ
    public bool die = false;                    // 死亡しているかどうかのフラグ

    [SerializeField]
    protected GameObject particlePrefab;        // パーティクルのプレハブを指定するための変数
    protected float particleLifetime = 2.0f;    // パーティクルの寿命（秒）


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 敵の生存判定
    public bool IsAlive()
    {
        return hp > 0;
    }

    // キャラクターのスピードを取得する関数
    public float GetCharacterSpeed()
    {
        return Mathf.Abs(rb.velocity.x);
    }
}
