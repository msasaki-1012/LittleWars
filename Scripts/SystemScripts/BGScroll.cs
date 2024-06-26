using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float speed = 0.002f;            // 背景をスクロールさせるスピード                              
    private float deadLine = 22.0f;         // 背景のスクロールを開始する位置
    private float startLineX = -22.0f;      // 背景のスクロールが終了する位置のx座標
    private float startLineY = -0.5f;       // 背景のスクロールが終了する位置のy座標

    // Update is called once per frame
    void Update()
    {
        //x座標をscrollSpeed分動かす
        transform.Translate(speed, 0, 0);

        //もし背景のx座標よりdeadLineが大きくなったら
        if (transform.position.x > deadLine)
        {
            //背景をstartLineまで戻す
            transform.position = new Vector3(startLineX, startLineY, 0);
        }
    }
}
