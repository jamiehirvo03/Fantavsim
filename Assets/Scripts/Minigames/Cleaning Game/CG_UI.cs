using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CG_UI : MonoBehaviour
{
    public Canvas tutorialPopup;
    public Canvas gameUI;
    public Canvas taskWindow;
    public Canvas taskFailedPopup;
    public Canvas winScreen;
    public Canvas firedScreen;

    public TextMeshProUGUI strikeDisplay;
    private int strikeCount = 0;

    public Slider taskTimer;
    public float taskTimeLimit = 10f;
    [SerializeField] private float currentTaskTime;

    private bool isInTask = false;
    private bool isPlayerFired = false;



    // Start is called before the first frame update
    void Start()
    {
        CG_Events.current.onGameOver += OnGameOver;
        CG_Events.current.onStartCleaningTask += OnStartCleaningTask;
        CG_Events.current.onCloseCleaningTask += OnCloseCleaningTask;
        CG_Events.current.onTaskFailed += OnTaskFailed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInTask)
        {
            //Decrease the timer over time but only when the player is in a task
            currentTaskTime -= Time.deltaTime;

            //Display timer value to slider UI
            taskTimer.value = currentTaskTime;
        }
    }

    public void StartButtonClicked()
    {
        //Start the game
        CG_Events.current.StartGame();

        //Disable tutorial UI
        tutorialPopup.enabled = false;

        //Enable game UI
        gameUI.enabled = true;

    }

    private void OnGameOver()
    {
        //Disable game UI
        gameUI.enabled = false;

        //Display end screen
        winScreen.enabled = true;

        if (isPlayerFired)
        {
            firedScreen.enabled = true;
        }
    }
    
    private void OnStartCleaningTask()
    {
        //Display cleaning window
        taskWindow.enabled = true;
        
        //Display cleaning timer bar
        taskTimer.enabled = true;

        //Reset the timer for new task
        currentTaskTime = taskTimeLimit;

        isInTask = true;

    }

    private void OnCloseCleaningTask()
    {
        isInTask = false;

        //Disable cleaning border
        taskWindow.enabled = false;

        //Disable cleaning timer bar
        taskTimer.enabled = false;
    }

    private void OnTaskFailed()
    {
        //Add to the strike count
        strikeCount++;

        //Display failed popup
        taskFailedPopup.enabled = true;

        //WAIT 5 SECONDS
        taskFailedPopup.enabled = false;

        //Close the task
        OnCloseCleaningTask();
    }
}
