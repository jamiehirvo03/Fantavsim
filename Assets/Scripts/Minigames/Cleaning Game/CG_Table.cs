using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_Table : MonoBehaviour
{
    private bool taskActive;

    private bool taskCompleted;

    private bool isInTableRange;

    private Collision collidedPlayer;

    // Start is called before the first frame update
    void Start()
    {
        taskActive = false;

        taskCompleted = false;

        CG_Events.current.onCloseCleaningTask += OnCloseCleaningTask;
    }

    // Update is called once per frame
    void Update()
    {
        if (!taskActive)
        {
            if (!taskCompleted)
            {
                if (isInTableRange)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        CG_Events.current.StartCleaningTask();

                        taskActive = true;
                    }
                }
            }            
        }
    }

    private void OnCloseCleaningTask()
    {
        taskActive = false;
        
        if (collidedPlayer.gameObject.GetComponent<CG_CleaningTask>().messList.Count == 0)
        {
            taskCompleted = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collidedPlayer = collision;

        if (collidedPlayer.gameObject.tag == "Player")
        {
            Debug.Log("Player is in range of a table, Press E to begin task");

            CG_Events.current.ShowTablePrompt();

            isInTableRange = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player has left table range");

            CG_Events.current.HideTablePrompt();

            isInTableRange = false;
        }
    }
}
