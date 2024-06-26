using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Tutorial : MonoBehaviour
{
    private AudioSource audioSource;        // オーディオソース
    public static bool firstTutorialflg = false;    // キャラクター生成のチュートリアルが終わったかどうかのフラグ
    public static bool tutorialflg = false;         // 戦闘チュートリアルのフラグ
    public static bool tutorialButtonflg = false;   // チュートリアルでキャラクター生成ボタンを押したかどうかのフラグ
    private bool hasStartedTutorial = false;  // チュートリアルを開始したかどうかのフラグ

    [SerializeField]
    private GameObject tutorialBack;   // チュートリアルの背景

    [SerializeField]
    private TextMeshProUGUI tutorialText;   // チュートリアルのテキスト

    private enum TutorialStep
    {
        Welcome,
        GoldTutorial,
        CharacterIconClick,
        AutoAttack,
        BattleTutorial,
        None
    }

    private TutorialStep currentStep = TutorialStep.Welcome;

    // Start is called before the first frame update
    void Start()
    {
        // オーディオソースを取得
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // チュートリアルを開始していないとき
        if (hasStartedTutorial == false)
        {
            StartTutorial();
            // チュートリアルを始める
            hasStartedTutorial = true;
        }

        if (firstTutorialflg == true)
        {
            // ボタンが押されたとき
            if (tutorialButtonflg == true)
            {
                // チュートリアルを進める
                currentStep = TutorialStep.AutoAttack;
            }
        }
    }

    private void StartTutorial()
    {
        // コルーチンを開始
        StartCoroutine(ActiveTutorialTextCoroutine());
    }

    // ボタンをクリックしたときに次のチュートリアルを始める関数
    public void OnNextButtonClicked()
    {
        tutorialButtonflg = true;
    }
   
    IEnumerator ActiveTutorialTextCoroutine()
    {
        // チュートリアルのテキストを出す
        tutorialBack.gameObject.SetActive(true);

        switch (currentStep)
        {
            case TutorialStep.Welcome:
                tutorialText.text = "LittleWarsへ\nようこそ！！！";
                yield return new WaitForSeconds(5f);

                tutorialText.text = "戦闘についての\nチュートリアルを\n開始します。";
                yield return new WaitForSeconds(5f);

                hasStartedTutorial = false;
                currentStep++;
                break;

            case TutorialStep.GoldTutorial:
                tutorialText.text = "左下で現在の所持ゴールドが\n確認できます。";
                yield return new WaitForSeconds(5f);

                tutorialText.text = "所持ゴールドがキャラクターの出撃コストより多いと\nキャラクターを出撃できます。";
                yield return new WaitForSeconds(5f);
                hasStartedTutorial = false;
                currentStep++;

                break;

            case TutorialStep.CharacterIconClick:
                tutorialText.text = "下にあるキャラクターの\nアイコンをクリック\nしてみよう！";
                yield return new WaitForSeconds(1f);
                hasStartedTutorial = false;
                firstTutorialflg = true;

                break;

                // キャラクターのボタンが押された後
            case TutorialStep.AutoAttack:
                tutorialText.text = "キャラクターは敵に近づくと\n自動で攻撃します。";
                yield return new WaitForSeconds(5f);
                hasStartedTutorial = false;
                firstTutorialflg = false;
                tutorialflg = true;
                currentStep++;

                break;

            case TutorialStep.BattleTutorial:
                tutorialText.text = "敵を倒すことで\n所持ゴールドが増えます。";
                yield return new WaitForSeconds(5f);

                tutorialText.text = "上にあるのが城の体力です。";
                yield return new WaitForSeconds(5f);

                tutorialText.text = "敵の城を破壊すると\nクリアとなります。";
                yield return new WaitForSeconds(5f);

                tutorialText.text = "所持Gが無くならないように\n気を付けて敵を\n倒してみよう！！！";

                yield return new WaitForSeconds(5f);

                hasStartedTutorial = false;
                currentStep++;

                break;

            case TutorialStep.None:
                // チュートリアルのテキストを消す
                tutorialBack.gameObject.SetActive(false);
                tutorialText.text = "";
                break;

            default:
                break;
        }
    }
}
