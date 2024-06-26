using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCharacterSpawn : CharacterSpawn
{
    private Castle castle;      // 味方の城

    // Start is called before the first frame update
    void Start()
    {
        // 味方の出現クールタイムの調整
        bgIntarval = 5.0f;
        bsIntarval = 8.0f;
        baIntarval = 12.0f;

        // 味方側の城を取得
        castle = GameObject.Find("BaseBlue").GetComponent<Castle>();
    }

    // Update is called once per frame
    void Update()
    {
        // タイマー加算処理
        bgTimer += Time.deltaTime;
        bsTimer += Time.deltaTime;
        baTimer += Time.deltaTime;

        // 敵を出現させる
        SpownCharacter();
    }

    // 味方キャラクター生成処理
    public void SpownCharacter()
    {
        // 城の体力が無くなっていたら
        if(castle.die == true)
        {
            // 実行しない
            return;
        }

        // キャラクターの数が上限より下の時
        if (characterCount < characterCountMax)
        {
            // キャラクター(bg)の生成クールタイムが終わったら
            if (bgTimer >= bgIntarval)
            {
                // キャラクターを生成させる
                Instantiate(bg, characterSpawnPos.position, Quaternion.identity);

                // キャラクターの数を加算する
                characterCount += 1;
                bgTimer = 0;
            }

            //　キャラクター(bs)の生成クールタイムが終わったら
            if (bsTimer >= bsIntarval)
            {
                // キャラクターを生成させる
                Instantiate(bs, characterSpawnPos.position, Quaternion.identity);

                // キャラクターの数を加算する
                characterCount += 1;
                bsTimer = 0;
            }

            // キャラクター(ra)の生成クールタイムが終わったら
            if (baTimer >= baIntarval)
            {
                // キャラクターを生成させる
                Instantiate(ba, characterSpawnPos.position, Quaternion.identity);

                // キャラクターの数を加算する
                characterCount += 1;
                baTimer = 0;
            }

            // bwがいないとき
            if (bwFlg == false)
            {
                // キャラクター(bw)の生成クールタイムが終わったら
                if (deathCharacterCountMax <= deathCharacterCount)
                {
                    // キャラクターを生成させる
                    Instantiate(bw, bwSpawnPos.position, Quaternion.identity);

                    // キャラクターの数を加算する
                    characterCount += 1;
                    //bwTimer = 0;

                    deathCharacterCount = 0;

                    // bwの存在フラグをtrueに
                    bwFlg = true;
                }
            }
        }
    }
}
