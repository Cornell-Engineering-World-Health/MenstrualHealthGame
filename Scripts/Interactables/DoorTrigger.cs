using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Code for some sort of "door" object that opens and closes when the player
 * or target object gets within a certain boundary
 *
 * Door object should have a Collider2D component with "trigger" checked,
 * and another Collider2D component without "trigger" checked
 */


public class DoorTrigger : MonoBehaviour
{
    #region Attributes
    public Transform door;

    public Vector2 closedPosition = new Vector2(1.05f, -.5f);
    public Vector2 openPosition = new Vector2(1.05f, 1.5f);

    public float openSpeed;

    private bool open; // default is false
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        door = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            door.position = Vector2.Lerp(door.position, openPosition,
                Time.deltaTime * openSpeed);
        }
        else
        {
            door.position = Vector2.Lerp(door.position, closedPosition,
                Time.deltaTime * openSpeed);
        }
    }

    // Opens door when an object tagged as "Ball" is within boundary
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            OpenDoor();
        }
    }

    // When the "Ball" object exits boundary of trigger, close door
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            CloseDoor();
        }
    }

    // When colliding with a "Player" object, remove Player from screen
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }

    public void CloseDoor()
    {
        open = false;
    }

    public void OpenDoor()
    {
        open = true;
    }

}
