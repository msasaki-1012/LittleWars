using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    public Scrollbar scrollbar;
    public float scrollSpeed = 0.1f; // 調整可能なスクロール速度

    void Start()
    {
        // スクロールバーの値を調整して、上端から始まるようにする
        scrollbar.value = 1.0f;
    }

    private void Update()
    {
        //float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        //scrollbar.value = Mathf.Clamp(scrollbar.value - scrollDelta * scrollSpeed, 0f, 1f);

        //if (scrollbar.value <= 1.0f)
        //{
        //    scrollbar.value = 1.0f;
        //}

        //if (scrollbar.value <= 0.0f)
        //{
        //    scrollbar.value = 0.0f;
        //}
    }
}