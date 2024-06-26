using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleEnemySpawn : EnemySpawn
{
    // Start is called before the first frame update
    void Start()
    {
        // 敵の出現クールタイムの調整
        rgIntarval = 5.0f;
        rsIntarval = 8.0f;
        raIntarval = 12.0f;
        
        // 敵の城を取得
        enemyCastles[0] = GameObject.Find("BaseRed").GetComponent<EnemyCastle>();
    }

    // Update is called once per frame
    void Update()
    {
        // タイマー加算処理
        rgTimer += Time.deltaTime;
        rsTimer += Time.deltaTime;
        raTimer += Time.deltaTime;

        // 敵の死亡数が最大数を超えたら
        if (deathCharacterCount > deathCharacterCountMax)
        {
            deathCharacterCount = deathCharacterCountMax;
        }

        // 敵を出現させる
        SpownTitleEnemy();
    }

    // タイトルでのエネミー生成処理
    private void SpownTitleEnemy()
    {
        // 城の体力が無くなっていたら
        if (enemyCastles[0].die == true)
        {
            return;
        }

        // 敵の数が上限より下の時
        if (enemyCount < enemyCountMax)
        {
            //敵(rg)の生成クールタイムが終わったら
            if (rgTimer >= rgIntarval)
            {
                // 敵を生成する
                Instantiate(rg, enemySpawnPos.position, Quaternion.identity);

                //　敵の数を加算する
                enemyCount++;
                rgTimer = 0;
            }

            //敵(rs)の生成クールタイムが終わったら
            if (rsTimer >= rsIntarval)
            {
                // 敵を生成する
                Instantiate(rs, enemySpawnPos.position, Quaternion.identity);

                //　敵の数を加算する
                enemyCount++;
                rsTimer = 0;
            }
            // 敵(ra)の生成クールタイムが終わったら
            if (raTimer >= raIntarval)
            {
                // 敵を生成する
                Instantiate(ra, enemySpawnPos.position, Quaternion.identity);

                //　敵の数を加算する
                enemyCount++;
                raTimer = 0;
            }

            //rwがいないとき
            if (rwFlg == false)
            {
                // 敵(rw)の生成クールタイムが終わったら
                if (deathCharacterCount >= deathCharacterCountMax)
                {
                    // 敵を生成する
                    Instantiate(rw, rwSpawnPos.position, Quaternion.identity);

                    //　敵の数を加算する
                    enemyCount++;

                    // 敵の死亡数をリセット
                    deathCharacterCount = 0;

                    // bwの存在フラグをtrueに
                    rwFlg = true;
                }
            }
        }
    }
}
