using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class DrinkService : MonoBehaviour
{
    //Content Warning: Spaghetti code
    public GameObject ScoreManager;

    public float kegVolume;
    public int nozzleSetting;
    private float pourRate;
    public int vesselAngle;
    public int vesselSize;
    public int currentCapacity;
    public int totalCapacity;
    public float currentVolume;
    public bool vesselInHand;
    private bool taskComplete;
    private bool instructionsVisible;
    public bool drinkReady;
    private float inputTimer;
    private float grogInTransit;
    public float wastedGrog;
    public float liquidVolume;
    public float frothVolume;
    public float liquidPercent;
    public float frothPercent;
    public bool nucleation;
    public int grade;
    public GameObject MugPrefab;
    public GameObject KegTap;
    public DrinkServiceAngle AngleScript;
    public ScoreTrack ScoreTrack;

    public SpriteRenderer tapRenderer;
    public Sprite tapSprite0;
    public Sprite tapSprite1;
    public Sprite tapSprite2;
    public Sprite tapSprite3;

    public TextMeshProUGUI notifyUI;
    public TextMeshProUGUI instructions;
    public TextMeshProUGUI notifySpill;

    //Handles time limit

    //Generates customer orders

    //Handles drink selections
    
    // Start is called before the first frame update
    void Start()
    {
        nozzleSetting = 0;
        pourRate = 0;
        vesselInHand = false;
        taskComplete = false;
        instructionsVisible = false;
        kegVolume = 50000;
        currentCapacity = 0;
        ScoreTrack = FindObjectOfType<ScoreTrack>();

    }

    // Update is called once per frame
    void Update()
    {
        // Controls the angle (and subsequently its current capacity) of a mug when in hand.
        if (vesselInHand == true)
        {

            //Increases angle and current capacity
            if ((Input.GetKey(KeyCode.A)) && (vesselAngle > 10))
                if (inputTimer <= 0)
            {
                vesselAngle -= 1;
                inputTimer = 1;
                UpdateCurrentCapacity();

            }

            //Decreases angle and current capacity.
            if ((Input.GetKey(KeyCode.D)) && (vesselAngle < 30))
            {
                if (inputTimer <= 0)
                {
                    vesselAngle += 1;
                    inputTimer = 1;
                    UpdateCurrentCapacity();
                }
            }
        }
        //Angle adjust support
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            inputTimer = 0;
        }
        // Speed of angle adjust
        if (inputTimer >= 0)
        {
            inputTimer -= Time.deltaTime * 15;
        }
        // Increases flow of booze
        if ((Input.GetKeyDown(KeyCode.S)) && (nozzleSetting < 3))
        {
            nozzleSetting +=1;
            ChangePour();
        }
        //Decreases flow of booze
        if ((Input.GetKeyDown(KeyCode.W)) && (nozzleSetting > 0))
        {
            nozzleSetting -= 1;
            ChangePour();
        }
        //Cuts flow of booze
        if ((Input.GetKeyDown(KeyCode.Space)) && (nozzleSetting > 0))
        {
            nozzleSetting = 0;
            ChangePour();
        }

        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            if (!instructionsVisible)
            {
                instructions.text = "PRESS TAB TO HIDE<br><br>" +
                    "HOW TO PLAY:<br><br>" +
                    "You are to pour the ideal mug of beer, 95% liquid and 5% froth.<br><br>" +
                    "The closer, the better the score!<br>Use the (E) key to get a new mug.<br><br>" +
                    "Use the (A) and (D) keys to change the angle of the vessel.<br><br>" +
                    "Use the (S) and (W) keys to change the flow from the nozzle.<br><br>" +
                    "You can also press (SPACE) to cut the nozzle to 0.<br><br>" +
                    "At the end, press (E) again to serve!";
                instructionsVisible = true;
            }
            else if (instructionsVisible)
            {
                instructions.text = "PRESS TAB TO SHOW INSTRUCTIONS";
                instructionsVisible = false;
            }
        }



        //Trigger flow of booze
        if (nozzleSetting > 0)
        {
            StartPour();
        }



        // This should either A) if you have no mug equipped, equip and a new one to hand, or B) if you have a mug equipped, serve it.
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (vesselInHand == true)
            {
                ServeDrink();
            }

            else if ((vesselInHand == false) && (taskComplete ==  false))
            {
                NewVessel();
            }
        }
        // Rules for when to apply froth
        if (liquidVolume > (totalCapacity - currentCapacity))
        {
            nucleation = true;
        }

        else
        {
            nucleation = false;
        }

    }

    public void NewVessel()
    {
        // Player picks up a new glass, generate new size, ensure its empty
        vesselInHand = true;
        vesselSize = 2;
        currentVolume = 0;
        totalCapacity = 150 * vesselSize;
        vesselAngle = 15;
        grade = 0;
        Instantiate(MugPrefab);
        AngleScript = FindObjectOfType<DrinkServiceAngle>();
        UpdateCurrentCapacity();
    }

    public void ServeDrink()
    {
        // Player serves drink to client. Grades performance and resets values.
        vesselAngle = 30;
        UpdateCurrentCapacity();
        GradeTask();
        liquidVolume = 0;
        frothVolume = 0;
        wastedGrog = 0;
        currentCapacity = 0;
        vesselInHand = false;
        AngleScript.ServeDrink();
        if (grade <= 3)
        {
            Debug.Log("Task Complete. Returning to Tavern.");
            taskComplete = true;
            ScoreTrack.drinkOpen = false;
            SceneManager.LoadScene(1);
        }

        else
        {
            grade = 0;
            StartCoroutine(ClearTimer());
        }
    }

    public void ChangePour()
    {
        // Math for flow settings
        pourRate = nozzleSetting * 15;
        if (nozzleSetting == 0)
        {
            tapRenderer.sprite = tapSprite0;
        }
        else if (nozzleSetting == 1)
        {
            tapRenderer.sprite = tapSprite1;
        }
        else if (nozzleSetting == 2)
        {
            tapRenderer.sprite = tapSprite2;
        }
        else if (nozzleSetting == 3)
        {
            tapRenderer.sprite = tapSprite3;
        }
    }


    public void StartPour()
    {
        if (kegVolume > 0)
        {
            grogInTransit += (Time.deltaTime * pourRate);
            kegVolume -= grogInTransit;
            if (vesselInHand == true)
            {
                if (currentVolume <= currentCapacity)
                {
                    if (nucleation == true)
                    {
                        liquidVolume += (grogInTransit * 0.9f);
                        frothVolume += (grogInTransit * 0.1f);
                        // currentVolume += grogInTransit;
                       /* if (currentVolume >= currentCapacity)
                        {
                            frothVolume -= grogInTransit;
                            liquidVolume += grogInTransit;
                        } */
                        grogInTransit = 0;
                    }

                    else if (nucleation == false)
                    {
                        liquidVolume += grogInTransit;
                        grogInTransit = 0;
                    }
                    currentVolume = liquidVolume + frothVolume;
                    liquidPercent = (liquidVolume / currentVolume) * 100;
                    frothPercent = (frothVolume / currentVolume) * 100;
                }
                else if (currentVolume >= currentCapacity)
                {
                    wastedGrog += grogInTransit;
                    grogInTransit = 0;
                    if (notifySpill.text == "")
                    {
                        notifySpill.text = "You're spilling it!";
                        StartCoroutine(ClearTimer2());
                    }
                }
            }
            else
            {
                wastedGrog += grogInTransit;
                grogInTransit = 0;
            }
        }

    }

    public void UpdateCurrentCapacity()
    {
        // Updates current capacity with angle adjustments
        currentCapacity = (totalCapacity / 30) * vesselAngle;
        AngleScript.UpdateAngle();
    }

    public void GradeTask()
    {
        // Review task, assign a grade and clear
        if (wastedGrog < 10)
        {
            grade += 0;
        }

        else if ((wastedGrog > 10) && (wastedGrog < 20))
        {
            grade += 1;
        } 

        else if (wastedGrog > 20)
        {
            grade += 2;
        }

        if ((frothPercent >= 4) && (frothPercent <= 6))
        {
            grade += 0;
        }

        else if (frothPercent < 4)
        {
            //Too little froth
            grade += 1;
        }

        else if ((frothPercent > 6) && (frothPercent < 10))
        {
            //Too much froth
            grade += 1;
        }

        else if (frothPercent >= 10)
        {
            //WAY TOO MUCH froth
            grade += 2;
        }

        if ((liquidPercent >= 94) && (liquidPercent <= 96))
        {
            grade += 0;
        }

        else if (liquidPercent > 96)
        {
            grade += 1;
        }

        else if ((liquidPercent < 94) && (liquidPercent >= 85))
        {
            grade += 2;
        }

        else if ((liquidPercent < 85) && (liquidPercent >= 60))
        {
            grade += 3;
        }

        else if (liquidPercent < 60)
        {
            grade += 4;
        }

        if (currentVolume < 225)
        {
            grade += 4;
        }

        if (grade == 0)
        {
            notifyUI.text = "PERFECT!";
            ScoreTrack.goldAmount += 10;
        }

        if (grade == 1)
        {
            notifyUI.text = "Great!";
            ScoreTrack.goldAmount += 8;
        }

        if (grade == 2)
        {
            notifyUI.text = "Good!";
            ScoreTrack.goldAmount += 6;
        }

        if (grade == 3)
        {
            notifyUI.text = "Passable.";
            ScoreTrack.goldAmount += 4;
        }

        if (grade >= 4)
        {
            notifyUI.text = "Bad. Try again.";
        }
    }

    IEnumerator ClearTimer()
    {
        yield return new WaitForSeconds(2f);
        notifyUI.text = "";
    }

    IEnumerator ClearTimer2()
    {
        yield return new WaitForSeconds(2f);
        notifySpill.text = "";
    }
}
