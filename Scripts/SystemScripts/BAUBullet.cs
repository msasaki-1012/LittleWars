using System.Collections.Generic;
using UnityEngine;

public class BAUBullet : MonoBehaviour
{
    public CharacterBAU characterBAU;       // キャラクターの取得
    public LayerMask enemyLayer;

    public int power = 2;                   // 攻撃力
    private Transform target;
    private List<Transform> enemiesInRange = new List<Transform>();     // 敵のリスト

    private float destroyTimer;             // 消滅するまでの時間

    // Start is called before the first frame update
    void Start()
    {
        // 弾を発射した瞬間に、一番近い敵を見つける
        FindNearestEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        destroyTimer += Time.deltaTime;

        // 一定時間経過したら
        if (destroyTimer > 1.0f)
        {
            // 弾を破壊する
            Destroy(gameObject);
            destroyTimer = 0.0f;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyRG enemyRG = collision.gameObject.GetComponent<EnemyRG>();
        EnemyRS enemyRS = collision.gameObject.GetComponent<EnemyRS>();
        EnemyRA enemyRA = collision.gameObject.GetComponent<EnemyRA>();
        EnemyRGU enemyRGU = collision.gameObject.GetComponent<EnemyRGU>();
        EnemyRSU enemyRSU = collision.gameObject.GetComponent<EnemyRSU>();
        EnemyRAU enemyRAU = collision.gameObject.GetComponent<EnemyRAU>();

        EnemyCastle enemyCastle = collision.gameObject.GetComponent<EnemyCastle>();

        if (enemiesInRange.Contains(collision.transform))
        {
            // 衝突した相手が敵の場合
            if (enemyRG != null)
            {
                //ダメージを与える
                enemyRG.TakeDamage(power);
            }

            // 衝突した相手が敵の場合
            if (enemyRS != null)
            {
                //ダメージを与える
                enemyRS.TakeDamage(power);
           }

            // 衝突した相手が敵の場合
            if (enemyRA != null)
            {
                //ダメージを与える
                enemyRA.TakeDamage(power);
            }

            // 衝突した相手が敵の場合
            if (enemyRGU != null)
            {
                //ダメージを与える
                enemyRGU.TakeDamage(power);
        }

            // 衝突した相手が敵の場合
            if (enemyRSU != null)
            {
                //ダメージを与える
                enemyRSU.TakeDamage(power);
            }

            // 衝突した相手が敵の場合
            if (enemyRAU != null)
            {
                //ダメージを与える
                enemyRAU.TakeDamage(power);
            }

            // 衝突した相手が敵の城の場合
            if (enemyCastle != null)
            {
                //ダメージを与える
                enemyCastle.TakeDamage(power);
            }
        }
    }

    // 一番近い敵を見つける
    void FindNearestEnemy()
    {
        // 弾の周りの半径10fの範囲内にある敵のCollider2Dを取得
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10f, enemyLayer);

        // 最も近い敵までの距離を無限大で初期化
        float nearestDistance = Mathf.Infinity;

        // 取得した敵のCollider2Dをループ
        foreach (Collider2D collider in colliders)
        {
            // 敵のTransformを取得
            Transform enemyTransform = collider.transform;

            // 弾と敵までの距離を計算
            float distanceToEnemy = Vector2.Distance(transform.position, enemyTransform.position);

            // もし計算した距離が現在の最も近い距離よりも短い場合
            if (distanceToEnemy < nearestDistance)
            {
                // 最も近い敵を更新
                nearestDistance = distanceToEnemy;
                target = enemyTransform;
            }

            // 生存している敵のみリストに追加
            if (enemyTransform.GetComponent<EnemyController>().IsAlive())
            {
                enemiesInRange.Add(enemyTransform);
            }
        }
    }
}
