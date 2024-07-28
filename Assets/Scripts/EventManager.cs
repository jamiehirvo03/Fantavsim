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
}
