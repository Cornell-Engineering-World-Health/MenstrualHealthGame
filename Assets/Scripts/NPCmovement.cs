
/* REFRENCES:
 * 1) https://www.youtube.com/watch?v=5owq_9lptdE (NPC Movement)
 * 2) https://www.youtube.com/watch?v=rTVZ1O12Vec (Constraining Movement)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCmovement : MonoBehaviour
{
    public float moveSpeed;

    public bool isWalking;

    public Collider2D walkZone;

    public float waitTime;
    private float waitCounter;

    public float walkTime;
    private float walkCounter;

    private int WalkDirection;
    private bool hasWalkZone;

    private Vector2 minWalkPoint;
    private Vector2 maxWalkPoint;

    private Rigidbody2D myRigidbody;


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        waitCounter = waitTime; //set waitCounter to waitTime, where waitTime is input
        walkCounter = walkTime; //set waitCounter to waitTime, where walkTime is input

        ChooseDirection(); //choose a random direction (left or right)

        if (walkZone != null) //check if there is a walk zone
        {
            minWalkPoint = walkZone.bounds.min; //bottom left corner of zone
            maxWalkPoint = walkZone.bounds.max; //bottom right corner of zone
            hasWalkZone = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            walkCounter -= Time.deltaTime; //subtract "1 second"

            switch(WalkDirection) //0 for left, 1 for right
            {
                case 0: //left
                    myRigidbody.velocity = new Vector2(-moveSpeed, 0); //calculates leftwards velocity
                    
                    if (hasWalkZone && transform.position.x < minWalkPoint.x) //NPC is outside of left border of walkzone
                    {
                        isWalking = false; 
                        waitCounter = waitTime; //reset waitCounter
                    }
                    
                    break;
                case 1: //right
                    
                    myRigidbody.velocity = new Vector2(moveSpeed, 0); //calculates rightwards velocity

                    if (hasWalkZone && transform.position.x > maxWalkPoint.x) //NPC is outside of right border of walkzone
                    {
                        isWalking = false;
                        waitCounter = waitTime; //reset waitCounter
                    }
                    
                    break;
            }

            if (walkCounter < 0) 
            {
                isWalking = false;
                waitCounter = waitTime; //reset waitCounter
            }
        }
        else
        {
            waitCounter -= Time.deltaTime; //subtract "1 second"

            myRigidbody.velocity = Vector2.zero; //not moving

            if(waitCounter < 0)
            {
                ChooseDirection(); //choose new direction to walk in
            }
        }
    }
    public void ChooseDirection()
    {
        WalkDirection = Random.Range(0, 2); // left : 0, right : 1
        isWalking = true; 
        walkCounter = walkTime; //reset walkCounter
    }
}
