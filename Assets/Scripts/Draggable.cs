using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool m_isDragging; // ドラッグ中かどうかを追跡
    private Vector3 m_offset; // ドラッグ開始時のマウス位置とオブジェクト位置のオフセット
    private bool placedInDropSpace; // ドロップエリアに配置されたかどうか
    public Vector3 m_lastPosition; // ドラッグ開始前のオブジェクトの位置
    private float m_fMovementtime = 15f; // 移動にかかる時間の係数
    private System.Nullable<Vector3> m_movementDestination; // 移動先の位置（null可能）

    private static Draggable m_lastDraggable; // 最後にドラッグされたオブジェクトを追跡

    // マウスボタンが押された時の処理
    private void OnMouseDown()
    {
        placedInDropSpace = false;
        m_isDragging = true; // ドラッグ状態を真に設定
        m_lastPosition = transform.position; // 最後の位置を現在の位置に設定
        m_lastDraggable = this; // このドラッガブルを最後にドラッグされたものとして設定
        Vector3 mousePos = Input.mousePosition; // マウスのスクリーン位置を取得
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y)); // マウス位置をワールド座標に変換
        m_offset = new Vector2(transform.position.x - worldPos.x, transform.position.y - worldPos.y); // オフセットを計算
        gameObject.layer = Layer.Dragging; // オブジェクトのレイヤーをドラッグ中に変更
    }

    // マウスがドラッグされている間の処理
    private void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition; // マウスのスクリーン位置を取得
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y)); // マウス位置をワールド座標に変換

        transform.position = new Vector2(worldPos.x + m_offset.x, worldPos.y + m_offset.y); // 新しい位置にオブジェクトを移動
    }

    // マウスボタンが離された時の処理
    private void OnMouseUp()
    {
        //余裕があれば何もない場所にドロップされたときに元に戻るようにしたい

        /*if (!placedInDropSpace)
        {
            m_movementDestination = m_lastPosition;
        }*/

        m_isDragging = false; // ドラッグ状態を偽に設定
        gameObject.layer = Layer.Default; // オブジェクトのレイヤーをデフォルトに戻す
    }

    // トリガーに何かが入った時の処理
    private void OnTriggerEnter2D(Collider2D _other)
    {
        Debug.Log("呼び出し");
        placedInDropSpace = true;
        Draggable colliderDraggable = _other.GetComponent<Draggable>(); // 他のドラッガブルオブジェクト
        DropSpace dropSpace = _other.GetComponent<DropSpace>(); // ドロップスペースコンポーネント

        if (colliderDraggable != null && m_lastDraggable == this) // 他のドラッガブルと衝突した場合
        {
            // 衝突オブジェクトとの距離と方向を計算
            ColliderDistance2D colliderDistance2D = _other.Distance(GetComponent<Collider2D>());
            Vector3 diff = new Vector3(colliderDistance2D.normal.x, colliderDistance2D.normal.y) * colliderDistance2D.distance;

            transform.position = m_lastPosition;
        }
        else if (dropSpace != null) // ドロップスペースに入った場合
        {
            if (dropSpace.Droppable()) // ドロップ可能な場合
            {
                dropSpace.SetDraggable(this); // このドラッガブルをドロップスペースに設定
                m_movementDestination = _other.transform.position; // 移動先をドロップスペースの位置に設定
            }
            else
            {
                m_movementDestination = m_lastPosition; // 移動先を最後の位置に戻す
            }
        }
        else
        {
            // その他のケースでは何もしない
        }
    }

    // 固定更新処理
    private void FixedUpdate()
    {
        if (m_movementDestination.HasValue) // 移動先が設定されている場合
        {
            if (m_isDragging) // ドラッグ中の場合
            {
                m_movementDestination = null; // 移動先をリセット
                return;
            }

            if (transform.position == m_movementDestination) // 目的地に到着した場合
            {
                gameObject.layer = Layer.Default; // レイヤーをデフォルトに戻す
                m_movementDestination = null; // 移動先をリセット
            }
            else
            {
                // 現在の位置から移動先へ滑らかに移動
                transform.position = Vector3.Lerp(transform.position, m_movementDestination.Value, m_fMovementtime * Time.deltaTime);
            }
        }
    }
}
