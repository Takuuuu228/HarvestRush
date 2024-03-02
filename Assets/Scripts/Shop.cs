using UnityEngine; // UnityEngineの名前空間を使用

// SeedShopクラスを定義し、MonoBehaviourを継承する
public class Shop : MonoBehaviour
{
    // Inspectorで設定できるように、GameObject型のPrefab変数を公開する
    public GameObject prefab;

    [SerializeField] private int seedPay = 500;
    [SerializeField] private int insecticidePay = 10000;

    // OnMouseDownメソッドをオーバーライドする
    void OnMouseDown()
    {
        // プレハブが設定されている場合、そのプレハブのインスタンスを生成する
        if (prefab != null)
        {
            // Instantiateメソッドを使用して、プレハブのインスタンスを現在のGameObjectの位置に生成する
            Instantiate(prefab, transform.position, Quaternion.identity);
            if (this.gameObject.CompareTag("SeedShop"))
            {
                // GameManagerのインスタンスを使用してスコアを500減算する
                GameManager.instance.DecreaseScore(seedPay);
            }
            if (this.gameObject.CompareTag("InsecticideShop"))
            {
                GameManager.instance.DecreaseScore(insecticidePay);
            }
        }
    }
}
