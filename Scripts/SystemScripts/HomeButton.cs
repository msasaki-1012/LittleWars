using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    [SerializeField]
    private Fade[] fade;        //FadeCanvas取得

    private AudioSource audioSource;    // オーディオソース
    public AudioClip enterSE;           // 決定音

    //フェード時間（秒）
    [SerializeField]
    private float fadeTime;
    private float normalSpeed = 1.0f;   // 通常時の速度

    // Start is called before the first frame update
    void Start()
    {
        // オーディオソースを取得
        audioSource = GetComponent<AudioSource>();

        // ゲーム内時間の速度を調整
        Time.timeScale = normalSpeed;

        //シーン開始時にフェードを掛ける
        fade[0].FadeOut(fadeTime);
    }

    // ステージセレクトへ戻るボタンを押した時の処理
    public void StageSelectSceneTransition()
    {
        //フェードを掛けてからシーン遷移する
        fade[1].FadeIn(fadeTime, () =>
        {
            SceneManager.LoadScene("StageSelect");
        });
    }

    // 各ボタンを押した時の処理
    public void StageSceneTransition(int num)
    {
        // 効果音を鳴らす
        audioSource.PlayOneShot(enterSE);

        //フェードを掛けてからシーン遷移する
        fade[1].FadeIn(fadeTime, () =>
        {
            // それぞれのステージに遷移
            SceneManager.LoadScene("Stage" + num);
        });
    }

    // タイトルへ戻るボタンを押した時の処理
    public void TitleSceneTransition()
    {
        // 効果音を鳴らす
        audioSource.PlayOneShot(enterSE);

        Title.titleCount++;

        //フェードを掛けてからシーン遷移する
        fade[2].FadeIn(fadeTime, () =>
        {
            SceneManager.LoadScene("Title");
        });
    }
}
