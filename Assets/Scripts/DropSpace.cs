using UnityEngine;

public class DropSpace : MonoBehaviour
{
    // Detects collision with another GameObject
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Draggable draggableComponent = collision.gameObject.GetComponent<Draggable>();
        if (draggableComponent != null)
        {
            // Set the GameObject as a child of the drop space
            collision.gameObject.transform.SetParent(transform, false);
            draggableComponent.StopDragging();
        }
    }
}
