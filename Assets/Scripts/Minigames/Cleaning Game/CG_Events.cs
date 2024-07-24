using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_Events : MonoBehaviour
{
    public static CG_Events current;

    public void Awake()
    {
        current = this;
    }

    public event Action onStartGame;
    public event Action onGameOver;
    public event Action onStartCleaningTask;
    public event Action onCloseCleaningTask;

    public void StartGame()
    {
        if (onStartGame != null)
        {
            onStartGame();
        }
    }
    public void GameOver()
    {
        if (onGameOver != null)
        {
            onGameOver();
        }
    }
    public void StartCleaningTask()
    {
        if (onStartCleaningTask != null)
        {
            onStartCleaningTask();
        }
    }
    public void CloseCleaningTask()
    {
        if (onCloseCleaningTask != null)
        {
            onCloseCleaningTask();
        }
    }
}
