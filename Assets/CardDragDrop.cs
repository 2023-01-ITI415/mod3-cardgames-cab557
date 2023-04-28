using Poker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDragDrop : MonoBehaviour
{
   
    private bool isDragging = false;
    private GameObject dragObject = null;
    private Vector2 dragOffset = Vector2.zero;
    private Card1 dropZone;
    public Prospector1 target;

    void Update()
    {
        if (target == isDragging)
        {
            // Move the drag object to follow the mouse position
            dragObject.transform.position = (Vector2)Input.mousePosition + dragOffset;
        }
    }

    void OnMouseDown()
    {
        // Create the drag object and set its position to match the card
        dragObject = new GameObject();
        dragObject.transform.position = transform.position;

        // Calculate the offset between the mouse position and the card position
        dragOffset = (Vector2)transform.position - (Vector2)Input.mousePosition;

        // Set dragging to true
        isDragging = true;
    }

    void OnMouseUp()
    {
        // Set dragging to false
        isDragging = false;

        // Check if the card is positioned over a valid drop zone
        RaycastHit2D hit = Physics2D.Raycast(dragObject.transform.position, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("Card"))
        {
            // Check if the drop zone is valid
             dropZone = hit.collider.GetComponent<Card1>();
            if (dropZone == true)
            {
                // Attach the card to the drop zone
                transform.SetParent(dropZone.transform);
            }
        }

        // Destroy the drag object
        Destroy(dragObject);
    }
}