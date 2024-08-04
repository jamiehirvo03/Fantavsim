using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CG_GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CG_Events.current.onStartGame += OnStartGame;
        CG_Events.current.onPlayerFired += OnPlayerFired;
    }
    // Update is called once per frame
    void Update()
    {


    }
    private void FixedUpdate()
    {
    
    }
    private void OnStartGame()
    {

    }
    private void OnPlayerFired()
    {
        
    }
    private void OnStartCleaningTask()
    {
        
    }
    private void OnCloseCleaningTask()
    {
        
    }
}
