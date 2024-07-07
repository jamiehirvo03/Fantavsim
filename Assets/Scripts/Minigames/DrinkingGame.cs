using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkingGame : MonoBehaviour
{
    private int timer;
    private int regularDrank;
    private int goldenDrank;
    private int spillageAmount;

    private int randomNum;
    private int randomMax;
    private int sinceGolden;

    private void Awake()
    {
        GameSetup();
    }
    private void GameSetup()
    {
        randomMax = 1;
        sinceGolden = 0;
    }

    //Generates tankard order
    private void TankardGenerator(bool isGolden)
    {
        randomNum = Random.Range(1, randomMax);
        //This will pick a random number, but by having the max be a variable it will allow for the chances to be easily changed

        if (randomNum == 1)
        {
            isGolden = false;

            sinceGolden += 1;
            //Increases the count everytime a tankard isnt chosen to be golden
            randomMax = (5 - sinceGolden);
            //Every time a normal tankard is chosen, the chance for a golden tankard increases by 20%
        }
        else
        {
            isGolden = true;

            sinceGolden = 0;
            //Since a golden tankard was chosen, the count is reset
            randomMax = 1;
            //Any time a tankard is selected to be golden, the next one is guaranteed to be normal
        }

    }

    //Handles balance mechanic

    //Keeps track of spill amount
    private void spillTracker()
    {

    }

    //Totals points throughout game

    // Start is called before the first frame update
}
