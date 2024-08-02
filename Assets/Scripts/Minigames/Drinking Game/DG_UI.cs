using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DG_UI : MonoBehaviour
{
    private float startingTime = 60f;
    public float currentTime;
    [SerializeField] private float minutes;
    [SerializeField] private float seconds;
    public TextMeshProUGUI countdownText;

    private Button startButton;
    public Canvas tutorialPopup;

    public GameObject DrinkingGame;

    // Start is called before the first frame update
    void Start()
    {
        DG_Events.current.onAddBonusTime += OnAddBonusTime;

        currentTime = startingTime;

        tutorialPopup.enabled = true;
        countdownText.enabled = false;
    }

    // Update is called once per frame
    void Update()
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
            DG_Events.current.TimeOver();
        }


        //DEFAULT TIMER COLOUR
        if (seconds > 10)
        {
            countdownText.color = Color.white;
        }

        //URGENT TIMER COLOUR
        if ((minutes == 0) && (seconds <= 10))
        {
            countdownText.color = Color.red;
        }
    }

    public void StartButtonClicked()
    {
        ShowTimer();
        HideTutorial();
        DG_Events.current.StartGame();
    }

    private void ShowTimer()
    {
        Debug.Log("Timer UI is showing");

        countdownText.enabled = true;
    }

    private void UpdateTimer()
    {
        currentTime -= Time.deltaTime;

        minutes = Mathf.FloorToInt(currentTime / 60);
        seconds = Mathf.FloorToInt(currentTime % 60);

        if (seconds < 10)
        {
            //Changes format of timer so that seconds still shows as two digits on UI
            countdownText.text = $"{minutes}:0{seconds}";
        }
        else
        {
            countdownText.text = $"{minutes}:{seconds}";
        }
    }

    private void OnAddBonusTime()
    {
        currentTime += 10;
    }

    private void HideTimer()
    {
        Debug.Log("Timer UI is hidden");

        countdownText.enabled = false;
    }

    private void ShowTutorial()
    {
        Debug.Log("Drinking tutorial is showing");

        tutorialPopup.enabled = true;
        startButton.enabled = true;
    }

    private void HideTutorial()
    {
        Debug.Log("Drinking tutorial is hidden");

        tutorialPopup.enabled = false;
    }
}
