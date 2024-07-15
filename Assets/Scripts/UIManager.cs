using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Handles timer & score display
    private float startingTime = 120f;
    public float currentTime;
    [SerializeField] private float minutes;
    [SerializeField] private float seconds;

    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI drinkingTutorial;
    public TextMeshProUGUI cleanupTutorial;

    private Button drinkingStartButton;
    private Button cleanupStartButton;

    public GameObject DrinkingGame;

    //Handles pause menu

    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.onShowTimer += OnShowTimer;
        EventManager.current.onHideTimer += OnHideTimer;

        EventManager.current.onShowDrinkingTutorial += OnShowDrinkingTutorial;
        EventManager.current.onHideDrinkingTutorial += OnHideDrinkingTutorial;
       
        EventManager.current.onShowCleanupTutorial += OnShowCleanupTutorial;
        EventManager.current.onHideCleanupTutorial += OnHideCleanupTutorial;

        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {        
        if (currentTime > 0)
        {
            UpdateTimer();
        }
        if (currentTime <= 0)
        {
            countdownText.text = "0:00";
        }

        if ((minutes == 0) && (seconds <= 10))
        {
            countdownText.color = Color.red;
        }

        if ((minutes > 1) && (seconds > 10))
        {
            countdownText.color = Color.white;
        }
    }

    private void StartButtonClicked()
    {
        countdownText.enabled = true;
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

    private void OnShowTimer()
    {
        //Debug log to test that event is working
        Debug.Log("Timer UI is showing");

        countdownText.enabled = true;
    }

    private void OnHideTimer()
    {
        //Debug log to test that event is working
        Debug.Log("Timer UI is hidden");

        countdownText.enabled = false;
    }

    private void OnShowDrinkingTutorial()
    {
        Debug.Log("Drinking Tutorial is showing");

        drinkingTutorial.enabled = true;
        drinkingStartButton.enabled = true;
    }
   
    private void OnHideDrinkingTutorial()
    {
        Debug.Log("Drinking Tutorial is hidden");

        drinkingTutorial.enabled = false;
        drinkingStartButton.enabled = false;
    }

    private void OnShowCleanupTutorial()
    {
        Debug.Log("Cleanup Tutorial is showing");

        cleanupTutorial.enabled = true;
        cleanupStartButton.enabled = true;
    }

    private void OnHideCleanupTutorial()
    {
        Debug.Log("Cleanup Tutorial is hidden");

        cleanupTutorial.enabled = false;
        cleanupStartButton.enabled = false;
    }
}
