using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
   // Treated as a variable outside the script
   public GameObject item
    {
        get
        {
            // If this.slot has a child, return the first child.
            // Otherwise, return null.
            if(transform.childCount>0)
            {
                // Note: GetChild() returns a transform.
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    /* Note: DragHandler goes on obj being dragged,
       DropHandler goes on receiving obj (this.slot). */
    public void OnDrop(PointerEventData eventData)
    {
        /* If this slot (the receiving obj) doesn't already have an item,
           change the itemBeingDragged's transform's parent
           to the current transform (a.k.a reset parent). */
        if(!item)
        {
            DragHandler.itemBeingDragged.transform.SetParent(transform);
            /* ExecuteHierarchy goes through every gameObj
               above the gameObj you called ExecuteHierarchy on
               until it finds a gameObj that can handle it. */
            // Updates text!!
            // NOTE: look more into lambda functions
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
        }
    }
}
