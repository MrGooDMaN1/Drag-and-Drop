using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{
    public Transform[] shelfPositions; // Define positions for placing objects on shelves

    void OnTriggerEnter2D(Collider2D collision)
    {
        DragAndDrop draggable = collision.GetComponent<DragAndDrop>();
        if (draggable != null && !draggable.enabled) // Object is not draggable anymore
        {
            float closestDistance = Mathf.Infinity;
            Transform closestShelf = null;

            foreach (Transform shelf in shelfPositions)
            {
                float distance = Vector3.Distance(shelf.position, collision.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestShelf = shelf;
                }
            }

            if (closestShelf != null)
            {
                collision.transform.position = closestShelf.position;
                draggable.enabled = false; // Lock object in place
            }
        }
    }
}
