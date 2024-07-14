using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Handles timer & score display
    [SerializeField] Text countdownText;
    
    [SerializeField] Text drinkingTutorial;
    private Button drinkingStartButton;
    [SerializeField] Text cleanupTutorial;
    private Button cleanupStartButton;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
