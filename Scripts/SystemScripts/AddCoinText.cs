using UnityEngine;

public class AddCoinText : MonoBehaviour
{
    public GameObject addCoinTextPrefab;         // コインのテキストプレハブ

    // Start is called before the first frame update
    void Start()
    {
        // テキストを1秒後に削除
        Destroy(addCoinTextPrefab.gameObject, 0.5f);
    }
}
