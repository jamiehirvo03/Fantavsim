using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class DrinkingGame : MonoBehaviour
{
    //Varaibles for points and stats tracking
    [SerializeField] private int regularDrank;
    [SerializeField] private int goldenDrank;
    [SerializeField] private int totalPoints;
    [SerializeField] private float totalDrank;

    //Variables for tankard generation
    private int randomNum;
    private int randomMax;
    private int sinceGolden;

    //Value on balance meter, much like a speedometer
    [SerializeField] private float balanceLevel;
    
    //Multiplies the standard deltatime for greater decay rate
    public float decayRate = 5f;

    [SerializeField] private float spillageAmount;
    [SerializeField] private float amountLeft;

    [SerializeField] private List<bool> IsGolden = new List<bool>(5);
    private bool currentListValue;
    private enum BalanceState
    {
        Idle,
        Drinking,
        Spilling,
        Chugging
    }

    private BalanceState currentState;

    //Bool to check if the setup steps have happened
    private bool gameIsSetup = false;

    void Start()
    {
        //Events that listen when the game tutorial has been displayed and player has clicked start
        EventManager.current.onStartDrinkingGame += OnStartDrinkingGame;
        EventManager.current.onDrinkingGameTimeOver += OnTimeOver;
    }

    //Listens to when the start button is pressed (to start the game once rules have been read)
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
        if (gameIsSetup)
        {
            if (amountLeft <= 0)
            {
                if (IsGolden[0] == true)
                {
                    goldenDrank++;
                    ClearDrink();
                    NextDrink();

                }
                else
                {
                    regularDrank++;
                    ClearDrink();
                    NextDrink();
                }
            }
            
            if (balanceLevel >= 0)
            {
                //Constantly lower the balance meter
                balanceLevel -= decayRate * Time.deltaTime;

                if (balanceLevel < 100)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Debug.Log("Player has pressed SPACE");

                        balanceLevel += 10f;
                    }
                }  
            }

            //Sets lower limit of balance meter to 0
            if (balanceLevel < 0)
            {
                balanceLevel = 0;
            }

            //Sets upper limit of balance meter to 100
            if (balanceLevel > 100)
            {
                balanceLevel = 100;
            }
        }
    }

    private void GameSetup()
    {
        randomMax = 1;
        sinceGolden = 0;

        spillageAmount = 0f;

        StartingTankards();
        amountLeft = 100f;

        EventManager.current.ShowTimer();

        gameIsSetup = true;
    }

    private void ClearDrink()
    {
        IsGolden.RemoveAt(0);

        for (int i = 1; i < 5; i++)
        {
            currentListValue = IsGolden[i];
            IsGolden.RemoveAt(i);
            IsGolden.Insert(i - 1, currentListValue);
        }
    }

    private void NextDrink()
    {
        balanceLevel = 0f;
        
        amountLeft = 100f;

        TankardGenerator();
    }

    //Generates the first 5 tankards
    private void StartingTankards()
    {
        for (int i = 1; i < 6; i++)
        {
            Debug.Log($"Starting Tankard #{i} has been generated");

            //This will pick a random number, but by having the max be a variable it will allow for the chances to be easily changed
            randomNum = Random.Range(1, randomMax);

            if (randomNum == 1)
            {
                IsGolden.Insert((i - 1), false);

                //Increases the count everytime a tankard isnt chosen to be golden
                sinceGolden += 1;

                //Every time a normal tankard is chosen, the chance for a golden tankard increases by 20%
                randomMax = (5 - sinceGolden);
            }
            else
            {
                IsGolden.Insert((i - 1), true);

                //Since a golden tankard was chosen, the count is reset
                sinceGolden = 0;

                //Any time a tankard is selected to be golden, the next one is guaranteed to be normal
                randomMax = 1;
            }
        }
    }

    //Generates next tankard
    private void TankardGenerator()
    {
        //This will pick a random number, but by having the max be a variable it will allow for the chances to be easily changed
        randomNum = Random.Range(1, randomMax);

        if (randomNum == 1)
        {
            IsGolden.Insert(4, false);

            //Increases the count everytime a tankard isnt chosen to be golden
            sinceGolden += 1;

            //Every time a normal tankard is chosen, the chance for a golden tankard increases by 20%
            randomMax = (5 - sinceGolden);
        }
        else
        {
            IsGolden.Insert(4, true);

            //Since a golden tankard was chosen, the count is reset
            sinceGolden = 0;

            //Any time a tankard is selected to be golden, the next one is guaranteed to be normal
            randomMax = 1;
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

