using UnityEngine;

public class DamageText : MonoBehaviour
{
    public GameObject damageTextPrefab;         // ダメージのテキスト

    // Start is called before the first frame update
    void Start()
    {
        // テキストを1秒後に削除
        Destroy(damageTextPrefab.gameObject, 0.3f);
    }
}
