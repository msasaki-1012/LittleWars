using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAUAttackArea : MonoBehaviour
{
    public EnemyRAU enemyRAU;     // キャラクターの取得

    [SerializeField]
    private float atIntarval = 2.0f;   // 攻撃待機時間
    private float atTimer;              // 攻撃計算用タイマー
    private bool canAttack = true;      // 攻撃可能かどうかのフラグ

    // Start is called before the first frame update
    void Start()
    {
        // 攻撃クールタイムの設定
        atTimer = atIntarval;
    }

    // Update is called once per frame
    void Update()
    {
        // 攻撃計算用タイマーの加算
        atTimer += Time.deltaTime;

        // 値がクールタイムを超えたらそれ以上大きくならないようにする
        if (atTimer >= atIntarval)
        {
            atTimer = atIntarval;
            // 攻撃可能フラグをtrueにする
            canAttack = true;
        }
    }

    // 範囲攻撃
    private void AreaAttack()
    {
        // ダメージを食らっていないとき
        if (enemyRAU.hit == false)
        {
            // 死亡していたら
            if (enemyRAU.die == true)
            {
                return;
            }
            // 攻撃アニメーション再生
            enemyRAU.AttackAnim();

            atTimer = 0;

            // 攻撃可能フラグをfalseにする
            canAttack = false;
        }
    }

    // 攻撃範囲に触れているとき
    private void OnTriggerStay2D(Collider2D other)
    {
        // 当たった相手が敵か敵の城だったら
        if (other.CompareTag("Player") || other.CompareTag("Castle"))
        {
            enemyRAU.StopMove();
            // 攻撃
            if (canAttack)
            {
                AreaAttack();
            }
        }
    }
}
