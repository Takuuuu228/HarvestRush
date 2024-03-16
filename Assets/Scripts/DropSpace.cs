using UnityEngine;
using UnityEngine.EventSystems; // イベントシステムを使用するために必要

// ドロップエリアを表すクラス
public class DropSpace : MonoBehaviour
{
    public Draggable m_hasDraggable; // 現在このドロップスペースに配置されているドラッガブルオブジェクト
    public AudioSource audioSource;

    // ドロップ可能かどうかを判断する関数
    public bool Droppable()
    {
        // m_hasDraggableがnullならば、ドロップスペースは空であり、新しいオブジェクトを受け入れることができる
        return m_hasDraggable == null;
    }

    // ドロップスペースにドラッガブルオブジェクトを設定する関数
    public void SetDraggable(Draggable _draggable)
    {
        // 引数として受け取ったドラッガブルオブジェクトをm_hasDraggableに設定
        m_hasDraggable = _draggable;
        if (m_hasDraggable.gameObject.tag == "Seed")
        {
            audioSource.Play();
        }

    }

    // 他のコライダーがこのドロップスペースから出て行った時に呼ばれる関数
    private void OnTriggerExit2D(Collider2D _other)
    {
        // イベントが発生したオブジェクトからDraggableコンポーネントを取得
        Draggable draggable = _other.GetComponent<Draggable>();

        // 取得したDraggableがnullでない、かつ、現在のドロップスペースにあるDraggableがこのDraggableと同じであれば
        if (draggable != null && m_hasDraggable == draggable)
        {
            // ドロップスペースからDraggableを削除する（nullを設定）
            m_hasDraggable = null;
        }
    }
}
