using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* For Player of the game.  
 * Must have a RigidBody2D component
 * Controlled by up/down/left/right arrows
 */

public class Move : MonoBehaviour
{
    public float speed;

    private Vector2 moveVelocity;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * speed;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity*Time.fixedDeltaTime);
    }

    // "collects" coins by destroying them, removing them from the game screen
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }
    }
}
