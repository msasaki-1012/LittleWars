using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGUAttackArea : MonoBehaviour
{
    public CharacterBGU characterBGU;                                 // キャラクターの取得
    public List<EnemyRG> enemiesInRangeRG = new List<EnemyRG>();     // 攻撃範囲内の敵のリスト
    public List<EnemyRS> enemiesInRangeRS = new List<EnemyRS>();     // 攻撃範囲内の敵のリスト
    public List<EnemyRA> enemiesInRangeRA = new List<EnemyRA>();     // 攻撃範囲内の敵のリスト
    public List<EnemyRGU> enemiesInRangeRGU = new List<EnemyRGU>();  // 攻撃範囲内の敵のリスト
    public List<EnemyRSU> enemiesInRangeRSU = new List<EnemyRSU>();  // 攻撃範囲内の敵のリスト
    public List<EnemyRAU> enemiesInRangeRAU = new List<EnemyRAU>();  // 攻撃範囲内の敵のリスト

    public List<EnemyCastle> enemiesInRangeCastle = new List<EnemyCastle>();     // 攻撃範囲内の敵のリスト

    [SerializeField]
    private int power;                  // キャラクターの攻撃力

    [SerializeField]
    private float atIntarval = 2.0f;    // 攻撃待機時間
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

        // 攻撃出来るとき
        if (canAttack == true)
        {
            // 攻撃する
            AreaAttack();
        }
    }

    // 範囲攻撃
    private void AreaAttack()
    {
        // ダメージを食らっていないとき
        if (characterBGU.hit == false)
        {
            // 死亡していたら
            if (characterBGU.die == true)
            {
                return;
            }

            // 攻撃範囲内の全ての敵に対して攻撃を行う
            foreach (EnemyRG enemyRG in enemiesInRangeRG)
            {
                if (enemyRG == null)
                {
                    // 倒された敵をリストから削除
                    enemiesInRangeRG.Remove(enemyRG);
                    continue;
                }
                // 攻撃アニメーション再生
                characterBGU.AttackAnim();

                // 敵にダメージを与えるメソッドを呼び出す
                enemyRG.TakeDamage(power);
                atTimer = 0;

                // 攻撃可能フラグをfalseにする
                canAttack = false;
            }

            // 攻撃範囲内の全ての敵に対して攻撃を行う
            foreach (EnemyRS enemyRS in enemiesInRangeRS)
            {
                if (enemyRS == null)
                {
                    // 倒された敵をリストから削除
                    enemiesInRangeRS.Remove(enemyRS);
                    continue;
                }
                // 攻撃アニメーション再生
                characterBGU.AttackAnim();

                // 敵にダメージを与えるメソッドを呼び出す
                enemyRS.TakeDamage(power);
                atTimer = 0;

                // 攻撃可能フラグをfalseにする
                canAttack = false;
            }

            // 攻撃範囲内の全ての敵に対して攻撃を行う
            foreach (EnemyRA enemyRA in enemiesInRangeRA)
            {
                if (enemyRA == null)
                {
                    // 倒された敵をリストから削除
                    enemiesInRangeRA.Remove(enemyRA);
                    continue;
                }
                // 攻撃アニメーション再生
                characterBGU.AttackAnim();

                // 敵にダメージを与えるメソッドを呼び出す
                enemyRA.TakeDamage(power);
                atTimer = 0;

                // 攻撃可能フラグをfalseにする
                canAttack = false;
            }

            // 攻撃範囲内の全ての敵に対して攻撃を行う
            foreach (EnemyRGU enemyRGU in enemiesInRangeRGU)
            {
                if (enemyRGU == null)
                {
                    // 倒された敵をリストから削除
                    enemiesInRangeRGU.Remove(enemyRGU);
                    continue;
                }
                // 攻撃アニメーション再生
                characterBGU.AttackAnim();

                // 敵にダメージを与えるメソッドを呼び出す
                enemyRGU.TakeDamage(power);
                atTimer = 0;

                // 攻撃可能フラグをfalseにする
                canAttack = false;
            }

            // 攻撃範囲内の全ての敵に対して攻撃を行う
            foreach (EnemyRSU enemyRSU in enemiesInRangeRSU)
            {
                if (enemyRSU == null)
                {
                    // 倒された敵をリストから削除
                    enemiesInRangeRSU.Remove(enemyRSU);
                    continue;
                }
                // 攻撃アニメーション再生
                characterBGU.AttackAnim();

                // 敵にダメージを与えるメソッドを呼び出す
                enemyRSU.TakeDamage(power);
                atTimer = 0;

                // 攻撃可能フラグをfalseにする
                canAttack = false;
            }

            // 攻撃範囲内の全ての敵に対して攻撃を行う
            foreach (EnemyRAU enemyRAU in enemiesInRangeRAU)
            {
                if (enemyRAU == null)
                {
                    // 倒された敵をリストから削除
                    enemiesInRangeRAU.Remove(enemyRAU);
                    continue;
                }
                // 攻撃アニメーション再生
                characterBGU.AttackAnim();

                // 敵にダメージを与えるメソッドを呼び出す
                enemyRAU.TakeDamage(power);
                atTimer = 0;

                // 攻撃可能フラグをfalseにする
                canAttack = false;
            }

            // 攻撃範囲内の全ての敵に対して攻撃を行う
            foreach (EnemyCastle enemyCastle in enemiesInRangeCastle)
            {
                if (enemyCastle == null)
                {
                    // 倒された敵をリストから削除
                    enemiesInRangeCastle.Remove(enemyCastle);
                    continue;
                }

                // 攻撃アニメーション再生
                characterBGU.AttackAnim();

                // 敵にダメージを与えるメソッドを呼び出す
                enemyCastle.TakeDamage(power);
                atTimer = 0;

                // 攻撃可能フラグをfalseにする
                canAttack = false;
            }
        }
    }

    // 攻撃範囲に触れているとき
    private void OnTriggerStay2D(Collider2D other)
    {
        // 攻撃範囲にいる相手が敵だったら
        if (other.CompareTag("Enemy"))
        {
            // 動きを止める
            characterBGU.StopMove();

            EnemyRG enemyRG = other.GetComponent<EnemyRG>();

            // リストに敵を追加
            if (enemyRG != null && !enemiesInRangeRG.Contains(enemyRG))
            {
                enemiesInRangeRG.Add(enemyRG);
            }

            EnemyRS EnemyRS = other.GetComponent<EnemyRS>();

            // リストに敵を追加
            if (EnemyRS != null && !enemiesInRangeRS.Contains(EnemyRS))
            {
                enemiesInRangeRS.Add(EnemyRS);
            }

            EnemyRA EnemyRA = other.GetComponent<EnemyRA>();

            // リストに敵を追加
            if (EnemyRA != null && !enemiesInRangeRA.Contains(EnemyRA))
            {
                enemiesInRangeRA.Add(EnemyRA);
            }

            EnemyRGU enemyRGU = other.GetComponent<EnemyRGU>();

            // リストに敵を追加
            if (enemyRGU != null && !enemiesInRangeRGU.Contains(enemyRGU))
            {
                enemiesInRangeRGU.Add(enemyRGU);
            }

            EnemyRSU EnemyRSU = other.GetComponent<EnemyRSU>();

            // リストに敵を追加
            if (EnemyRSU != null && !enemiesInRangeRSU.Contains(EnemyRSU))
            {
                enemiesInRangeRSU.Add(EnemyRSU);
            }

            EnemyRAU EnemyRAU = other.GetComponent<EnemyRAU>();

            // リストに敵を追加
            if (EnemyRAU != null && !enemiesInRangeRAU.Contains(EnemyRAU))
            {
                enemiesInRangeRAU.Add(EnemyRAU);
            }
        }

        // 攻撃範囲にいる相手が敵の城だったら
        if (other.CompareTag("EnemyCastle"))
        {
            // 動きを止める
            characterBGU.StopMove();

            EnemyCastle enemycastle = other.GetComponent<EnemyCastle>();

            // リストに敵を追加
            if (enemycastle != null && !enemiesInRangeCastle.Contains(enemycastle))
            {
                enemiesInRangeCastle.Add(enemycastle);
            }
        }
    }

    // 攻撃範囲から出ていくとき
    private void OnTriggerExit2D(Collider2D collision)
    {
        // リストから敵を削除
        EnemyRG enemyRG = collision.GetComponent<EnemyRG>(); // 敵のスクリプトを取得

        if (enemyRG != null && enemiesInRangeRG.Contains(enemyRG))
        {
            enemiesInRangeRG.Remove(enemyRG);
        }

        // リストから敵を削除
        EnemyRS enemyRS = collision.GetComponent<EnemyRS>(); // 敵のスクリプトを取得

        if (enemyRS != null && enemiesInRangeRS.Contains(enemyRS))
        {
            enemiesInRangeRS.Remove(enemyRS);
        }

        // リストから敵を削除
        EnemyRA enemyRA = collision.GetComponent<EnemyRA>(); // 敵のスクリプトを取得

        if (enemyRA != null && enemiesInRangeRA.Contains(enemyRA))
        {
            enemiesInRangeRA.Remove(enemyRA);
        }

        // リストから敵を削除
        EnemyRGU enemyRGU = collision.GetComponent<EnemyRGU>(); // 敵のスクリプトを取得

        if (enemyRGU != null && enemiesInRangeRGU.Contains(enemyRGU))
        {
            enemiesInRangeRGU.Remove(enemyRGU);
        }

        // リストから敵を削除
        EnemyRSU enemyRSU = collision.GetComponent<EnemyRSU>(); // 敵のスクリプトを取得

        if (enemyRSU != null && enemiesInRangeRSU.Contains(enemyRSU))
        {
            enemiesInRangeRSU.Remove(enemyRSU);
        }

        // リストから敵を削除
        EnemyRAU enemyRAU = collision.GetComponent<EnemyRAU>(); // 敵のスクリプトを取得

        if (enemyRAU != null && enemiesInRangeRAU.Contains(enemyRAU))
        {
            enemiesInRangeRAU.Remove(enemyRAU);
        }

        // リストから敵を削除
        EnemyCastle enemyCastle = collision.GetComponent<EnemyCastle>();

        if (enemyCastle != null && enemiesInRangeCastle.Contains(enemyCastle))
        {
            enemiesInRangeCastle.Remove(enemyCastle);
        }
    }
}
