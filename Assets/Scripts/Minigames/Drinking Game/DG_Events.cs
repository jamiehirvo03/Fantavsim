using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DG_Events : MonoBehaviour
{
    public static DG_Events current;

    public void Awake()
    {
        current = this;
    }

    public event Action onStartGame;
    public event Action onTimeOver;
    public event Action onAddBonusTime;

    public event Action onGenerateStartingDrinks;
    public event Action onDrinkEmpty;
    public event Action onNextDrink;

    public event Action onCurrentDrinkRegular;
    public event Action onCurrentDrinkGolden;

    public event Action onIdle;
    public event Action onDrinking;
    public event Action onSpilling1;
    public event Action onChugging;
    public event Action onSpilling2;

    public void StartGame()
    {
        if (onStartGame != null)
        {
            onStartGame();
        }
    }
    public void TimeOver()
    {
        if (onTimeOver != null)
        {
            onTimeOver();
        }
    }
    public void AddBonusTime()
    {
        if (onAddBonusTime != null)
        {
            onAddBonusTime();
        }
    }
    public void GenerateStartingDrinks()
    {
        if (onGenerateStartingDrinks != null)
        {
            onGenerateStartingDrinks();
        }
    }
    public void DrinkEmpty()
    {
        if (onDrinkEmpty != null)
        {
            onDrinkEmpty();
        }
    }
    public void NextDrink()
    {
        if (onNextDrink != null)
        {
            onNextDrink();
        }
    }
    public void CurrentDrinkRegular()
    {
        if (onCurrentDrinkRegular != null)
        {
            onCurrentDrinkRegular();
        }
    }
    public void CurrentDrinkGolden()
    {
        if (onCurrentDrinkGolden != null)
        {
            onCurrentDrinkGolden();
        }
    }
    public void Idle()
    {
        if (onIdle != null)
        {
            onIdle();
        }
    }
    public void Drinking()
    {
        if (onDrinking != null)
        {
            onDrinking();
        }
    }
    public void Spilling1()
    {
        if (onSpilling1 != null)
        {
            onSpilling1();
        }
    }
    public void Chugging()
    {
        if (onChugging != null)
        {
            onChugging();
        }
    }
    public void Spilling2()
    {
        if (onSpilling2 != null)
        {
            onSpilling2();
        }
    }
}
