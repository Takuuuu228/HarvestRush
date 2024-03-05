using UnityEngine;
using System.Collections;

public class SeedManager : MonoBehaviour
{
    // cropという名前のGameObjectを設定するためのpublic変数。
    // この変数にはUnityEditorからPrefab化したGameObjectがアタッチされる。
    public GameObject crop;
    [SerializeField] private float waitTime;
    [SerializeField] private Draggable draggable;

    void Update()
    {
        HandlePlacementChanged(draggable.placedInDropSpace);
    }

    // DraggableスクリプトのplacedInDropSpaceの状態が変わった時に呼ばれるメソッド。
    private void HandlePlacementChanged(bool placed)
    {
        // もしplacedInDropSpaceがtrueになったら、Coroutineを開始する。
        if (placed)
        {
            StartCoroutine(RemoveAndSpawnCoroutine());
        }
    }

    // GameObjectを削除し、指定されたPrefabを生成するCoroutine。
    private IEnumerator RemoveAndSpawnCoroutine()
    {
        // 5秒待つ。
        yield return new WaitForSeconds(waitTime);

        // このGameObjectを削除。
        Destroy(gameObject);

        // cropのPrefabを現在の位置に生成。
        Instantiate(crop, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    }
}

