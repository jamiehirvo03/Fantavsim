using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CG_UI : MonoBehaviour
{
    //Add at top    
    public ScoreTrack ScoreTrack;

    public Canvas tutorialPopup;
    public Canvas gameOverPopup;

    public TextMeshProUGUI pointsDisplay;
    private int pointsCount = 0;

    public float startingTime = 60f;
    private float minutes;
    private float seconds;
    public TextMeshProUGUI countdownText;

    [SerializeField] private float currentTime;

    public TextMeshProUGUI incorrectPlacementText;

    private bool isGameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        //Add at void Start
        ScoreTrack = FindObjectOfType<ScoreTrack>();

        CG_Events.current.onGameOver += OnGameOver;
        CG_Events.current.onMessPlacementCorrect += OnMessPlacementCorrect;
        CG_Events.current.onMessPlacementIncorrect += OnMessPlacementIncorrect;

        gameOverPopup.enabled = false;

        currentTime = startingTime;

        incorrectPlacementText.enabled = false;

        countdownText.enabled = false;

        pointsDisplay.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStarted)
        {
            if (countdownText.enabled == true)
            {
                if (currentTime > 0)
                {
                    UpdateTimer();
                }
            }
            if (currentTime <= 0)
            {
                countdownText.text = "0:00";
                CG_Events.current.GameOver();
            }
        }
        if (seconds < 10)
        {
            countdownText.color = Color.white;
        }
        if ((minutes == 0) && (seconds <= 10))
        {
            countdownText.color = Color.red;
        }
    }

    public void StartButtonClicked()
    {
        //Start the game
        CG_Events.current.StartGame();

        //Disable tutorial UI
        tutorialPopup.enabled = false;

        countdownText.enabled = true;
        countdownText.color = Color.white;

        pointsDisplay.enabled = true;

        isGameStarted = true;

    }
    private void UpdateTimer()
    {
        currentTime -= Time.deltaTime;

        minutes = Mathf.FloorToInt(currentTime / 60);
        seconds = Mathf.FloorToInt(currentTime % 60);

        if (seconds < 10)
        {
            countdownText.text = $"{minutes}:0{seconds}";
        }
        else
        {
            countdownText.text = $"{minutes}:{seconds}";
        }
    }
    private void OnGameOver()
    {
        countdownText.enabled = false;

        pointsDisplay.enabled = false;

        gameOverPopup.enabled = true;

        //Add where game ends
        ScoreTrack.messValue -= 1;

        if (ScoreTrack.atTable1 == true)
        {
            ScoreTrack.dirtyTable1 = false;
            ScoreTrack.atTable1 = false;
        }


        else if (ScoreTrack.atTable2 == true)
        {
            ScoreTrack.dirtyTable2 = false;

            ScoreTrack.atTable2 = false;
        }
    }

    private void OnMessPlacementCorrect()
    {
        pointsCount++;

        pointsDisplay.text = $"Points: {pointsCount}";
    }

    private void OnMessPlacementIncorrect()
    {
        Debug.Log("Incorrect Placment Text is showing");

        //Display 'incorrect placement text'
        incorrectPlacementText.enabled = true;

        //Wait a few seconds then remove text
        
        StartCoroutine (WaitForTextRemoval());
    }
    private IEnumerator WaitForTextRemoval()
    {
        yield return new WaitForSeconds(3);

        Debug.Log("Incorrect Placement Text has been removed after 3 seconds");

        incorrectPlacementText.enabled = false;
    }
}
