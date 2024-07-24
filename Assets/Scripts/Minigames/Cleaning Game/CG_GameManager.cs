using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_GameManager : MonoBehaviour
{
    Rigidbody2D player;

    private float horizontal;
    private float vertical;
    private float moveLimiter = 0.7f;

    public float runSpeed = 7.0f;

    // Start is called before the first frame update
    void Start()
    {
        CG_Events.current.onStartGame += OnStartGame;
        CG_Events.current.onGameOver += OnGameOver;
        CG_Events.current.onStartCleaningTask += OnStartCleaningTask;
        CG_Events.current.onCloseCleaningTask += OnCloseCleaningTask;


        player = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        //Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); //-1 is left
        vertical = Input.GetAxisRaw("Vertical"); //-1 is down
    }
    private void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0)
        {
            //Limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        player.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
    private void OnStartGame()
    {
        //Allow player movement
    }
    private void OnGameOver()
    {
        //Disable player movement
    }
    private void OnStartCleaningTask()
    {
        //Disable player movement
    }
    private void OnCloseCleaningTask()
    {
        //Enable player movement
    }
}
