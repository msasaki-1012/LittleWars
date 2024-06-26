using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacterWithParticles : MonoBehaviour
{
    private new ParticleSystem particleSystem;              // パーティクルを動かすための変数
    private Character character;                            // キャラクターの変数

    public Transform characterTransform;                    // キャラクターのトランスフォーム用変数
    public GameObject particlePrefab;                       // パーティクルのプレハブを指定するための変数

    private float activationSpeedThreshold = 0.5f;          // パーティクルをアクティブにする速度の閾値
    public Vector3 offset = new Vector3(-0.5f, 0.0f, 0.0f); // オフセット量

    private void Start()
    {
        // プレハブからパーティクルシステムを生成
        GameObject particleObject = Instantiate(particlePrefab, GetOffsetPosition(), Quaternion.identity, transform);
        particleSystem = particleObject.GetComponent<ParticleSystem>();

        // キャラクターのトランスフォームを取得
        characterTransform = GetComponent<Transform>();

        // Characterスクリプトを持つコンポーネントを取得
        character = characterTransform.GetComponent<Character>();

        // パーティクルを取得
        particlePrefab = GetComponent<GameObject>();
    }

    private void Update()
    {
        // プレイヤーの速度が閾値以上の場合にのみパーティクルをアクティブにする
        bool shouldPlayParticle = character.GetCharacterSpeed() >= activationSpeedThreshold;
        if (shouldPlayParticle)
        {
            // パーティクルを出す
            if (!particleSystem.isPlaying)
                particleSystem.Play();
        }
        else
        {
            // パーティクルを止める
            if (particleSystem.isPlaying)
                particleSystem.Stop();
        }

        // パーティクルの位置を更新
        particleSystem.transform.position = GetOffsetPosition();
    }

    // プレイヤーの少し後方にオフセットした位置を返す
    private Vector3 GetOffsetPosition()
    {
        // プレイヤーの位置を取得
        Vector3 characterPosition = characterTransform.position;
        // プレイヤーの向きを取得
        Vector3 characterDirection = characterTransform.right;
        // プレイヤーの後方にオフセットした位置を計算して返す
        return characterPosition + characterDirection * offset.x;
    }
}
