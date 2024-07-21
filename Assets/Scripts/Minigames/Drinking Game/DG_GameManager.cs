using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class DG_GameManager : MonoBehaviour
{
    //Variables for points and stats tracking
    [SerializeField] private int regularDrank;
    [SerializeField] private int goldenDrank;
    [SerializeField] private int totalPoints;
    [SerializeField] private float totalDrank;

    //Variables for tankard generation
    [SerializeField] private int randomNum;
    [SerializeField] private int randomMax;
    [SerializeField] private int sinceGolden;
    [SerializeField] private bool isCurrentGolden;

    //Value on balance meter, much like a speedometer
    [SerializeField] private float balanceLevel;

    //Multiplies the standard deltatime for greater decay rate
    public float decayRate = 20f;

    [SerializeField] private float totalSpillageAmount;
    [SerializeField] private float currentSpillageAmount;
    [SerializeField] private float amountLeft;

    [SerializeField] private List<string> UpcomingTankards = new List<string>(5);

    //Has the drink been changed, used to halt following operations until change has been made
    [SerializeField] private bool isChangeWaiting;

    [SerializeField] private bool isGameOver;

    //References to the 5 upcoming tankard sprites
    public GameObject FirstTankard;
    public GameObject SecondTankard;
    public GameObject ThirdTankard;
    public GameObject FourthTankard;
    public GameObject FifthTankard;

    //References to the sprites for the 2 tankard variations
    public Sprite RegularTankard;
    public Sprite GoldenTankard;

    //States of balancing
    [SerializeField] private BalanceState currentState;
    [SerializeField] private bool stateHasChanged;
    private enum BalanceState
    {
        Idle,
        Drinking,
        Spilling1,
        Chugging,
        Spilling2
    }

    //Bool to check if the setup steps have happened
    private bool gameIsSetup = false;


    public Slider ProgressSlider;


    // Start is called before the first frame update
    void Start()
    {
        DG_Events.current.onStartGame += OnStartGame;
        DG_Events.current.onTimeOver += OnTimeOver;
    }
    private void OnStartGame()
    {
        isCurrentGolden = false;

        randomMax = 1;
        sinceGolden = 0;

        totalSpillageAmount = 0f;
        currentSpillageAmount = 0f;

        GenerateStartingDrinks();

        gameIsSetup = true;
    }

    private void OnTimeOver()
    {
        Debug.Log("Game Over!");

        //Total and display player stats to UI
        Debug.Log($"Regular: {regularDrank} |Golden: {goldenDrank} |Total: {totalDrank} |Amount Spilt: {totalSpillageAmount}");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsSetup)
        {
            if (!isGameOver)
            {
                if (!isChangeWaiting)
                {
                    if (amountLeft <= 0)
                    {
                        DrinkEmpty();
                    }

                    if (amountLeft >= 0)
                    {
                        ProgressSliderUpdate();
                    }

                    if (balanceLevel >= 0)
                    {
                        if ((balanceLevel < 100) && (amountLeft > 0))
                        {
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                Debug.Log("Player has pressed SPACE");

                                balanceLevel += 10f;
                            }
                        }
                    }

                    if (balanceLevel > 0)
                    {
                        BalanceStateUpdate();

                        //Constantly lower the balance meter
                        balanceLevel -= decayRate * Time.deltaTime;
                    }

                    //Sets lower limit of balance meter to 0
                    if (balanceLevel < 0)
                    {
                        balanceLevel = 0f;
                    }

                    //Sets upper limit of balance meter to 100
                    if (balanceLevel > 100)
                    {
                        balanceLevel = 100f;
                    }
                }
            }
        }
    }

    //CODE FOR GENERATING THE FIRST 5 UPCOMING TANKARDS
    private void GenerateStartingDrinks()
    {
        for (int i = 1; i < 6; i++)
        {
            Debug.Log($"Starting Tankard #{i} has been generated");

            //This will pick a random number, but by having the max be a variable it will allow for the chances to be easily changed
            randomNum = Random.Range(1, randomMax);

            if (randomNum == 1)
            {
                UpcomingTankards.Insert((i - 1), "Regular");

                //Increases the count everytime a tankard isnt chosen to be golden
                sinceGolden += 1;

                //Every time a normal tankard is chosen, the chance for a golden tankard increases by 20%
                randomMax = (5 - sinceGolden);

                if (i == 5)
                {
                    SpriteChanger();
                }
            }
            else
            {
                UpcomingTankards.Insert((i - 1), "Golden");

                //Since a golden tankard was chosen, the count is reset
                sinceGolden = 0;

                //Any time a tankard is selected to be golden, the next one is guaranteed to be normal
                randomMax = 1;

                if (i == 5)
                {
                    SpriteChanger();
                }
            }
        }  
    }
    //CODE FOR REMOVING EMPTY TANKARD
    private void DrinkEmpty()
    {
        balanceLevel = 0f;

        if (isCurrentGolden == true)
        {
            goldenDrank++;

            if (currentSpillageAmount == 0)
            {
                Debug.Log("Completed a golden tankard without spilling! An extra 10s has been added!");

                DG_Events.current.AddBonusTime();
            }

            NewDrink();
        }
        if (isCurrentGolden == false)
        {
            regularDrank++;

            NewDrink();
        }
    }

    //CODE FOR MAKING NEXT TANKARD THE CURRENT ONE
    private void NewDrink()
    {
        isChangeWaiting = true;

        if ((UpcomingTankards[0] == "Regular") || (UpcomingTankards[0] == "Golden"))
        {
            if (UpcomingTankards[0] == "Golden")
            {
                isCurrentGolden = true;
                UpcomingTankards[0] = "";
                MoveListUp();
            }
            if (UpcomingTankards[0] == "Regular")
            {
                isCurrentGolden = false;
                UpcomingTankards[0] = "";
                MoveListUp();
            }
        } 
    }

    //CODE FOR MOVING TANKARDS IN LIST
    private void MoveListUp()
    {
        string currentListValue;

        for (int i = 0; i < 4; i++)
        {
            currentListValue = UpcomingTankards[i + 1];
            UpcomingTankards.RemoveAt(i + 1);
            UpcomingTankards.Insert(i, currentListValue);

            if (i == 3)
            {
                if ((UpcomingTankards[4] == "") || (UpcomingTankards[4] == null))
                {
                    FillLastSlot();
                }
            }
        }
    }

    //CODE FOR ADDING NEW TANKARD TO LAST SLOT
    private void FillLastSlot()
    {
        //This will pick a random number, but by having the max be a variable it will allow for the chances to be easily changed
        randomNum = Random.Range(1, randomMax);

        if (randomNum == 1)
        {
            UpcomingTankards[4] = "Regular";
            //Increases the count everytime a tankard isnt chosen to be golden
            sinceGolden += 1;

            //Every time a normal tankard is chosen, the chance for a golden tankard increases by 20%
            randomMax = (5 - sinceGolden);
        }
        else
        {
            UpcomingTankards[4] = "Golden";

            //Since a golden tankard was chosen, the count is reset
            sinceGolden = 0;

            //Any time a tankard is selected to be golden, the next one is guaranteed to be normal
            randomMax = 1;
        }

        if ((UpcomingTankards[4] == "Regular") || (UpcomingTankards[4] == "Golden"))
        {
            SpriteChanger();
        }
    }


    private void SpriteChanger()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i == 0)
            {
                if (UpcomingTankards[0] == "Golden")
                {
                    //Sets reference to the sprite renderer component that is on the corresponding tankard object
                    SpriteRenderer spriteRenderer = FirstTankard.GetComponent<SpriteRenderer>();
                    //Sets the objects sprite to its correct state
                    spriteRenderer.sprite = GoldenTankard;
                }
                if (UpcomingTankards[0] == "Regular")
                {
                    //Sets reference to the sprite renderer component that is on the corresponding tankard object
                    SpriteRenderer spriteRenderer = FirstTankard.GetComponent<SpriteRenderer>();
                    //Sets the objects sprite to its correct state
                    spriteRenderer.sprite = RegularTankard;
                }
            }
            if (i == 1)
            {
                if (UpcomingTankards[1] == "Golden")
                {
                    //Sets reference to the sprite renderer component that is on the corresponding tankard object
                    SpriteRenderer spriteRenderer = SecondTankard.GetComponent<SpriteRenderer>();
                    //Sets the objects sprite to its correct state
                    spriteRenderer.sprite = GoldenTankard;
                }
                if (UpcomingTankards[1] == "Regular")
                {
                    //Sets reference to the sprite renderer component that is on the corresponding tankard object
                    SpriteRenderer spriteRenderer = SecondTankard.GetComponent<SpriteRenderer>();
                    //Sets the objects sprite to its correct state
                    spriteRenderer.sprite = RegularTankard;
                }
            }
            if (i == 2)
            {
                if (UpcomingTankards[2] == "Golden")
                {
                    //Sets reference to the sprite renderer component that is on the corresponding tankard object
                    SpriteRenderer spriteRenderer = ThirdTankard.GetComponent<SpriteRenderer>();
                    //Sets the objects sprite to its correct state
                    spriteRenderer.sprite = GoldenTankard;
                }
                if (UpcomingTankards[2] == "Regular")
                {
                    //Sets reference to the sprite renderer component that is on the corresponding tankard object
                    SpriteRenderer spriteRenderer = ThirdTankard.GetComponent<SpriteRenderer>();
                    //Sets the objects sprite to its correct state
                    spriteRenderer.sprite = RegularTankard;
                }
            }
            if (i == 3)
            {
                if (UpcomingTankards[3] == "Golden")
                {
                    //Sets reference to the sprite renderer component that is on the corresponding tankard object
                    SpriteRenderer spriteRenderer = FourthTankard.GetComponent<SpriteRenderer>();
                    //Sets the objects sprite to its correct state
                    spriteRenderer.sprite = GoldenTankard;
                }
                if (UpcomingTankards[3] == "Regular")
                {
                    //Sets reference to the sprite renderer component that is on the corresponding tankard object
                    SpriteRenderer spriteRenderer = FourthTankard.GetComponent<SpriteRenderer>();
                    //Sets the objects sprite to its correct state
                    spriteRenderer.sprite = RegularTankard;
                }
            }
            if (i == 4)
            {
                if (UpcomingTankards[4] == "Golden")
                {
                    //Sets reference to the sprite renderer component that is on the corresponding tankard object
                    SpriteRenderer spriteRenderer = FifthTankard.GetComponent<SpriteRenderer>();
                    //Sets the objects sprite to its correct state
                    spriteRenderer.sprite = GoldenTankard;
                    amountLeft = 100f;
                    isChangeWaiting = false;
                }
                if (UpcomingTankards[4] == "Regular")
                {
                    //Sets reference to the sprite renderer component that is on the corresponding tankard object
                    SpriteRenderer spriteRenderer = FifthTankard.GetComponent<SpriteRenderer>();
                    //Sets the objects sprite to its correct state
                    spriteRenderer.sprite = RegularTankard;
                    amountLeft = 100f;
                    isChangeWaiting = false;
                }
            }
        }
    }

    //Handles balance mechanic
    private void BalanceStateUpdate()
    {
        if (balanceLevel <= 20)
        {
            //Do nothing

            if (currentState != BalanceState.Idle)
            {
                Debug.Log("State: Idle");
                DG_Events.current.Idle();
                currentState = BalanceState.Idle;
            }
        }
        if ((balanceLevel > 20) && (balanceLevel <= 50))
        {
            if (currentState != BalanceState.Drinking)
            {
                Debug.Log("State: Drinking");
                DG_Events.current.Drinking();
                currentState = BalanceState.Drinking;
            }
        }
        if ((balanceLevel > 50) && (balanceLevel <= 70))
        {
            if (currentState != BalanceState.Spilling1)
            {
                Debug.Log("State: Spilling 1");
                DG_Events.current.Spilling1();
                currentState = BalanceState.Spilling1;
            }
        }
        if ((balanceLevel > 70) && (balanceLevel <= 85))
        {
            if (currentState != BalanceState.Chugging)
            {
                Debug.Log("State: Chugging");
                DG_Events.current.Chugging();
                currentState = BalanceState.Chugging;
            }
        }
        if ((balanceLevel > 85) && (balanceLevel <= 100))
        {
            if (currentState != BalanceState.Spilling2)
            {
                Debug.Log("State: Spilling 2");
                DG_Events.current.Spilling2();
                currentState = BalanceState.Spilling2;
            }
        }

        switch (currentState)
        {
            case BalanceState.Idle:
                
                break;

            case BalanceState.Drinking:
                //Gradually decrease amountLeft
                amountLeft -= 2 * Time.deltaTime;
                totalDrank += 2 * Time.deltaTime;
                break;

            case BalanceState.Spilling1:
                //Gradually increase spill meter and decrease amount left at a slower rate
                currentSpillageAmount += Time.deltaTime;
                totalSpillageAmount += Time.deltaTime;
                amountLeft -= Time.deltaTime;
                totalDrank += Time.deltaTime;
                break;

            case BalanceState.Chugging:
                //Decrease amountLeft by a larger value
                amountLeft -= 6 * Time.deltaTime;
                totalDrank += 6 * Time.deltaTime;
                break;
            case BalanceState.Spilling2:
                //Gradually increase spill meter at a larger rate
                currentSpillageAmount += 2 * Time.deltaTime;
                totalSpillageAmount += 2 * Time.deltaTime;
                amountLeft -= 2 * Time.deltaTime;
                break;
        }
    }

    private void ProgressSliderUpdate()
    {
        //Update the slider (temporary solution) regularly to show amount left in current drink
        ProgressSlider.value = amountLeft;
    }
}
