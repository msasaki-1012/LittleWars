using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSAttackArea : MonoBehaviour
{
    public EnemyRS enemyRS;     // キャラクターの取得
    public List<CharacterBG> charactersInRangeBG = new List<CharacterBG>();     // 攻撃範囲内の敵(BG)のリスト
    public List<CharacterBS> charactersInRangeBS = new List<CharacterBS>();     // 攻撃範囲内の敵(BS)のリスト
    public List<CharacterBA> charactersInRangeBA = new List<CharacterBA>();     // 攻撃範囲内の敵(BA)のリスト
    public List<CharacterBGU> charactersInRangeBGU = new List<CharacterBGU>();  // 攻撃範囲内の敵(BG)のリスト
    public List<CharacterBSU> charactersInRangeBSU = new List<CharacterBSU>();  // 攻撃範囲内の敵(BS)のリスト
    public List<CharacterBAU> charactersInRangeBAU = new List<CharacterBAU>();  // 攻撃範囲内の敵(BA)のリスト

    public List<Castle> InRangeCastle = new List<Castle>();     // 攻撃範囲内の敵(城)のリスト

    [SerializeField]
    private int power = 3;             // キャラクターの攻撃力
    [SerializeField]
    private float atIntarval = 3.0f;   // 攻撃待機時間
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
        // 攻撃用計算タイマーの加算
        atTimer += Time.deltaTime;

        // 値がクールタイムを超えたらそれ以上大きくならないようにする
        if (atTimer >= atIntarval)
        {
            atTimer = atIntarval;

            // 攻撃可能フラグをtrueにする
            canAttack = true;
        }

        // 攻撃
        if (canAttack)
        {
            AreaAttack();
        }
    }

    // 範囲攻撃
    private void AreaAttack()
    {
        // ダメージを食らっていないとき
        if (enemyRS.hit == false)
        {
            // 死亡していたら
            if (enemyRS.die == true)
            {
                return;
            }

            // 攻撃範囲内の全ての敵(BG)に対して攻撃を行う
            foreach (CharacterBG characterBG in charactersInRangeBG)
            {
                if (characterBG == null)
                {
                    // 倒された敵をリストから削除
                    charactersInRangeBG.Remove(characterBG);
                    continue;
                }

                // 攻撃アニメーション
                enemyRS.AttackAnim();

                // 敵にダメージを与えるメソッドを呼び出す
                characterBG.TakeDamage(power);
                atTimer = 0;

                // 攻撃可能フラグをfalseにする
                canAttack = false;
            }

            // 攻撃範囲内の全ての敵(BS)に対して攻撃を行う
            foreach (CharacterBS characterBS in charactersInRangeBS)
            {
                if (characterBS == null)
                {
                    // 倒された敵をリストから削除
                    charactersInRangeBS.Remove(characterBS);
                    continue;
                }
                // 攻撃アニメーション
                enemyRS.AttackAnim();

                // 敵にダメージを与えるメソッドを呼び出す
                characterBS.TakeDamage(power);
                atTimer = 0;

                // 攻撃可能フラグをfalseにする
                canAttack = false;
            }

            // 攻撃範囲内の全ての敵(BA)に対して攻撃を行う
            foreach (CharacterBA characterBA in charactersInRangeBA)
            {
                if (characterBA == null)
                {
                    // 倒された敵をリストから削除
                    charactersInRangeBA.Remove(characterBA);
                    continue;
                }
                // 攻撃アニメーション
                enemyRS.AttackAnim();

                // 敵にダメージを与えるメソッドを呼び出す
                characterBA.TakeDamage(power);
                atTimer = 0;

                // 攻撃可能フラグをfalseにする
                canAttack = false;
            }

            // 攻撃範囲内の全ての敵(BGU)に対して攻撃を行う
            foreach (CharacterBGU characterBGU in charactersInRangeBGU)
            {
                if (characterBGU == null)
                {
                    // 倒された敵をリストから削除
                    charactersInRangeBGU.Remove(characterBGU);
                    continue;
                }
                // 攻撃アニメーション
                enemyRS.AttackAnim();

                // 敵にダメージを与えるメソッドを呼び出す
                characterBGU.TakeDamage(power);
                atTimer = 0;

                // 攻撃可能フラグをfalseにする
                canAttack = false;
            }

            // 攻撃範囲内の全ての敵(BSU)に対して攻撃を行う
            foreach (CharacterBSU characterBSU in charactersInRangeBSU)
            {
                if (characterBSU == null)
                {
                    // 倒された敵をリストから削除
                    charactersInRangeBSU.Remove(characterBSU);
                    continue;
                }
                // 攻撃アニメーション
                enemyRS.AttackAnim();

                // 敵にダメージを与えるメソッドを呼び出す
                characterBSU.TakeDamage(power);
                atTimer = 0;

                // 攻撃可能フラグをfalseにする
                canAttack = false;
            }

            // 攻撃範囲内の全ての敵(BAU)に対して攻撃を行う
            foreach (CharacterBAU characterBAU in charactersInRangeBAU)
            {
                if (characterBAU == null)
                {
                    // 倒された敵をリストから削除
                    charactersInRangeBAU.Remove(characterBAU);
                    continue;
                }
                // 攻撃アニメーション
                enemyRS.AttackAnim();

                // 敵にダメージを与えるメソッドを呼び出す
                characterBAU.TakeDamage(power);
                atTimer = 0;

                // 攻撃可能フラグをfalseにする
                canAttack = false;
            }

            // 攻撃範囲内の城に対して攻撃を行う
            foreach (Castle Castle in InRangeCastle)
            {
                if (Castle == null)
                {
                    // 倒された敵をリストから削除
                    InRangeCastle.Remove(Castle);
                    continue;
                }
                // 攻撃アニメーション
                enemyRS.AttackAnim();

                // 敵にダメージを与えるメソッドを呼び出す
                Castle.TakeDamage(power);
                atTimer = 0;

                // 攻撃可能フラグをfalseにする
                canAttack = false;
            }
        }
    }

    // 攻撃範囲に触れているとき
    private void OnTriggerStay2D(Collider2D other)
    {
        // 攻撃範囲にいる相手がキャラクターだったら
        if (other.CompareTag("Player"))
        {
            // 移動しないようにする
            enemyRS.StopMove();

            CharacterBG characterBG = other.GetComponent<CharacterBG>();

            // リストに敵を追加
            if (characterBG != null && !charactersInRangeBG.Contains(characterBG))
            {
                charactersInRangeBG.Add(characterBG);
            }

            CharacterBS characterBS = other.GetComponent<CharacterBS>();

            // リストに敵を追加
            if (characterBS != null && !charactersInRangeBS.Contains(characterBS))
            {
                charactersInRangeBS.Add(characterBS);
            }

            CharacterBA characterBA = other.GetComponent<CharacterBA>();

            // リストに敵を追加
            if (characterBA != null && !charactersInRangeBA.Contains(characterBA))
            {
                charactersInRangeBA.Add(characterBA);
            }

            CharacterBGU characterBGU = other.GetComponent<CharacterBGU>();

            // リストに敵を追加
            if (characterBGU != null && !charactersInRangeBGU.Contains(characterBGU))
            {
                charactersInRangeBGU.Add(characterBGU);
            }

            CharacterBSU characterBSU = other.GetComponent<CharacterBSU>();

            // リストに敵を追加
            if (characterBSU != null && !charactersInRangeBSU.Contains(characterBSU))
            {
                charactersInRangeBSU.Add(characterBSU);
            }

            CharacterBAU characterBAU = other.GetComponent<CharacterBAU>();

            // リストに敵を追加
            if (characterBAU != null && !charactersInRangeBAU.Contains(characterBAU))
            {
                charactersInRangeBAU.Add(characterBAU);
            }
        }

        // 攻撃範囲にいる相手が敵の城だったら
        if (other.CompareTag("Castle"))
        {
            // 移動しないようにする
            enemyRS.StopMove();

            Castle castle = other.GetComponent<Castle>();

            // リストに敵を追加
            if (castle != null && !InRangeCastle.Contains(castle))
            {
                InRangeCastle.Add(castle);
            }
        }
    }

    // 攻撃範囲から出ていくとき
    private void OnTriggerExit2D(Collider2D collision)
    {
        // リストから敵を削除
        CharacterBG characterBG = collision.GetComponent<CharacterBG>(); // 敵のスクリプトを取得

        if (characterBG != null && charactersInRangeBG.Contains(characterBG))
        {
            charactersInRangeBG.Remove(characterBG);
        }

        // リストから敵を削除
        CharacterBS characterBS = collision.GetComponent<CharacterBS>(); // 敵のスクリプトを取得

        if (characterBS != null && charactersInRangeBS.Contains(characterBS))
        {
            charactersInRangeBS.Remove(characterBS);
        }

        // リストから敵を削除
        CharacterBA characterBA = collision.GetComponent<CharacterBA>(); // 敵のスクリプトを取得

        if (characterBA != null && charactersInRangeBA.Contains(characterBA))
        {
            charactersInRangeBA.Remove(characterBA);
        }

        // リストから敵を削除
        CharacterBGU characterBGU = collision.GetComponent<CharacterBGU>(); // 敵のスクリプトを取得

        if (characterBGU != null && charactersInRangeBGU.Contains(characterBGU))
        {
            charactersInRangeBGU.Remove(characterBGU);
        }

        // リストから敵を削除
        CharacterBSU characterBSU = collision.GetComponent<CharacterBSU>(); // 敵のスクリプトを取得

        if (characterBSU != null && charactersInRangeBSU.Contains(characterBSU))
        {
            charactersInRangeBSU.Remove(characterBSU);
        }

        // リストから敵を削除
        CharacterBAU characterBAU = collision.GetComponent<CharacterBAU>(); // 敵のスクリプトを取得

        if (characterBAU != null && charactersInRangeBAU.Contains(characterBAU))
        {
            charactersInRangeBAU.Remove(characterBAU);
        }

        // リストから敵を削除
        Castle castle = collision.GetComponent<Castle>();

        if (castle != null && InRangeCastle.Contains(castle))
        {
            InRangeCastle.Remove(castle);
        }
    }
}
