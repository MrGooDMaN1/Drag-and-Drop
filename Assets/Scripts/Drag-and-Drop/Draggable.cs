using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool IsDragging;
    public Vector3 LastPosition;

    private Collider2D _collider;
    private DragController _dragController;
    private float _movementTime = 15f;
    private System.Nullable<Vector3> _movementDestianation;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _dragController = FindObjectOfType<DragController>();
    }

    private void FixedUpdate()
    {
        if(_movementDestianation.HasValue)
        {
            if(IsDragging)
            {
                _movementDestianation = null;
                return;
            }

            if (transform.position == _movementDestianation)
            {
                gameObject.layer = Layer.Default;
                _movementDestianation = null;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, _movementDestianation.Value, Time.fixedDeltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Draggable collidedDraggble = other.GetComponent<Draggable>();

        if(collidedDraggble != null && _dragController.LastDragged.gameObject == gameObject)
        {
            ColliderDistance2D colliderDistance2D = other.Distance(_collider);
            Vector3 diff = new Vector3(colliderDistance2D.normal.x, colliderDistance2D.normal.y) * colliderDistance2D.distance;
            transform.position -= diff;
        }

        if (other.CompareTag("DropValid"))
        {
            _movementDestianation = other.transform.position;
        }
        else if (other.CompareTag("DropInvalid"))
        {
            _movementDestianation = LastPosition;
        }
    }
}
