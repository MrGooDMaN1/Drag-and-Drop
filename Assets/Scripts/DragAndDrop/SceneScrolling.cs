using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScrolling : MonoBehaviour
{
    public float scrollSpeed = 10f;
    public float minX, maxX, minY, maxY; // Scene bounds

    private Vector2 lastTouchPosition;
    private bool isDragging;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPosition = touch.position;
                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector2 touchDelta = touch.position - lastTouchPosition;
                Vector3 position = transform.position;

                position.x = Mathf.Clamp(position.x - touchDelta.x * scrollSpeed * Time.deltaTime / Screen.width, minX, maxX);
                position.y = Mathf.Clamp(position.y - touchDelta.y * scrollSpeed * Time.deltaTime / Screen.height, minY, maxY);

                transform.position = position;

                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }
        }
    }
}
