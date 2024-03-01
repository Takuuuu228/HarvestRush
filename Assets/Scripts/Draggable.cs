using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool isDragging = false;
    private GameObject clone;
    private Vector3 offset;

    void Update()
    {
        if (isDragging)
        {
            // マウスのワールド座標にオフセットを加えて、クローンの位置を更新
            Vector3 mousePosition = GetMouseWorldPosition() + offset;
            clone.transform.position = mousePosition;
        }
    }

    void OnMouseDown()
    {
        // ドラッグ開始時にオブジェクトをクローンし、ドラッグを開始
        isDragging = true;
        clone = Instantiate(gameObject, transform.position, Quaternion.identity);
        clone.name = gameObject.name + " clone";
        // クローンにDraggableコンポーネントを追加
        if (clone.GetComponent<Draggable>() == null)
        {
            clone.AddComponent<Draggable>();
        }

        // マウスのワールド座標を取得し、オフセットを計算
        Vector3 mousePosition = GetMouseWorldPosition();
        offset = transform.position - mousePosition;
        // ドラッグ中はクローンを無視するレイヤーに設定
        clone.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    void OnMouseUp()
    {
        // マウスを離したときにドラッグを終了
        StopDragging();
    }

    // マウスのワールド座標を取得するヘルパーメソッド
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.nearClipPlane; // カメラからの距離
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    // ドラッグを終了するメソッド
    public void StopDragging()
    {
        isDragging = false;
        // クローンがDropSpaceの子でない場合は破棄
        if (clone.transform.parent == null || !clone.transform.parent.GetComponent<DropSpace>())
        {
            Destroy(clone);
        }
    }
}
