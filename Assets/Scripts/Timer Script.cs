using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using TMPro;
using Unity.VisualScripting;
using System;

public class TimerScript : MonoBehaviour
{
    private TMP_Text _timerText;

    //types of timers to be selected
    enum TimerType { countdown, stopwatch}
    [SerializeField] private TimerType timerType;

    //how long can time run for
    [SerializeField] public float timeToDisplay = 10.0f;

    private bool _isRunning;

   private void Start()
    {
        //getting text for display
        _timerText = GetComponent<TMP_Text>();
        EventManager.OnTimerStart();
    }

    private void OnEnable()
    {
       //what timer does when it is on
        EventManager.TimerStart += EventManagerOnTimerStart;
        EventManager.TimerStop += EventManagerOnTimerStop;
        EventManager.TimerUpdate += EventManagerOnTimerUpdate;
    }

    private void OnDisable()
    {
        //what timer does when it is off
        EventManager.TimerStart -= EventManagerOnTimerStart;
        EventManager.TimerStop -= EventManagerOnTimerStop;
        EventManager.TimerUpdate -= EventManagerOnTimerUpdate;
    }

  
    private void EventManagerOnTimerStart() => _isRunning = true;

    private void EventManagerOnTimerStop() => _isRunning = false;

    private void EventManagerOnTimerUpdate(float value) => timeToDisplay += value;

    // Update is called once per frame
    void Update()
    {
        //checks to see if timer is running
        if (!_isRunning) return;
        //checks to see if timer has reached time cap
        if (timerType == TimerType.countdown && timeToDisplay < 0.0f)
        {
            EventManager.OnTimerStop();
            return;
        }

        timeToDisplay += timerType == TimerType.countdown ? -Time.deltaTime : Time.deltaTime;

        //counter being displayed to on screen UI
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeToDisplay);
        _timerText.text = timeToDisplay.ToString();//timeSpan.ToString(@"mm/:ss/:ff");
    }
}
