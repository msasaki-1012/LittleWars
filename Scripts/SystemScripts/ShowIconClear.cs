using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowIconClear : MonoBehaviour
{
    public Image []IconClearImage;              // クリアマークの画像

    public static bool stage1WinFlg = false;    // ステージ1のクリアフラグ
    public static bool stage2WinFlg = false;    // ステージ2のクリアフラグ
    public static bool stage3WinFlg = false;    // ステージ3のクリアフラグ
    public static bool stage4WinFlg = false;    // ステージ4のクリアフラグ
    public static bool stage5WinFlg = false;    // ステージ5のクリアフラグ
    public static bool stage6WinFlg = false;    // ステージ6のクリアフラグ
    public static bool stage7WinFlg = false;    // ステージ7のクリアフラグ

    public static bool showClearIcon1 = false;  // ステージ1のアイコン表示フラグ
    public static bool showClearIcon2 = false;  // ステージ2のアイコン表示フラグ
    public static bool showClearIcon3 = false;  // ステージ3のアイコン表示フラグ
    public static bool showClearIcon4 = false;  // ステージ4のアイコン表示フラグ
    public static bool showClearIcon5 = false;  // ステージ5のアイコン表示フラグ
    public static bool showClearIcon6 = false;  // ステージ6のアイコン表示フラグ
    public static bool showClearIcon7 = false;  // ステージ7のアイコン表示フラグ

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ステージクリアのアイコンを出す
        ShowClearIcon();

        // それぞれのステージをクリアしたとき
        if (stage4WinFlg == true)
        {
            ShowIconCharacter.BGUGetFlg = Random.value > 0.5f;
        }
        if (stage5WinFlg == true)
        {
            ShowIconCharacter.BSUGetFlg = Random.value > 0.5f;
        }
        if (stage6WinFlg == true)
        {
            ShowIconCharacter.BAUGetFlg = Random.value > 0.5f;
        }
        if (stage7WinFlg == true)
        {
            ShowIconCharacter.BWUGetFlg = Random.value > 0.5f;
        }
    }

    // ステージクリアのアイコンを出す処理
    private void ShowClearIcon()
    {
        if (showClearIcon1 == true)
        {
            IconClearImage[0].gameObject.SetActive(true);
            stage1WinFlg = false;
        }
        if (showClearIcon2 == true)
        {
            IconClearImage[1].gameObject.SetActive(true);
            stage2WinFlg = false;
        }
        if (showClearIcon3 == true)
        {
            IconClearImage[2].gameObject.SetActive(true);
            stage3WinFlg = false;
        }
        if (showClearIcon4 == true)
        {
            IconClearImage[3].gameObject.SetActive(true);
            stage4WinFlg = false;
        }
        if (showClearIcon5 == true)
        {
            IconClearImage[4].gameObject.SetActive(true);
            stage5WinFlg = false;
        }
        if (showClearIcon6 == true)
        {
            IconClearImage[5].gameObject.SetActive(true);
            stage6WinFlg = false;
        }
        if (showClearIcon7 == true)
        {
            IconClearImage[6].gameObject.SetActive(true);
            stage7WinFlg = false;
        }
    }
}
