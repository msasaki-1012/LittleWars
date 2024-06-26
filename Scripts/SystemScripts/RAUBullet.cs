using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAUBullet : MonoBehaviour
{
    public EnemyRAU enemyRAU;                 // キャラクターの取得
    public LayerMask enemyLayer;

    public int power = 2;                   // 攻撃力
    private Transform target;
    private List<Transform> charactersInRange = new List<Transform>();

    private float destroyTimer;             // 消滅するまでの時間

    // Start is called before the first frame update
    void Start()
    {
        // 弾を発射した瞬間に、一番近い敵を見つける
        FindNearestCharacter();
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
        CharacterBG charcterBG = collision.gameObject.GetComponent<CharacterBG>();
        CharacterBS charcterBS = collision.gameObject.GetComponent<CharacterBS>();
        CharacterBA charcterBA = collision.gameObject.GetComponent<CharacterBA>();
        CharacterBGU charcterBGU = collision.gameObject.GetComponent<CharacterBGU>();
        CharacterBSU charcterBSU = collision.gameObject.GetComponent<CharacterBSU>();
        CharacterBAU charcterBAU = collision.gameObject.GetComponent<CharacterBAU>();
        Castle castle = collision.gameObject.GetComponent<Castle>();

        if (charactersInRange.Contains(collision.transform))
        {
            // 衝突した相手が敵の場合
            if (charcterBG != null)
            {
                //ダメージを与える
                charcterBG.TakeDamage(power);
            }

            if (charcterBS != null)
            {
                //ダメージを与える
                charcterBS.TakeDamage(power);
            }

            if (charcterBA != null)
            {
                //ダメージを与える
                charcterBA.TakeDamage(power);
            }

            if (charcterBGU != null)
            {
                //ダメージを与える
                charcterBGU.TakeDamage(power);
            }

            if (charcterBSU != null)
            {
                //ダメージを与える
                charcterBSU.TakeDamage(power);
            }

            if (charcterBAU != null)
            {
                //ダメージを与える
                charcterBAU.TakeDamage(power);
            }

            // 衝突した相手が敵の城の場合
            if (castle != null)
            {
                //ダメージを与える
                castle.TakeDamage(power);
            }
        }
    }

    // 一番近い敵を見つける
    void FindNearestCharacter()
    {
        // 弾の周りの半径10fの範囲内にある敵のCollider2Dを取得
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10f, enemyLayer);

        // 最も近い敵までの距離を無限大で初期化
        float nearestDistance = Mathf.Infinity;

        // 取得した敵のCollider2Dをループ
        foreach (Collider2D collider in colliders)
        {
            // 敵のTransformを取得
            Transform characterTransform = collider.transform;

            // 弾と敵までの距離を計算
            float distanceToCharacter = Vector2.Distance(transform.position, characterTransform.position);

            // もし計算した距離が現在の最も近い距離よりも短い場合
            if (distanceToCharacter < nearestDistance)
            {
                // 最も近い敵を更新
                nearestDistance = distanceToCharacter;
                target = characterTransform;
            }

            // 生存している敵のみリストに追加
            if (characterTransform.GetComponent<Character>().IsAlive())
            {
                charactersInRange.Add(characterTransform);
            }
        }
    }
}
