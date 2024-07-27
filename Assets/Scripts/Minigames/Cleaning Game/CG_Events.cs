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
    public event Action onTaskFailed;
    public event Action onCreateMessItem;
    public event Action onOverTankardBin;
    public event Action onOverScrapsBin;
    public event Action onOverDustBin;
    public event Action onOverRodentBin;
    public event Action onMessPlacementCorrect;
    public event Action onMessPlacementIncorrect;

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
    public void TaskFailed()
    {
        if (onTaskFailed != null)
        {
            onTaskFailed();
        }
    }
    public void CreateMessItem()
    {
        if (onCreateMessItem != null)
        {
            onCreateMessItem();
        }
    }
    public void OverTankardBin()
    {
        if (onOverTankardBin != null)
        {
            onOverTankardBin();
        }
    }
    public void OverScrapsBin()
    {
        if (onOverScrapsBin != null)
        {
            onOverScrapsBin();
        }
    }
    public void OverDustBin()
    {
        if (onOverDustBin != null)
        {
            onOverDustBin();
        }
    }
    public void OverRodentBin()
    {
        if (onOverRodentBin != null)
        {
            onOverRodentBin();
        }
    }
    public void MessPlacementCorrect()
    {
        if (onMessPlacementCorrect != null)
        {
            onMessPlacementCorrect();
        }
    }
    public void MessPlacementIncorrect()
    {
        if (onMessPlacementIncorrect != null)
        {
            onMessPlacementIncorrect();
        }
    }
}
