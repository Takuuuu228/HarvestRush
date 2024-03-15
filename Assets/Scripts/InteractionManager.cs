using UnityEngine;

// このスクリプトはGameObjectにアタッチされることを想定しています。
public class InteractionManager : MonoBehaviour
{
    // OrderManagerへの参照
    public OrderManager orderManager;

    // Triggerに入った際に呼ばれるメソッド
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // このGameObjectの子オブジェクトの中で最初に見つかったもののタグを取得
        // 子オブジェクトが存在しない場合、処理をスキップ
        if (transform.childCount == 0) return;

        GameObject firstChild = transform.GetChild(0).gameObject;

        // ぶつかったオブジェクトのタグと、このGameObjectの子オブジェクトのタグが一致するかチェック
        if (collision.gameObject.tag == firstChild.tag)
        {
            // 一致した場合、ぶつかったGameObjectとこのGameObjectの最初の子オブジェクトを消去
            Destroy(collision.gameObject); // ぶつかったGameObjectを削除
            Destroy(firstChild); // このGameObjectの最初の子オブジェクトを削除

            // OrderManagerのStartメソッドを呼び出し、初期化を再度行う
            orderManager.Start();
        }
    }
}
