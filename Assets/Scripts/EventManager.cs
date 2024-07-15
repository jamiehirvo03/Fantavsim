using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager current;

    public void Awake()
    {
        current = this;
    }

    public event Action onShowTimer;
    public event Action onHideTimer;
    public event Action onStartTimer;
    public event Action onTimeOver;

    public event Action onShowDrinkingTutorial;
    public event Action onHideDrinkingTutorial;
    public event Action onStartDrinkingGame;

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

    public void TimeOver()
    {
        if (onTimeOver != null)
        {
            onTimeOver();
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
