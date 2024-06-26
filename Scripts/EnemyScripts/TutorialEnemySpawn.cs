using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemySpawn : EnemySpawn
{
    private float tutorialRGIntarval = 8.0f;       // RGのクールタイム

    // Start is called before the first frame update
    void Start()
    {
        // 敵の城を取得
        enemyCastles[0] = GameObject.Find("BaseRed").GetComponent<EnemyCastle>();
    }

    // Update is called once per frame
    void Update()
    {
        //// タイマー加算処理
        //rgTimer += Time.deltaTime;
       
        // 敵の死亡数が最大数を超えたら
        if (deathCharacterCount > deathCharacterCountMax)
        {
            deathCharacterCount = deathCharacterCountMax;
        }

        // キャラクターのアイコンをクリックしたら
        if (Tutorial.tutorialflg == true && Tutorial.tutorialButtonflg == true)
        {
            // タイマー加算処理
            rgTimer += Time.deltaTime;

            // 敵を出現させる
            SpownTutorialEnemy();
        }
    }

    // タイトルでのエネミー生成処理
    private void SpownTutorialEnemy()
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
            if (rgTimer >= tutorialRGIntarval)
            {
                // 敵を生成する
                Instantiate(rg, enemySpawnPos.position, Quaternion.identity);

                //　敵の数を加算する
                enemyCount++;
                rgTimer = 0;
            }
        }
    }
}
