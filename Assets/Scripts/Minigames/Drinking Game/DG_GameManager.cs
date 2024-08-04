using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DG_GameManager : MonoBehaviour
{
    //Variables for points and stats tracking
    [SerializeField] private int regularDrank;
    [SerializeField] private int goldenDrank;
    [SerializeField] private int totalPoints;
    [SerializeField] private float totalDrank;
    [SerializeField] private float litresDrank = 0f;
    [SerializeField] private float litresDrankRounded;

    [SerializeField] private float amountLeft;
    [SerializeField] private float totalSpillageAmount;
    [SerializeField] private float currentSpillageAmount;
    [SerializeField] private float litresSpilt = 0f;
    [SerializeField] private float litresSpiltRounded;

    //Variables for tankard generation
    [SerializeField] private int randomNum;
    [SerializeField] private int randomMax;
    [SerializeField] private int sinceGolden;
    [SerializeField] private bool isCurrentGolden;

    [SerializeField] private List<string> UpcomingTankards = new List<string>(5);

    //Value on balance meter, much like a speedometer
    [SerializeField] private float balanceLevel;

    //Multiplies the standard deltatime for greater decay rate
    public float decayRate = 20f;

    //Bool to check if the setup steps have happened
    [SerializeField] private bool isGameSetup = false;
    //Has the new drink setup started
    [SerializeField] private bool isChangeStarted;
    //Has the drink been changed, used to halt following operations until change has been made
    [SerializeField] private bool isChangeWaiting;
    //Has the current tankard already been changed?
    [SerializeField] private bool isCurrentChanged;
    //Has the timer reached 0? if so end the game
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

    // VVVVV PUT IN UI SCRIPT VVVVV
    public Slider ProgressSlider;
    public TextMeshProUGUI AmountDrank;
    public TextMeshProUGUI AmountSpilt;
    public Canvas GameOverPopup;
    public TextMeshProUGUI GameOverText;

    public GameObject BalanceMeter;
    public GameObject BalanceMeterMarker;
    public float minRotationAngle;
    public float maxRotationAngle;
    // ^^^^^ PUT IN UI SCRIPT ^^^^^


    // Start is called before the first frame update
    void Start()
    {
        DG_Events.current.onStartGame += OnStartGame;
        DG_Events.current.onTimeOver += OnTimeOver;

        ProgressSlider.enabled = false;
        AmountDrank.enabled = false;
        AmountSpilt.enabled = false;
        GameOverPopup.enabled = false;
        
        isGameOver = false;
    }
    private void OnStartGame()
    {
        
        isCurrentGolden = false;

        randomMax = 1;
        sinceGolden = 0;

        totalDrank = 0;
        totalSpillageAmount = 0;
        currentSpillageAmount = 0;

        ProgressSlider.enabled = true;
        AmountDrank.enabled = true;
        AmountSpilt.enabled = true;

        GenerateStartingDrinks();
        amountLeft = 100f;

        isChangeStarted = false;
        isGameSetup = true;

        Debug.Log("Game is set up");
    }

    private void OnTimeOver()
    {
        Debug.Log("Game Over!");

        ProgressSlider.enabled = false;
        AmountDrank.enabled = false;
        AmountSpilt.enabled = false;

        isGameOver = true;

        //Total and display player stats to UI
        Debug.Log($"Regular: {regularDrank} |Golden: {goldenDrank} |Amount Drank: {litresDrank} L |Amount Spilt: {totalSpillageAmount}");

        GameOverPopup.enabled = true;

        GameOverText.text = $"Regular: {regularDrank} \nGolden: {goldenDrank} \nAmount Drank: {litresDrank} L \nAmount Spilt: {totalSpillageAmount}";

        //Display final screen on UI

        DG_Events.current.onTimeOver -= OnTimeOver;
    }

    public void RestartButtonClicked()
    {
        SceneManager.LoadScene("DrinkingGame");
    }

    public void ExitButtonClicked()
    {
        SceneManager.LoadScene("CleanupMinigame");
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameSetup)
        {
            if (!isGameOver)
            {
                if (!isChangeWaiting)
                {
                    if (amountLeft <= 0)
                    {
                        Debug.Log("Drink is empty");

                        if (!isChangeStarted)
                        {
                            DrinkEmpty();
                        }
                    }

                        if (amountLeft >= 0)
                        {
                            //Update the slider (temporary solution) regularly to show amount left in current drink
                            ProgressSlider.value = amountLeft;

                            //AmountDrankUpdate
                            litresDrank = Mathf.Round((totalDrank * 10.00f) / 10.00f);
                            litresDrankRounded = ((litresDrank * 5f));

                            AmountDrank.text = $"Amount Drank: {litresDrankRounded} mL";

                            if (totalDrank == 0)
                            {
                                AmountDrank.text = "Amount Drank: 0 mL";
                            }

                            //AmountSpiltUpdate
                            litresSpilt = Mathf.Round((totalSpillageAmount * 10.00f) / 10.00f);
                            litresSpiltRounded = ((litresSpilt * 5f));


                            AmountSpilt.text = $"Amount Spilt: {litresSpiltRounded} mL";

                            if (totalSpillageAmount == 0)
                            {
                                AmountSpilt.text = "Amount Spilt: 0 mL";
                            }
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

                    if (BalanceMeter != null)
                    {
                        if (BalanceMeterMarker != null)
                        {
                            BalanceMeterMarker.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minRotationAngle, maxRotationAngle, balanceLevel / 100));
                            //new Vector3(0, 0, Mathf.Lerp(minRotationAngle, maxRotationAngle, balanceLevel / 100));
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
            Debug.Log("Runnning DrinkEmpty()");

            balanceLevel = 0f;

            isCurrentChanged = false;

            if (isCurrentGolden == true)
            {
                goldenDrank++;

                if (currentSpillageAmount == 0)
                {
                    Debug.Log("Completed a golden tankard without spilling! An extra 10s has been added!");

                    DG_Events.current.AddBonusTime();
                }

                isChangeStarted = true;


                NewDrink();
            }
            if (isCurrentGolden == false)
            {
                regularDrank++;

                isChangeStarted = true;

                NewDrink();
            }     
        }

        //CODE FOR MAKING NEXT TANKARD THE CURRENT ONE
        private void NewDrink()
        {
            string currentDrink = UpcomingTankards[0];
            Debug.Log("Running NewDrink()");

            amountLeft = 100f;

            isChangeWaiting = true;
            isChangeStarted = false;

            if (!isCurrentChanged)
            {
                if (currentDrink == "Golden")
                {
                    isCurrentChanged = true;
                    Debug.Log("Current Tankard is changed to Golden");

                    isCurrentGolden = true;

                    DG_Events.current.CurrentDrinkGolden();    

                    UpcomingTankards[0] = "";
                    MoveListUp();
                }
                if (currentDrink == "Regular")
                {
                    isCurrentChanged = true;
                    Debug.Log("Current Tankard is changed to Regular");

                    isCurrentGolden = false;

                    DG_Events.current.CurrentDrinkRegular();

                    UpcomingTankards[0] = "";
                    MoveListUp();
                }
            }
        }

        //CODE FOR MOVING TANKARDS IN LIST
        private void MoveListUp()
        {
            Debug.Log("Running MoveListUp");

            string currentListValue;

            for (int i = 0; i < 4; i++)
            {
                currentListValue = UpcomingTankards[i + 1];
                UpcomingTankards[i] = currentListValue;

                if (i == 3)
                {
                    UpcomingTankards[i + 1] = "";    

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
            Debug.Log("Running FillLastSlot");

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

            if (isChangeWaiting)
            {
                SpriteChanger();
            }
        }


        private void SpriteChanger()
        {
            Debug.Log("Running SpriteChanger()");

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

                        currentSpillageAmount = 0f;
                        isChangeWaiting = false;
                }
                    if (UpcomingTankards[4] == "Regular")
                    {
                        //Sets reference to the sprite renderer component that is on the corresponding tankard object
                        SpriteRenderer spriteRenderer = FifthTankard.GetComponent<SpriteRenderer>();
                        //Sets the objects sprite to its correct state
                        spriteRenderer.sprite = RegularTankard;

                        currentSpillageAmount = 0f;
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
                    currentState = BalanceState.Idle;
                    DG_Events.current.Idle();
            }
            }
            if ((balanceLevel > 20) && (balanceLevel <= 50))
            {
                if (currentState != BalanceState.Drinking)
                {
                    Debug.Log("State: Drinking");
                    currentState = BalanceState.Drinking;
                    DG_Events.current.Drinking();
            }
            }
            if ((balanceLevel > 50) && (balanceLevel <= 70))
            {
                if (currentState != BalanceState.Spilling1)
                {
                    Debug.Log("State: Spilling 1");                    
                    currentState = BalanceState.Spilling1;
                    DG_Events.current.Spilling1();
                }
            }
            if ((balanceLevel > 70) && (balanceLevel <= 85))
            {
                if (currentState != BalanceState.Chugging)
                {
                    Debug.Log("State: Chugging");                    
                    currentState = BalanceState.Chugging;
                    DG_Events.current.Chugging();
                }
            }
            if ((balanceLevel > 85) && (balanceLevel <= 100))
            {
                if (currentState != BalanceState.Spilling2)
                {
                    Debug.Log("State: Spilling 2");                    
                    currentState = BalanceState.Spilling2;
                    DG_Events.current.Spilling2();
                }
            }

            switch (currentState)
            {
                case BalanceState.Idle:

                    break;

                case BalanceState.Drinking:
                    //Gradually decrease amountLeft
                    amountLeft -= 4 * Time.deltaTime;
                    totalDrank += 4 * Time.deltaTime;
                    break;

                case BalanceState.Spilling1:
                    //Gradually increase spill meter and decrease amount left at a slower rate
                    currentSpillageAmount += 2 * Time.deltaTime;
                    totalSpillageAmount += 2 * Time.deltaTime;
                    amountLeft -= 4 * Time.deltaTime;
                    totalDrank += 2 * Time.deltaTime;
                    break;

                case BalanceState.Chugging:
                    //Decrease amountLeft by a larger value
                    amountLeft -= 12 * Time.deltaTime;
                    totalDrank += 12 * Time.deltaTime;
                    break;
                case BalanceState.Spilling2:
                    //Gradually increase spill meter at a larger rate
                    currentSpillageAmount += 4 * Time.deltaTime;
                    totalSpillageAmount += 4 * Time.deltaTime;
                    amountLeft -= 4 * Time.deltaTime;
                    break;
            }
        }
    }
