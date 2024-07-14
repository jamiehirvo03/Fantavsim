using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkingGame : MonoBehaviour
{
    //Varaibles for points and stats tracking
    private int regularDrank;
    private int goldenDrank;
    private int totalPoints;
    private float totalDrank;
    private float timer;

    //Variables for tankard generation
    private int randomNum;
    private int randomMax;
    private int sinceGolden;

    //Value on balance meter, much like a speedometer
    private int balanceLevel;
    private float spillageAmount;
    private float amountLeft;

    private enum BalanceState
    {
        Idle,
        Drinking,
        Spilling,
        Boosting
    }

    private BalanceState currentState;

    void Start()
    {
        GameSetup();
    }

    private void Update()
    {        
        switch (currentState)
        {
            case BalanceState.Idle:
                //Do nothing
                break;
            case BalanceState.Drinking:
                //Gradually decrease amountLeft
                amountLeft -= Time.deltaTime;
                break;
            case BalanceState.Spilling:
                //Gradually increase spill meter
                spillageAmount += Time.deltaTime;
                break;
            case BalanceState.Boosting:
                //Decrease amountLeft by a larger value
                amountLeft -= 2 * Time.deltaTime;
                break;
        }
    }

    private void updateTimer()
    {
        timer -= Time.deltaTime;

        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
    }

    private void GameSetup()
    {
        randomMax = 1;
        sinceGolden = 0;

        currentState = BalanceState.Idle;

        spillageAmount = 0f;

        timer = 60.00f;
    }

    //Generates tankard order
    private void TankardGenerator(bool isGolden)
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

    //Handles balance mechanic

    //Keeps track of spill amount
    private void spillTracker()
    {

    }
}
