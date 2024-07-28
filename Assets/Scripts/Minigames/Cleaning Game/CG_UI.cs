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

    public TextMeshProUGUI incorrectPlacementText;

    private bool isInTask = false;
    private bool isPlayerFired = false;



    // Start is called before the first frame update
    void Start()
    {
        CG_Events.current.onGameWin += OnGameWin;
        CG_Events.current.onGameOver += OnGameOver;
        CG_Events.current.onShowTablePrompt += OnShowTablePrompt;
        CG_Events.current.onHideTablePrompt += OnHideTablePrompt;
        CG_Events.current.onStartCleaningTask += OnStartCleaningTask;
        CG_Events.current.onCloseCleaningTask += OnCloseCleaningTask;
        CG_Events.current.onMessPlacementIncorrect += OnMessPlacementIncorrect;
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

        if (isPlayerFired)
        {
            OnGameOver();
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
    private void OnShowTablePrompt()
    {

    }
    private void OnHideTablePrompt()
    {
        
    }

    private void OnGameWin()
    {
        gameUI.enabled = false;

        winScreen.enabled = true;

        isPlayerFired = false;
    }

    private void OnGameOver()
    {
        //Disable game UI
        gameUI.enabled = false;

        firedScreen.enabled = true;
    }
    
    private void OnStartCleaningTask()
    {
        //Disable table prompt
        OnHideTablePrompt();

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
    private void OnMessPlacementIncorrect()
    {
        //Display 'incorrect placement text'
        incorrectPlacementText.enabled = true;

        //Wait a few seconds then remove text
        CloseIncorrectPlacementText();
    }

    IEnumerator CloseIncorrectPlacementText()
    {
        yield return new WaitForSeconds(3);

        incorrectPlacementText.enabled = false;
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
