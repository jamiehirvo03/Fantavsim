using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static event UnityAction TimerStart;
    public static event UnityAction TimerStop;
    public static event UnityAction<float> TimerUpdate;

    public static void OnTimerStart() => TimerStart.Invoke();
    public static void OnTimerStop() => TimerStop.Invoke();
    public static void OnTimerUpdate(float value) => TimerUpdate.Invoke(value);

    public void Awake()
    {
        //current = this;
    }

    //TIMER EVENTS
    public event Action onShowTimer;
    public event Action onHideTimer;
    public event Action onStartTimer;

    //POPUP TUTORIAL EVENTS
    public event Action onShowDrinkingTutorial;
    public event Action onHideDrinkingTutorial;
    public event Action onStartDrinkingGame;
    public event Action onDrinkingGameTimeOver;

    public event Action onShowCleanupTutorial;
    public event Action onHideCleanupTutorial;
    public event Action onStartCleanupGame;

    public void ShowTimer()
    {
        if (onShowTimer != null)
        {
            onShowTimer();
        }
    }
    public void HideTimer()
    {
        if(onHideTimer != null)
        {
            onHideTimer();
        }
    }

    public void StartTimer()
    {
        if (onStartTimer != null)
        {
            onStartTimer();
        }
    }

    public void DrinkingGameTimeOver()
    {
        if (onDrinkingGameTimeOver != null)
        {
            onDrinkingGameTimeOver();
        }
    }

    public void ShowDrinkingTutorial()
    {
        if(onShowDrinkingTutorial != null)
        {
            onShowDrinkingTutorial();
        }
    }
    public void HideDrinkingTutorial()
    {
        if (onHideDrinkingTutorial != null)
        {
            onHideDrinkingTutorial();
        }
    }
    public void ShowCleanupTutorial()
    {
        if (onShowCleanupTutorial != null)
        {
            onShowCleanupTutorial();
        }
    }
    public void HideCleanupTutorial()
    {
        if (onHideCleanupTutorial != null)
        {
            onHideCleanupTutorial();
        }
    }
    public void StartDrinkingGame()
    {
        if (onStartDrinkingGame != null)
        {
            onStartDrinkingGame();
        }
    }
    public void StartCleaningGame()
    {
        if (onStartCleanupGame != null)
        {
            onStartCleanupGame();
        }
    }
}
