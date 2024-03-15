using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    // 3種類のGameObjectを保持する配列を定義します。
    public GameObject[] objectsToSpawn;

    public void Start()
    {
        // Startメソッドが呼び出された時に実行されます。

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