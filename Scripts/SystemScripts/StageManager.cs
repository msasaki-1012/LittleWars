using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    private WaveManager waveManager;    // ウェーブマネージャー

    [SerializeField]
    private Image victory;              // 勝利時の画像

    [SerializeField]
    private Image defeat;               // 敗北時の画像

    [SerializeField]
    private Button button;              // ステージ選択に戻るボタン

    [SerializeField]
    private Button panelBG;             // BGのパネル
    [SerializeField]
    private Button panelBS;             // BSのパネル
    [SerializeField]
    private Button panelBA;             // BAのパネル
    [SerializeField]
    private Button panelBW;             // BWのパネル

    [SerializeField]
    private Button panelBGU;            // BGのパネル
    [SerializeField]
    private Button panelBSU;            // BSのパネル
    [SerializeField]
    private Button panelBAU;            // BAのパネル
    [SerializeField]
    private Button panelBWU;            // BWのパネル

    private AudioSource audioSource;    // オーディオソース
    public AudioClip winSE;             // 勝利音
    public AudioClip loseSE;            // 敗北音

    public bool winFlg { get; set; } = false;        // 敵に勝利した時のフラグ
    public bool winSEflg { get; set; } = false;

    public bool loseFlg { get; set; } = false;       // 敵に敗北した時のフラグ
    public bool loseSEflg { get; set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        // オーディオソースを取得
        audioSource = GetComponent<AudioSource>();

        // Waveマネージャーを取得
        waveManager = GetComponent<WaveManager>();

        // 勝利、敗北時の画像を一度アクティブにする
        victory.gameObject.SetActive(true);
        defeat.gameObject.SetActive(true);

        //  ステージセレクトへ戻るボタンを一度アクティブにする
        button.gameObject.SetActive(true);

        // 勝利、敗北時の画像を取得
        victory = GameObject.Find("Victory").GetComponent<Image>();
        defeat= GameObject.Find("Defeat").GetComponent<Image>();

        // ステージセレクトへ戻るボタンを取得
        button = GameObject.Find("StageButton").GetComponent<Button>();

        // パネルを取得
        panelBG = GameObject.Find("PanelBG").GetComponent<Button>();

        // 今いるシーンがステージのとき
        if (SceneManager.GetActiveScene().name != "Title" && SceneManager.GetActiveScene().name != "StageSelect" && SceneManager.GetActiveScene().name != "Tutorial")
        {
            panelBS = GameObject.Find("PanelBS").GetComponent<Button>();
            panelBA = GameObject.Find("PanelBA").GetComponent<Button>();
            panelBW = GameObject.Find("PanelBW").GetComponent<Button>();

            panelBGU = GameObject.Find("PanelBGU").GetComponent<Button>();
            panelBSU = GameObject.Find("PanelBSU").GetComponent<Button>();
            panelBAU = GameObject.Find("PanelBAU").GetComponent<Button>();
            panelBWU = GameObject.Find("PanelBWU").GetComponent<Button>();
        }

        // 勝利、敗北時の画像を一度消す
        victory.gameObject.SetActive(false);
        defeat.gameObject.SetActive(false);

        // ステージセレクトへ戻るボタンを一度消す
        button.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 今いるシーンがチュートリアルのとき
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            // 戦いに勝利したとき
            if (winFlg == true)
            {
                // ステージセレクトへ戻るボタンをアクティブにする
                button.gameObject.SetActive(true);

                if (winSEflg == true)
                {
                    // 効果音を鳴らす
                    audioSource.PlayOneShot(winSE);
                    winSEflg = false;
                }

                // 勝利時の画像をアクティブにする
                victory.gameObject.SetActive(true);

                // キャラクターのパネルを消す
                panelBG.gameObject.SetActive(false);
            }
            else
            {
                // 勝利時の画像を消す
                victory.gameObject.SetActive(false);

                // キャラクターのパネルを出す
                panelBG.gameObject.SetActive(true);
            }
            // 戦いに敗北したとき
            if (loseFlg == true)
            {
                if (loseSEflg == true)
                {
                    // 効果音を鳴らす
                    audioSource.PlayOneShot(loseSE);
                    loseSEflg = false;
                }

                // 画像をアクティブにする
                defeat.gameObject.SetActive(true);

                // ステージセレクトへ戻るボタンをアクティブにする
                button.gameObject.SetActive(true);

                // キャラクターのパネルを消す
                panelBG.gameObject.SetActive(false);
            }
        }
        else
        {
            // 戦いに勝利したとき
            if (winFlg == true)
            {
                // 現在のウェーブが3だったら
                if (waveManager.currentWave == waveManager.maxWave)
                {
                    if (winSEflg == true)
                    {
                        // 効果音を鳴らす
                        audioSource.PlayOneShot(winSE);
                        winSEflg = false;
                    }

                    // ステージセレクトへ戻るボタンをアクティブにする
                    button.gameObject.SetActive(true);

                    // 今いるシーンがステージ1のとき
                    if (SceneManager.GetActiveScene().name == "Stage1")
                    {
                        // ステージ1の勝利フラグをtrueに
                        ShowIconClear.stage1WinFlg = true;
                        ShowIconClear.showClearIcon1 = true;
                    }
                    if (SceneManager.GetActiveScene().name == "Stage2")
                    {
                        // ステージ2の勝利フラグをtrueに
                        ShowIconClear.stage2WinFlg = true;
                        ShowIconClear.showClearIcon2 = true;
                    }
                    if (SceneManager.GetActiveScene().name == "Stage3")
                    {
                        // ステージ3の勝利フラグをtrueに
                        ShowIconClear.stage3WinFlg = true;
                        ShowIconClear.showClearIcon3 = true;

                    }
                    if (SceneManager.GetActiveScene().name == "Stage4")
                    {
                        // ステージ4の勝利フラグをtrueに
                        ShowIconClear.stage4WinFlg = true;
                        ShowIconClear.showClearIcon4 = true;

                    }
                    if (SceneManager.GetActiveScene().name == "Stage5")
                    {
                        // ステージ5の勝利フラグをtrueに
                        ShowIconClear.stage5WinFlg = true;
                        ShowIconClear.showClearIcon5 = true;

                    }
                    if (SceneManager.GetActiveScene().name == "Stage6")
                    {
                        // ステージ6の勝利フラグをtrueに
                        ShowIconClear.stage6WinFlg = true;
                        ShowIconClear.showClearIcon6 = true;

                    }
                    if (SceneManager.GetActiveScene().name == "Stage7")
                    {
                        // ステージ7の勝利フラグをtrueに
                        ShowIconClear.stage7WinFlg = true;
                        ShowIconClear.showClearIcon7 = true;

                    }
                }

                // 勝利時の画像をアクティブにする
                victory.gameObject.SetActive(true);

                // キャラクターのパネルを消す
                panelBG.gameObject.SetActive(false);
                panelBS.gameObject.SetActive(false);
                panelBA.gameObject.SetActive(false);
                panelBW.gameObject.SetActive(false);

                panelBGU.gameObject.SetActive(false);
                panelBSU.gameObject.SetActive(false);
                panelBAU.gameObject.SetActive(false);
                panelBWU.gameObject.SetActive(false);
            }
            else
            {
                // 勝利時の画像を消す
                victory.gameObject.SetActive(false);

                // キャラクターのパネルを出す
                panelBG.gameObject.SetActive(true);
                panelBS.gameObject.SetActive(true);
                panelBA.gameObject.SetActive(true);
                panelBW.gameObject.SetActive(true);

                // 追加キャラクターのパネルを表示
                ActivePanel();
            }

            // 戦いに敗北したとき
            if (loseFlg == true)
            {
                if (loseSEflg == true)
                {
                    // 効果音を鳴らす
                    audioSource.PlayOneShot(loseSE);
                    loseSEflg = false;
                }

                // 画像をアクティブにする
                defeat.gameObject.SetActive(true);

                // ステージセレクトへ戻るボタンをアクティブにする
                button.gameObject.SetActive(true);

                // キャラクターのパネルを消す
                panelBG.gameObject.SetActive(false);
                panelBS.gameObject.SetActive(false);
                panelBA.gameObject.SetActive(false);
                panelBW.gameObject.SetActive(false);

                panelBGU.gameObject.SetActive(false);
                panelBSU.gameObject.SetActive(false);
                panelBAU.gameObject.SetActive(false);
                panelBWU.gameObject.SetActive(false);
            }
        }
    }

    // ボタンが押されたときに呼ばれる関数
    public void PushButton()
    {
        // ゲーム内時間の速度を調整
        Time.timeScale = 1;

        // ステージ選択画面に遷移
        SceneManager.LoadScene("StageSelect");
    }

    private void ActivePanel()
    {
        // キャラクターBGUを入手したら
        if (ShowIconCharacter.BGUGetFlg == true)
        {
            // キャラクターのパネルを出す
            panelBGU.gameObject.SetActive(true);
        }
        else
        {
            // キャラクターのパネルを消す
            panelBGU.gameObject.SetActive(false);
        }

        // キャラクターBSUを入手したら
        if (ShowIconCharacter.BSUGetFlg == true)
        {
            // キャラクターのパネルを出す
            panelBSU.gameObject.SetActive(true);
        }
        else
        {
            // キャラクターのパネルを消す
            panelBSU.gameObject.SetActive(false);
        }

        // キャラクターBAUを入手したら
        if (ShowIconCharacter.BAUGetFlg == true)
        {
            // キャラクターのパネルを出す
            panelBAU.gameObject.SetActive(true);
        }
        else
        {
            // キャラクターのパネルを消す
            panelBAU.gameObject.SetActive(false);
        }

        // キャラクターBWUを入手したら
        if (ShowIconCharacter.BWUGetFlg == true)
        {
            // キャラクターのパネルを出す
            panelBWU.gameObject.SetActive(true);
        }
        else
        {
            // キャラクターのパネルを消す
            panelBWU.gameObject.SetActive(false);
        }
    }
}
