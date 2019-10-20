using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterControl : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D character;
	public bool jump;
    public float horizontalMove;
    public float runSpeed = 30f;
    bool onground = true;
    private Vector3 velocity = Vector3.zero;




    void Start()
    {
        character = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			jump = true;
		}

	}

	public void Move(float move, bool jump)
	{
		Vector3 targetVelocity = new Vector2(move * 10f, character.velocity.y);
		character.velocity = Vector3.SmoothDamp(character.velocity, targetVelocity, ref velocity, 0.2f);
	}




    void FixedUpdate()
	{
		// Move our character
		
       
        Move(horizontalMove * Time.fixedDeltaTime, jump);
        if (jump && onground)
        {
            character.AddForce(new Vector2(0, 400));
        }
        jump = false;
	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground") onground = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground") onground = false;
    }
}
