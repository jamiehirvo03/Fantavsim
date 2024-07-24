using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_UI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CG_Events.current.onGameOver += OnGameOver;
        CG_Events.current.onStartCleaningTask += OnStartCleaningTask;
        CG_Events.current.onCloseCleaningTask += OnCloseCleaningTask;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGameOver()
    {
        //Disable game UI
        
        //Display end screen
    }
    
    private void OnStartCleaningTask()
    {
        //Display cleaning border

        //Display background dimmer
        
        //Display cleaning timer bar
    }

    private void OnCloseCleaningTask()
    {
        //Disable cleaning border

        //Disable background dimmer

        //Disable cleaning timer bar
    }

}
