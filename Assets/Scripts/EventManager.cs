using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager current;

    //Communicates between scripts

    //Listens for events to be triggered

    //Calls events to start

    public void Awake()
    {
        current = this;
    }

    public event Action onShowTimer;
    public event Action onHideTimer;

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
}
