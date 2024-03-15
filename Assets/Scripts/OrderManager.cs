using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    // 3種類のGameObjectを保持する配列を定義します。
    public GameObject[] objectsToSpawn;

    void Start()
    {
        InstantiateObject();

    }

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
            InstantiateObject();
        }
    }

    void InstantiateObject()
    {

        if (objectsToSpawn.Length == 0)
        {
            // objectsToSpawnが空の場合、警告をログに出力して処理を終了します。
            Debug.LogWarning("No objects set to spawn.");
            return;
        }

        // objectsToSpawnからランダムにGameObjectを1つ選択します。
        int randomIndex = Random.Range(0, objectsToSpawn.Length);
        GameObject selectedObject = objectsToSpawn[randomIndex];

        if (selectedObject != null)
        {
            // 選択されたGameObjectを現在のGameObjectの子としてインスタンス化します。
            // インスタンス化する際、親オブジェクトの位置情報を使用し、回転情報は子オブジェクトにそのまま引き継がせます。
            Instantiate(selectedObject, transform.position, Quaternion.identity, transform);
        }
        else
        {
            // 選択されたオブジェクトがnullの場合、エラーメッセージをログに出力します。
            Debug.LogError("Selected object is null.");
        }
    }
}