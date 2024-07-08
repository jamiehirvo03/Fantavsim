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

    //Variables for tankard generation
    private int randomNum;
    private int randomMax;
    private int sinceGolden;

    //Value on balance meter, much like a speedometer
    private int balanceLevel;
    private bool inDrinkZone;
    private bool inBoostZone;
    private bool inSpillZone;
    private float spillageAmount;

    private float amountLeft;

    void Start()
    {
        GameSetup();
    }

    private void Update()
    {        
        if (inSpillZone)
        {
            //Gradually increase spill meter
            spillageAmount +=  Time.deltaTime;
        }

        if (inDrinkZone)
        {
            //Gradually decrease amount in tankard
            amountLeft -= Time.deltaTime;
        }

        if (inBoostZone)
        {
            //Decrease amount in tankard by a larger value
            amountLeft -= 2 * Time.deltaTime;
        }
    }

    private void updateTimer(float timer)
    {
        timer -= Time.deltaTime;

        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
    }

    private void GameSetup()
    {
        randomMax = 1;
        sinceGolden = 0;

        inDrinkZone = false;
        inSpillZone = false;

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
