using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BWAttackArea : MonoBehaviour
{
    public CharacterBW characterBW;                                  // キャラクターの取得
    public List<EnemyRG> enemiesInRangeRG = new List<EnemyRG>();     // 攻撃範囲内の敵のリスト
    public List<EnemyRS> enemiesInRangeRS = new List<EnemyRS>();     // 攻撃範囲内の敵のリスト
    public List<EnemyRA> enemiesInRangeRA = new List<EnemyRA>();     // 攻撃範囲内の敵のリスト
    public List<EnemyRGU> enemiesInRangeRGU = new List<EnemyRGU>();  // 攻撃範囲内の敵のリスト
    public List<EnemyRSU> enemiesInRangeRSU = new List<EnemyRSU>();  // 攻撃範囲内の敵のリスト
    public List<EnemyRAU> enemiesInRangeRAU = new List<EnemyRAU>();  // 攻撃範囲内の敵のリスト

    public GameObject particlePrefab;           // パーティクルのプレハブを指定するための変数
    public float particleLifetime = 2.0f;       // パーティクルの寿命（秒）

    [SerializeField]
    private int power = 1;              // キャラクターの攻撃力
    [SerializeField]
    private float atIntarval = 3.0f;    // 攻撃待機時間
    private float atTimer;              // 攻撃計算用タイマー
    private bool canAttack = false;      // 攻撃可能かどうかのフラグ

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

        // 値がクールタイムを超えたら
        if (atTimer >= atIntarval)
        {
            // それ以上大きくならないようにする
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
        if (characterBW.hit == false)
        {
            // 死亡していたら
            if (characterBW.die == true)
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
                characterBW.AttackAnim();

                // 敵にダメージを与えるメソッドを呼び出す
                enemyRG.TakeDamage(power);
               
                atTimer = 0;

                // パーティクルを生成して敵の位置に表示
                if (particlePrefab != null)
                {
                    // パーティクル再生
                    GameObject particleInstance = Instantiate(particlePrefab, enemyRG.transform.position, Quaternion.identity);

                    // パーティクルを一定時間後に削除
                    Destroy(particleInstance, particleLifetime);
                }

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
                characterBW.AttackAnim();

                // 敵にダメージを与えるメソッドを呼び出す
                enemyRS.TakeDamage(power);

                atTimer = 0;

                // パーティクルを生成して敵の位置に表示
                if (particlePrefab != null)
                {
                    // パーティクル再生
                    GameObject particleInstance = Instantiate(particlePrefab, enemyRS.transform.position, Quaternion.identity);

                    // パーティクルを一定時間後に削除
                    Destroy(particleInstance, particleLifetime);
                }

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
                characterBW.AttackAnim();

                // 敵にダメージを与えるメソッドを呼び出す
                enemyRA.TakeDamage(power);

                atTimer = 0;

                // パーティクルを生成して敵の位置に表示
                if (particlePrefab != null)
                {
                    // パーティクル再生
                    GameObject particleInstance = Instantiate(particlePrefab, enemyRA.transform.position, Quaternion.identity);

                    // パーティクルを一定時間後に削除
                    Destroy(particleInstance, particleLifetime);
                }

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
                characterBW.AttackAnim();

                // 敵にダメージを与えるメソッドを呼び出す
                enemyRGU.TakeDamage(power);

                atTimer = 0;

                // パーティクルを生成して敵の位置に表示
                if (particlePrefab != null)
                {
                    // パーティクル再生
                    GameObject particleInstance = Instantiate(particlePrefab, enemyRGU.transform.position, Quaternion.identity);

                    // パーティクルを一定時間後に削除
                    Destroy(particleInstance, particleLifetime);
                }

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
                characterBW.AttackAnim();

                // 敵にダメージを与えるメソッドを呼び出す
                enemyRSU.TakeDamage(power);

                atTimer = 0;

                // パーティクルを生成して敵の位置に表示
                if (particlePrefab != null)
                {
                    // パーティクル再生
                    GameObject particleInstance = Instantiate(particlePrefab, enemyRSU.transform.position, Quaternion.identity);

                    // パーティクルを一定時間後に削除
                    Destroy(particleInstance, particleLifetime);
                }

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
                characterBW.AttackAnim();

                // 敵にダメージを与えるメソッドを呼び出す
                enemyRAU.TakeDamage(power);

                atTimer = 0;

                // パーティクルを生成して敵の位置に表示
                if (particlePrefab != null)
                {
                    // パーティクル再生
                    GameObject particleInstance = Instantiate(particlePrefab, enemyRAU.transform.position, Quaternion.identity);

                    // パーティクルを一定時間後に削除
                    Destroy(particleInstance, particleLifetime);
                }

                // 攻撃可能フラグをfalseにする
                canAttack = false;
            }
        }
    }

    // 攻撃範囲に触れているとき
    private void OnTriggerStay2D(Collider2D other)
    {
        // 当たった相手が敵だったら
        if (other.CompareTag("Enemy"))
        {
            // 移動を止める
            characterBW.StopMove();

            EnemyRG enemyRG = other.GetComponent<EnemyRG>();

            // リストに敵を追加
            if (enemyRG != null && !enemiesInRangeRG.Contains(enemyRG))
            {
                enemiesInRangeRG.Add(enemyRG);
            }

            EnemyRS enemyRS = other.GetComponent<EnemyRS>();

            // リストに敵を追加
            if (enemyRS != null && !enemiesInRangeRS.Contains(enemyRS))
            {
                enemiesInRangeRS.Add(enemyRS);
            }

            EnemyRA enemyRA = other.GetComponent<EnemyRA>();

            // リストに敵を追加
            if (enemyRA != null && !enemiesInRangeRA.Contains(enemyRA))
            {
                enemiesInRangeRA.Add(enemyRA);
            }

            EnemyRGU enemyRGU = other.GetComponent<EnemyRGU>();

            // リストに敵を追加
            if (enemyRGU != null && !enemiesInRangeRGU.Contains(enemyRGU))
            {
                enemiesInRangeRGU.Add(enemyRGU);
            }

            EnemyRSU enemyRSU = other.GetComponent<EnemyRSU>();

            // リストに敵を追加
            if (enemyRSU != null && !enemiesInRangeRSU.Contains(enemyRSU))
            {
                enemiesInRangeRSU.Add(enemyRSU);
            }

            EnemyRAU enemyRAU = other.GetComponent<EnemyRAU>();

            // リストに敵を追加
            if (enemyRAU != null && !enemiesInRangeRAU.Contains(enemyRAU))
            {
                enemiesInRangeRAU.Add(enemyRAU);
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
    }
}
