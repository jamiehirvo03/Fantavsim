using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkingGame : MonoBehaviour
{
    //Varaibles for points and stats tracking
    private bool isGolden;
    private int regularDrank;
    private int goldenDrank;
    private int totalPoints;
    private float totalDrank;

    //Variables for tankard generation
    private int randomNum;
    private int randomMax;
    private int sinceGolden;

    //Value on balance meter, much like a speedometer
    private int balanceLevel;
    private float spillageAmount;
    private float amountLeft;

    [SerializeField] private List<string> TankardOrder = new List<string>();
    private enum BalanceState
    {
        Idle,
        Drinking,
        Spilling,
        Chugging
    }

    private BalanceState currentState;

    void Start()
    {
        //Events that listen when the game tutorial has been displayed and player has clicked start
        EventManager.current.onStartDrinkingGame += OnStartDrinkingGame;
        EventManager.current.onDrinkingGameTimeOver += OnTimeOver;
    }

    private void OnStartDrinkingGame()
    {
        GameSetup();
        
    }

    private void OnTimeOver()
    {
        Debug.Log("Game Over");

        //Total and display player stats
        Debug.Log($"Regular: {regularDrank}|Golden: {goldenDrank}|Total Drank: {totalDrank}|Amount Spilt: {spillageAmount}");
    }

    private void Update()
    {        
        if (amountLeft <= 0)
        {
            if (isGolden)
            {
                goldenDrank++;
                NextDrink();

            }
            else
            {
                regularDrank++;
                NextDrink();
            }
        }
    }

    private void GameSetup()
    {
        randomMax = 1;
        sinceGolden = 0;

        spillageAmount = 0f;

        EventManager.current.ShowTimer();
    }

    private void NextDrink()
    {
        currentState = BalanceState.Idle;
        
        amountLeft = 100;

        TankardGenerator();
    }

    //Generates tankard order
    private void TankardGenerator()
    {
        for (int i = 1; i < 5; i++)
        {
            //This will pick a random number, but by having the max be a variable it will allow for the chances to be easily changed
            randomNum = Random.Range(1, randomMax);

            if (randomNum == 1)
            {
                isGolden = false;

                //Increases the count everytime a tankard isnt chosen to be golden
                sinceGolden += 1;

                //Every time a normal tankard is chosen, the chance for a golden tankard increases by 20%
                randomMax = (5 - sinceGolden);
            }
            else
            {
                isGolden = true;

                //Since a golden tankard was chosen, the count is reset
                sinceGolden = 0;

                //Any time a tankard is selected to be golden, the next one is guaranteed to be normal
                randomMax = 1;
            }
        }
    }

    //Handles balance mechanic
    private void BalanceMeter()
    {
        switch (currentState)
        {
            case BalanceState.Idle:
                //Do nothing
                Debug.Log("Player is idle");
                break;

            case BalanceState.Drinking:
                //Gradually decrease amountLeft
                amountLeft -= Time.deltaTime;
                totalDrank += Time.deltaTime;
                Debug.Log("Player is drinking");
                break;

            case BalanceState.Spilling:
                //Gradually increase spill meter
                spillageAmount += Time.deltaTime;
                totalDrank += 0.5f * Time.deltaTime;
                Debug.Log("Player is spilling");
                break;

            case BalanceState.Chugging:
                //Decrease amountLeft by a larger value
                amountLeft -= 2 * Time.deltaTime;
                Debug.Log("Player is chugging");
                break;
        }
    }

  
}

