using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemyWithParticles : MonoBehaviour
{
    private new ParticleSystem particleSystem;
    private EnemyController enemy;

    public Transform enemyTransform;
    public GameObject particlePrefab; // パーティクルのプレハブを指定するための変数

    private float activationSpeedThreshold = 0.5f; // パーティクルをアクティブにする速度の閾値
    public Vector3 offset = new Vector3(0.5f, 0.0f, 0.0f); // オフセット量

    private void Start()
    {
        // プレハブからパーティクルシステムを生成
        GameObject particleObject = Instantiate(particlePrefab, GetOffsetPosition(), Quaternion.identity, transform);
        particleSystem = particleObject.GetComponent<ParticleSystem>();

        // Characterスクリプトを持つコンポーネントを取得
        enemy = enemyTransform.GetComponent<EnemyController>();
    }

    private void Update()
    {
        // プレイヤーの速度が閾値以上の場合にのみパーティクルをアクティブにする
        bool shouldPlayParticle = enemy.GetCharacterSpeed() >= activationSpeedThreshold;
        if (shouldPlayParticle)
        {
            if (!particleSystem.isPlaying)
                particleSystem.Play();
        }
        else
        {
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
        Vector3 enemyPosition = enemyTransform.position;
        // プレイヤーの向きを取得
        Vector3 enemyDirection = enemyTransform.right;
        // プレイヤーの後方にオフセットした位置を計算して返す
        return enemyPosition + enemyDirection * offset.x;
    }
}
