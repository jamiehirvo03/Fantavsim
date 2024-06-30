using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Handles game startup

    //Manages Saving/Loading

    //Tells EventManager to start everything


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Test that input is working
            Debug.Log("Show timer button has been pressed");

            EventManager.current.ShowTimer();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //Test that input is working
            Debug.Log("Hide timer button has been pressed");

            EventManager.current.HideTimer();
        }
    }
    
}
