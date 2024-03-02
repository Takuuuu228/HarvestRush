using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool m_isDragging;
    private Vector3 m_offset;
    private bool placedInDropSpace;
    public Vector3 m_lastPosition;
    private float m_fMovementtime = 15f;
    private System.Nullable<Vector3> m_movementDestination;

    private static Draggable m_lastDraggable;

    private void OnMouseDown()
    {
        m_isDragging = true;
        m_lastPosition = transform.position;
        m_lastDraggable = this;
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));
        m_offset = new Vector2(transform.position.x - worldPos.x, transform.position.y - worldPos.y);
        gameObject.layer = Layer.Dragging;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));

        transform.position = new Vector2(worldPos.x + m_offset.x, worldPos.y + m_offset.y);
    }

    private void OnMouseUp()
    {
        m_isDragging = false;
        gameObject.layer = Layer.Default;
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        Draggable colliderDraggable = _other.GetComponent<Draggable>();
        DropSpace dropSpace = _other.GetComponent<DropSpace>();

        if (colliderDraggable != null && m_lastDraggable == this)
        {
            ColliderDistance2D colliderDistance2D = _other.Distance(GetComponent<Collider2D>());
            Vector3 diff = new Vector3(colliderDistance2D.normal.x, colliderDistance2D.normal.y) * colliderDistance2D.distance;

            transform.position -= diff;
        }
        else if (dropSpace != null)
        {
            if (dropSpace.Droppable())
            {
                dropSpace.SetDraggable(this);
                m_movementDestination = _other.transform.position;
            }
            else
            {
                m_movementDestination = m_lastPosition;
            }
        }
        else
        {
            // no action
        }
    }

    private void FixedUpdate()
    {
        if (m_movementDestination.HasValue)
        {
            if (m_isDragging)
            {
                m_movementDestination = null;
                return;
            }

            if(transform.position == m_movementDestination)
            {
                gameObject.layer = Layer.Default;
                m_movementDestination = null;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, m_movementDestination.Value, m_fMovementtime * Time.deltaTime);
            }
        }
    }
}
