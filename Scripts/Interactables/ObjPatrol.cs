using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Two ways to make the object "patrol" certain areas
 * 1. Create some marker objects, like trees or something, and designate those
 *    as elements of the Transform array moveSpots. The object will randomly
 *    move from one marker to the other
 * 2. Delete the array, and just use a regualr Transform object. Instead, create
 *    an array of points with x and y-coordinates, and use Random.Range(...)
 *    to pick points.
 */

public class RandomMovement : MonoBehaviour
{
    public float speed;
    public float startWaitTime;
    private float waitTime;

    public Transform[] moveSpots;
    private int randomSpot;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,
            moveSpots[randomSpot].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
