using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUpButton : MonoBehaviour
{
    public Image image;                 // スピードアップボタンの画像
    public BGScroll[] bGScroll;         // 背景スクロール

    public bool speedUp = false;        // ゲーム内時間の速度が上がっているかどうかのフラグ
    private float normalSpeed = 1.0f;   // 通常時の速度
    public float blinkSpeed = 1.0f;     // 点滅の速さを調整するための変数
    public float acceleration = 2.0f;   // 加速度

    // Start is called before the first frame update
    void Start()
    {
        // ボタンの画像
        image = GameObject.Find("SpeedUpButton").GetComponent<Image>();

        // 背景スクロールを取得
        bGScroll[0] = GameObject.Find("Back 3").GetComponent<BGScroll>();

        // 背景スクロールを取得
        bGScroll[1] = GameObject.Find("Back 4").GetComponent<BGScroll>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 点滅のコルーチン
    IEnumerator Blink()
    {
        // ゲーム内時間が加速しているとき
        while (speedUp)
        {
            // 現在の透明度を取得
            float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1.0f);

            // SpriteRendererの透明度を更新
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

            // 0.02秒待つ
            yield return new WaitForSeconds(0.02f);
        }

        // 透明度を元に戻す
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
    }

    // ボタンを押したとき
    public void PushButton()
    {
        // ゲーム内時間が加速していないとき
        if (speedUp == false)
        {
            // ゲーム内時間の速度を調整
            Time.timeScale = acceleration;

            bGScroll[0].speed *= acceleration;
            bGScroll[1].speed *= acceleration;

            // 加速フラグをtrueに
            speedUp = true;

            // コルーチンを開始
            StartCoroutine(Blink());
        }
        // ゲーム内時間が加速しているとき
        else
        {
            // ゲーム内時間の速度を調整
            Time.timeScale = normalSpeed;
            bGScroll[0].speed /= acceleration;
            bGScroll[1].speed /= acceleration;

            // 加速フラグをfalseに
            speedUp = false;
        }
    }
}
