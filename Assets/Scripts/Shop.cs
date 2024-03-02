using UnityEngine; // UnityEngineの名前空間を使用

// SeedShopクラスを定義し、MonoBehaviourを継承する
public class Shop : MonoBehaviour
{
    // Inspectorで設定できるように、GameObject型のPrefab変数を公開する
    public GameObject prefab;

    // OnMouseDownメソッドをオーバーライドする
    void OnMouseDown()
    {
        // プレハブが設定されている場合、そのプレハブのインスタンスを生成する
        if (prefab != null)
        {
            // Instantiateメソッドを使用して、プレハブのインスタンスを現在のGameObjectの位置に生成する
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
