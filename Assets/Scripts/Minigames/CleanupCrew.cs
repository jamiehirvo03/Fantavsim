using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanupCrew : MonoBehaviour
{
    //Handles time limit

    //Controls player movement
    Rigidbody2D player;
    

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public float runSpeed = 7.0f;

    //Generates cleanup tasks

    //Counts points

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Gives a value between -1 and 1

        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
    }

    private void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            //limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        player.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
}
