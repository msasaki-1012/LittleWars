using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonHover : MonoBehaviour
{
    [SerializeField]
    private Button[] buttons;                   // ボタンの参照
    [SerializeField]
    private Transform[] positions;              // 画像を出す位置
    [SerializeField]
    private TextMeshProUGUI[] stageTexts;       // ボタンにカーソルを合わせた時に出るテキスト
    [SerializeField]
    private Image hoverImage;                   // 矢印の画像
    [SerializeField]
    private TextMeshProUGUI stageSelectText;    // ステージセレクトのテキスト
    private int currentIndex = -1;              // 現在のボタンのインデックスを追跡

    private AudioSource audioSource;            // オーディオソース
    public AudioClip selectSE;                  // 選択音

    void Start()
    {
        // コンポーネントを取得
        stageSelectText = GameObject.Find("StageSelect").GetComponent<TextMeshProUGUI>();

        // オーディオソースを取得
        audioSource = GetComponent<AudioSource>();
        
        // ボタンごとのテキストを表示する
        foreach (var stageText in stageTexts)
        {
            stageText.gameObject.SetActive(true);
        }

        // ボタンごとにテキストを取得
        stageTexts[0] = GameObject.Find("StageText1").GetComponent<TextMeshProUGUI>();
        stageTexts[1] = GameObject.Find("StageText2").GetComponent<TextMeshProUGUI>();
        stageTexts[2] = GameObject.Find("StageText3").GetComponent<TextMeshProUGUI>();
        stageTexts[3] = GameObject.Find("StageText4").GetComponent<TextMeshProUGUI>();
        stageTexts[4] = GameObject.Find("StageText5").GetComponent<TextMeshProUGUI>();
        stageTexts[5] = GameObject.Find("StageText6").GetComponent<TextMeshProUGUI>();
        stageTexts[6] = GameObject.Find("StageText7").GetComponent<TextMeshProUGUI>();

        // 矢印の画像を非表示にする
        hoverImage.gameObject.SetActive(false);

        // ステージセレクトのテキストを表示する
        stageSelectText.gameObject.SetActive(true);

        // ボタンごとのテキストを非表示にする
        foreach (var stageText in stageTexts)
        {
            stageText.gameObject.SetActive(false);
        }
    }

    // マウスカーソルがボタンの上にある場合
    public void OnMouseOverButton(int buttonIndex)
    {
        if (currentIndex != buttonIndex)
        {
            // 効果音を鳴らす
            audioSource.PlayOneShot(selectSE);

            // ステージセレクトのテキストを非表示にする
            stageSelectText.gameObject.SetActive(false);

            // 対応するボタンのテキストを表示する
            stageTexts[buttonIndex].gameObject.SetActive(true);

            // 矢印の画像を対応する位置に表示する
            hoverImage.transform.position = positions[buttonIndex].position;
            hoverImage.gameObject.SetActive(true);

            currentIndex = buttonIndex;
        }
    }

    // マウスカーソルがボタンの上から離れた場合
    public void OnMouseExitButton()
    {
        if (currentIndex != -1)
        {
            // 対応するボタンのテキストを非表示にする
            stageTexts[currentIndex].gameObject.SetActive(false);

            // 矢印の画像を非表示にする
            hoverImage.gameObject.SetActive(false);

            // ステージセレクトのテキストを表示する
            stageSelectText.gameObject.SetActive(true);

            currentIndex = -1;
        }
    }
}