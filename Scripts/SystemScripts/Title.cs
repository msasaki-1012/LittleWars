using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField]
    private Fade[] fade;        //FadeCanvas取得
    
    [SerializeField]
    private float fadeTime;     //フェード時間取得（秒）

    public static int titleCount = 0;   // タイトル画面に入った回数

    private AudioSource audioSource;    // オーディオソース
    public AudioClip enterSE;           // 決定音

    // Start is called before the first frame update
    void Start()
    {
        // オーディオソースを取得
        audioSource = GetComponent<AudioSource>();

        // タイトルに戻ってきたとき
        if (titleCount > 0)
        {
            //シーン開始時にフェードを掛ける
            fade[0].FadeOut(fadeTime);
        }
    }

    // ボタンが押されたときに呼ばれる関数
    public void PushButton()
    {
        // 効果音を鳴らす
        audioSource.PlayOneShot(enterSE);

        // タイトルから初めて移動するとき
        if (titleCount == 0)
        {
            // フェードインを実行
            fade[1].FadeIn(fadeTime, () =>
            {
                // チュートリアルへ遷移
                SceneManager.LoadScene("Tutorial");
            });
        }
        else
        { 
            // フェードインを実行
            fade[1].FadeIn(fadeTime, () =>
            {
                // ステージへ遷移
                SceneManager.LoadScene("StageSelect");
            });
        }
    }
}
