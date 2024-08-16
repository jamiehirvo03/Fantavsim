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

    //Events that relate to what area the mess object is hovering over
    public event Action onOverTankardBin;
    public event Action onOverScrapsBin;
    public event Action onOverDustBin;
    public event Action onOverPestBin;
    public event Action onOverNoBin;
    public event Action onOverTable;

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
    public void OverPestBin()
    {
        if (onOverPestBin != null)
        {
            onOverPestBin();
        }
    }
    public void OverNoBin()
    {
        if (onOverNoBin != null)
        {
            onOverNoBin();
        }
    }
    public void OverTable()
    {
        if (onOverTable != null)
        {
            onOverTable();
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
