using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return; // Avoid interacting through UI
        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.gravityScale = 0; // Disable gravity while dragging
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        mousePosition.z = 0; // Keep the object in 2D plane
        transform.position = mousePosition;
    }

    void OnMouseUp()
    {
        isDragging = false;
        rb.gravityScale = 1; // Re-enable gravity after releasing the object
    }
}
