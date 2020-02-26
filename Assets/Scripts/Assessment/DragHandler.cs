using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //look more into 

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /* The GameObject being dragged.
     * Static only allows user to drag one at a time. */
    public static GameObject itemBeingDragged;

    /* Stores start position of current gameObject.
     * Used as marker to snap object back into place if user
     * drops in invalid location. */
    Vector3 startPosition;

    /* Record what parent it is */
    Transform startParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Set item variable to current gameObject
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        // Determines if obj has been dropped into new slot
        startParent = transform.parent;
        // Don't allow collisions (a.k.a "raycasts")
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Tracks position with every frame
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Pops gameObject back into place if invalid location.
        itemBeingDragged = null;

        // If parent has not changed, snap it back into place.
        // Otherwise, go to new position.
        if(transform.parent == startParent || transform.parent == transform.root) 
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;

    }

}
