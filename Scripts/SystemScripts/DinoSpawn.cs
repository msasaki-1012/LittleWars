using UnityEngine;
using TMPro;

public class DinoSpawn : MonoBehaviour
{  
    public GameObject dino;           // Dino
    public Transform dinoSpawnPos;    // Dinoの出現位置
    public bool dinoFlg = false;
    public bool dinoSpawnFlg = false;   // Dinoの生成フラグ

    // Dinoの生成関数
    public void SpawnDino()
    {
        // クリアしていないときかつDinoがいないとき
        if (dinoFlg == false)
        {
            // Dinoを生成する
            Instantiate(dino, dinoSpawnPos.position, Quaternion.identity);
            dinoSpawnFlg = true;

            // 存在フラグをtrueに
            dinoFlg = true;
        }
    }
}
