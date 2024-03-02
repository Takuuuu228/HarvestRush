using UnityEngine;
using UnityEngine.EventSystems; // Needed for detecting drop events

public class DropSpace : MonoBehaviour
{
    public Draggable m_hasDraggable;
    public bool Droppable()
    {
        return m_hasDraggable == null;
    }

    public void SetDraggable(Draggable _draggable)
    {
        m_hasDraggable = _draggable;
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        Draggable draggable = _other.GetComponent<Draggable>();
        if(draggable != null && m_hasDraggable == draggable)
        {
            m_hasDraggable = null;
        }
    }
}
