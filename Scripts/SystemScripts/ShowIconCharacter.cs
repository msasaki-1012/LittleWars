using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowIconCharacter : MonoBehaviour
{
    public Image[] IconCharacterImage;      // クリアマークの画像

    public static bool BGUGetFlg = false;   // キャラクターのゲットフラグ
    public static bool BSUGetFlg = false;   // キャラクターのゲットフラグ
    public static bool BAUGetFlg = false;   // キャラクターのゲットフラグ
    public static bool BWUGetFlg = false;   // キャラクターのゲットフラグ

    private Color originalColor;            // 初期の色

    private Color targetColor = Color.black;  // 黒色

    // Start is called before the first frame update
    void Start()
    {
        // 初期の色を取得
        originalColor = IconCharacterImage[0].color;
    }

    // Update is called once per frame
    void Update()
    {
        // キャラクターBGUを入手したら
        if (BGUGetFlg == true)
        {
            IconCharacterImage[0].color = originalColor;
        }
        else
        {
            IconCharacterImage[0].color = targetColor;
        }
        // キャラクターBSUを入手したら
        if (BSUGetFlg == true)
        {
            IconCharacterImage[1].color = originalColor;
        }
        else
        {
            IconCharacterImage[1].color = targetColor;
        }
        // キャラクターBAUを入手したら
        if (BAUGetFlg == true)
        {
            IconCharacterImage[2].color = originalColor;
        }
        else
        {
            IconCharacterImage[2].color = targetColor;
        }
        // キャラクターBWUを入手したら
        if (BWUGetFlg == true)
        {
            IconCharacterImage[3].color = originalColor;
        }
        else
        {
            IconCharacterImage[3].color = targetColor;
        }
    }
}
